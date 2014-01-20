/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person address edit page', function() {
    var ptor = protractor.getInstance();

    beforeEach(function() {
      ptor.get('#/persons/1/documentIds/10');
    });

    it('should update breadcrumb text', function () {
      /*jshint quotmark:false */
      var text = ptor.findElement(protractor.By.xpath("//ul[@class='breadcrumb']/li[last()]"))
        .getText();
      expect(text).toEqual('Редакция на документ за самоличност');
    });

    it('should display correct filled out data', function () {
      expect(
        ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType').text())
        .getText())
        .toEqual('Лична карта');

      expect(ptor.findElement(protractor.By.nomenclature('model.valid').text()).getText())
        .toEqual('Да');

      ptor.findElement(protractor.By.input('model.bookPageNumber'))
        .getAttribute('value').then(function (text) {
          expect(text).toEqual('3');
        });

      ptor.findElement(protractor.By.input('model.pageCount'))
        .getAttribute('value').then(function (text) {
          expect(text).toEqual('1');
        });

      ptor.findElement(protractor.By.input('model.documentNumber'))
        .getAttribute('value').then(function (text) {
          expect(text).toEqual('6765432123');
        });

      ptor.findElement(protractor.By.css('div[name=documentDateValidFrom] > input'))
        .getAttribute('value').then(function (text) {
          expect(text).toEqual('04.04.2010');
        });
      
      ptor.findElement(protractor.By.css('div[name=documentDateValidTo] > input'))
        .getAttribute('value').then(function (text) {
          expect(text).toEqual('04.04.2020');
        });
      
      ptor.findElement(protractor.By.input('model.documentPublisher'))
        .getAttribute('value').then(function (text) {
          expect(text).toEqual('МВР София');
        });
      
      ptor.findElement(protractor.By.className('test-single-file-span'))
        .getText().then(function (text) {
          expect(text).toEqual('testName.pdf');
        });
      var dataPromise;
      ptor
        .findElements(protractor.By.datatable('model').column('applicationId'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['1', '2']);
        });

      ptor
        .findElements(protractor.By.datatable('model').column('applicationName'))
        .then(function (elements) {
          dataPromise = protractor.promise.fullyResolved(elements.map(function (el) {
            return el.getText();
          }));
 
          expect(dataPromise).toEqual(['application1', 'application2']);
        });
    });
   
    it('should change document id data correctly', function() {
      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType')).click();
      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType').dropdownInput())
        .sendKeys('Паспорт');
      ptor.findElement(protractor.By.nomenclature('model.personDocumentIdType').dropdownInput())
        .sendKeys(protractor.Key.ENTER);

      ptor.findElement(protractor.By.nomenclature('model.valid')).click();
      ptor.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys('Не');
      ptor.findElement(protractor.By.nomenclature('model.valid').dropdownInput())
        .sendKeys(protractor.Key.ENTER);
      

      var pageNumberInput = ptor.findElement(protractor.By.input('model.bookPageNumber'));
      pageNumberInput.clear();
      pageNumberInput.sendKeys('5');

      var pageCountInput = ptor.findElement(protractor.By.input('model.pageCount'));
      pageCountInput.clear();
      pageCountInput.sendKeys('3');

      var documentNumberInput = ptor.findElement(protractor.By.input('model.documentNumber'));
      documentNumberInput.clear();
      documentNumberInput.sendKeys('1000');

      var notesTextarea = ptor.findElement(protractor.By.css('textarea'));
      notesTextarea.clear();
      notesTextarea.sendKeys('Записки...');

      var publisherInput = ptor.findElement(protractor.By.input('model.documentPublisher'));
      publisherInput.clear();
      publisherInput.sendKeys('МВР Бургас');

      var documentDateValidFromInput =
        ptor.findElement(protractor.By.css('div[name=documentDateValidFrom] > input'));
      documentDateValidFromInput.clear();
      documentDateValidFromInput.sendKeys('22.12.2014');

      var documentDateValidToInput =
        ptor.findElement(protractor.By.css('div[name=documentDateValidTo] > input'));
      documentDateValidToInput.clear();
      documentDateValidToInput.sendKeys('01.08.2018');

      ptor.findElement(protractor.By.name('saveBtn')).click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentIds');

      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.docTypeId'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['Паспорт']);
          }));
        });

      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.valid'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['Не']);
          }));
        });

      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.documentNumber'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['1000']);
          }));
        });

      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.documentDateValidFrom'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['22.12.2014']);
          }));
        });
      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.documentDateValidTo'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['01.08.2018']);
          }));
        });

      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.bookPageNumber'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['5']);
          }));
        });
      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.pageCount'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['3']);
          }));
        });
      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.notes'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['Записки...']);
          }));
        });

      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.documentPublisher'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['МВР Бургас']);
          }));
        });

      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.file'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['testName.pdf', '']);
          }));
        });
    });
  });

} (protractor, describe, beforeEach, it, expect));