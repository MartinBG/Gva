/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person address edit page', function() {
    var ptor = protractor.getInstance();

    beforeEach(function() {
      ptor.get('#/persons/1/addresses/2');
    });

    it('should update breadcrumb text', function () {
      /*jshint quotmark:false */
      var text = ptor.findElement(protractor.By.xpath("//ul[@class='breadcrumb']/li[last()]"))
        .getText();
      expect(text).toEqual('Редакция на адрес');
    });

    it('should display correct filled out data', function () {

      expect(ptor.findElement(protractor.By.nomenclature('model.addressType').text()).getText())
        .toEqual('Постоянен адрес');
      expect(ptor.findElement(protractor.By.nomenclature('model.valid').text()).getText())
        .toEqual('Не');
      expect(ptor.findElement(protractor.By.nomenclature('model.settlement').text()).getText())
        .toEqual('гр.Пловдив');

      ptor.findElement(protractor.By.input('model.address'))
        .getAttribute('value').then(function (text) {
        expect(text).toEqual('бул.Цариградско шосе 28 ет.9');
      });

      ptor.findElement(protractor.By.input('model.addressAlt'))
        .getAttribute('value').then(function (text) {
        expect(text).toEqual('bul.Tsarigradski shose 28 et.9');
      });
    });
   
    it('should change address data correctly', function() {
      ptor.findElement(protractor.By.nomenclature('model.addressType')).click();
      ptor.findElement(protractor.By.nomenclature('model.addressType').dropdownInput())
        .sendKeys('Седалище');
      ptor.findElement(protractor.By.nomenclature('model.addressType').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      ptor.findElement(protractor.By.nomenclature('model.valid')).click();
      ptor.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys('Да');
      ptor.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys(protractor.Key.ENTER);
      
      ptor.findElement(protractor.By.nomenclature('model.settlement')).click();
      ptor.findElement(protractor.By.nomenclature('model.settlement').dropdownInput())
        .sendKeys('София');
      ptor.findElement(protractor.By.nomenclature('model.settlement').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      var addressInput = ptor.findElement(protractor.By.input('model.address'));
      addressInput.clear();
      addressInput.sendKeys('ж.к. Драгалевци');

      var addressAltInput = ptor.findElement(protractor.By.input('model.addressAlt'));
      addressAltInput.clear();
      addressAltInput.sendKeys('j.k.Dragalevci');

      ptor.findElement(protractor.By.input('model.postalCode')).sendKeys('1000');
      ptor.findElement(protractor.By.input('model.phone')).sendKeys('0999212');

      ptor.findElement(protractor.By.name('saveBtn')).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/addresses');

      var dataPromise;
      ptor
        .findElements(protractor.By.datatable('addresses').column('part.addressType.name'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['Седалище', 'Адрес за кореспонденция']);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.settlement.name'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['София', 'гр.Пловдив']);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.address'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual([
            'ж.к. Драгалевци',
            'жг.Толстой бл.39 ап.40'
          ]);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.postalCode'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['1000', '']);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.phone'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['0999212', '']);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.valid.name'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['Да', 'Да']);
        });
    });
  });

} (protractor, describe, beforeEach, it, expect));