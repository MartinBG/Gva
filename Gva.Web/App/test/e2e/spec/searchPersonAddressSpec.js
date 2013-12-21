/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person address search page', function() {
    var ptor = protractor.getInstance();

    beforeEach(function() {
      ptor.get('#/persons/1/addresses');
    });

    it('should update breadcrumb text', function () {
      /*jshint quotmark:false */
      var text = ptor.findElement(protractor.By.xpath("//ul[@class='breadcrumb']/li[last()]"))
        .getText();
      expect(text).toEqual('Адреси');
    });

    it('should display data correctly', function() {
      var dataPromise;
      ptor
        .findElements(protractor.By.datatable('addresses').column('part.addressType.name'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['Постоянен адрес', 'Адрес за кореспонденция']);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.settlement.name'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['гр.Пловдив', 'гр.Пловдив']);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.address'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['бул.Цариградско шосе 28 ет.9', 'жг.Толстой бл.39 ап.40']);
        });

      ptor
        .findElements(protractor.By.datatable('addresses').column('part.valid.name'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['Не', 'Да']);
        });
    });

    it('should delete an address', function() {
      var btnSelector = 'tbody tr:first-child td:nth-child(8)';
      ptor.findElement(protractor.By.css(btnSelector))
        .click();
      ptor
      .findElements(protractor.By.datatable('addresses').column('part.addressType.name'))
      .then(function (elements) {
        var dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
          return el.getText();
        }));
 
        expect(dataPromise).toEqual(['Адрес за кореспонденция']);
      });
    });

    it('should go to edit page', function() {
      var btnSelector = 'tbody tr:first-child td:nth-child(7)';
      ptor.findElement(protractor.By.css(btnSelector))
        .click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/addresses/2');
    });
  });

} (protractor, describe, beforeEach, it, expect));