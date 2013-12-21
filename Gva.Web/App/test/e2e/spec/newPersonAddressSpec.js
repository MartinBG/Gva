/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person address new page', function() {
    var ptor = protractor.getInstance();

    beforeEach(function() {
      ptor.get('#/persons/1/addresses/new');
    });

    it('should update breadcrumb text', function () {
      /*jshint quotmark:false */
      var text = ptor.findElement(protractor.By.xpath("//ul[@class='breadcrumb']/li[last()]"))
        .getText();
      expect(text).toEqual('Нов адрес');

    });

    it('should create new address correctly', function() {
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

      ptor.findElement(protractor.By.input('model.address')).sendKeys('ж.к. Драгалевци');
      ptor.findElement(protractor.By.input('model.addressAlt')).sendKeys('j.k.Dragalevci');
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
 
          expect(dataPromise).toEqual(['Постоянен адрес', 'Адрес за кореспонденция', 'Седалище']);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.settlement.name'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['гр.Пловдив', 'гр.Пловдив', 'София']);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.address'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual([
            'бул.Цариградско шосе 28 ет.9',
            'жг.Толстой бл.39 ап.40',
            'ж.к. Драгалевци'
          ]);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.postalCode'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['', '', '1000']);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.phone'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['', '', '0999212']);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.valid.name'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['Не', 'Да', 'Да']);
        });
    });

    it('should disable save button unless all required fields are filled out', function() {
      expect(ptor.isElementPresent(protractor.By.css('button[disabled=disabled]')))
        .toEqual(true);

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

      expect(ptor.isElementPresent(protractor.By.css('button[disabled=disabled]')))
        .toEqual(true);

      ptor.findElement(protractor.By.input('model.address')).sendKeys('ж.к. Драгалевци');
      ptor.findElement(protractor.By.input('model.addressAlt')).sendKeys('j.k.Dragalevci');

      expect(ptor.isElementPresent(protractor.By.css('button[disabled=disabled]')))
        .toEqual(false);
    });

    it('should go to search view at clicking on cancel button', function () {
      ptor.findElement(protractor.By.name('cancelBtn'))
        .click().then(function () {
            ptor.getCurrentUrl().then(function (url) {
              expect(url).toEqual('http://localhost:52560/#/persons/1/addresses');
            });
          });
    });
  });

} (protractor, describe, beforeEach, it, expect));