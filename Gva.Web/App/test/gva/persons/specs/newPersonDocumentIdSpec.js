/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person address new page', function() {
    var ptor = protractor.getInstance();

    beforeEach(function() {
      ptor.get('#/persons/1/documentIds/new');
    });

    it('should update breadcrumb text', function () {
      /*jshint quotmark:false */
      var text = ptor.findElement(protractor.By.xpath("//ul[@class='breadcrumb']/li[last()]"))
        .getText();
      expect(text).toEqual('Нов документ за самоличност');

    });

    it('should create new document id correctly', function() {
      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType')).click();
      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType').dropdownInput())
        .sendKeys('Задграничен паспорт');
      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      ptor.findElement(protractor.By.nomenclature('model.valid')).click();
      ptor.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys('Не');
      ptor.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      ptor.findElement(protractor.By.input('model.bookPageNumber')).sendKeys('5');
      ptor.findElement(protractor.By.input('model.pageCount')).sendKeys('3');
      ptor.findElement(protractor.By.input('model.documentNumber')).sendKeys('1000');
      ptor.findElement(protractor.By.css('textarea')).sendKeys('Записки...');
      ptor.findElement(protractor.By.input('model.documentPublisher')).sendKeys('МВР Бургас');
      ptor.findElement(protractor.By.css('div[name=documentDateValidFrom] > input'))
        .sendKeys('22.12.2014');
      ptor.findElement(protractor.By.css('div[name=documentDateValidTo] > input'))
        .sendKeys('01.08.2018');

      ptor.findElement(protractor.By.name('saveBtn')).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentIds');

      var dataPromise;
      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.personDocumentIdType.name'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['Лична карта', 'Задграничен паспорт']);
        });

      ptor
        .findElements(protractor.By.datatable('documentIds').column('part.documentNumber'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['6765432123', '1000']);
        });

      ptor
        .findElements(protractor.By.datatable('documentIds').column('part.documentDateValidFrom'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['04.04.2010', '22.12.2014']);
        });

      ptor
        .findElements(protractor.By.datatable('documentIds').column('part.documentDateValidTo'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['04.04.2020', '01.08.2018']);
        });

      ptor
        .findElements(protractor.By.datatable('documentIds').column('part.valid.name'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['Да', 'Не']);
        });

      ptor
        .findElements(protractor.By.datatable('documentIds').column('part.bookPageNumber'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['3', '5']);
        });

      ptor
        .findElements(protractor.By.datatable('documentIds').column('part.pageCount'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['1', '3']);
        });
    });
    
    it('should disable save button unless all required fields are filled out', function() {
      expect(ptor.isElementPresent(protractor.By.css('button[disabled=disabled]')))
        .toEqual(true);

      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType')).click();
      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType').dropdownInput())
        .sendKeys('Задграничен паспорт');
      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      ptor.findElement(protractor.By.nomenclature('model.valid')).click();
      ptor.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys('Не');
      ptor.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      expect(ptor.isElementPresent(protractor.By.css('button[disabled=disabled]')))
        .toEqual(true);

      ptor.findElement(protractor.By.input('model.bookPageNumber')).sendKeys('5');
      ptor.findElement(protractor.By.input('model.documentNumber')).sendKeys('1000');

      expect(ptor.isElementPresent(protractor.By.css('button[disabled=disabled]')))
        .toEqual(true);

      ptor.findElement(protractor.By.input('model.documentPublisher')).sendKeys('МВР Бургас');
      ptor.findElement(protractor.By.css('div[name=documentDateValidFrom] > input'))
        .sendKeys('22.12.2014');
      ptor.findElement(protractor.By.css('div[name=documentDateValidTo] > input'))
        .sendKeys('01.08.2018');

      expect(ptor.isElementPresent(protractor.By.css('button[disabled=disabled]')))
        .toEqual(false);

    });

    it('should go to search view at clicking on cancel button', function () {
      ptor.findElement(protractor.By.name('cancelBtn'))
        .click().then(function () {
            ptor.getCurrentUrl().then(function (url) {
              expect(url).toEqual('http://localhost:52560/#/persons/1/documentIds');
            });
          });
    });
  });

} (protractor, describe, beforeEach, it, expect));