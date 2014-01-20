/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person address search page', function() {
    var ptor = protractor.getInstance();

    beforeEach(function() {
      ptor.get('#/persons/1/documentIds');
    });

    it('should update breadcrumb text', function () {
      /*jshint quotmark:false */
      var text = ptor.findElement(protractor.By.xpath("//ul[@class='breadcrumb']/li[last()]"))
        .getText();
      expect(text).toEqual('Документи за самоличност');
    });

    it('should display data correctly', function() {
      ptor
         .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.docTypeId'))
          .then(function (elements) {
            protractor.promise.fullyResolved(elements.map(function (el) {
                expect(el.getText()).toEqual(['Лична карта']);
              }));
          });

      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.documentNumber'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['6765432123']);
          }));
        });

      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.documentPublisher'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['МВР София']);
          }));
        });

      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.documentDateValidFrom'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['04.04.2010']);
          }));
        });
      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.documentDateValidTo'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['04.04.2020']);
          }));
        });
      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.valid'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['Да']);
          }));
        });
      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.bookPageNumber'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['3']);
          }));
        });
      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.pageCount'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['1']);
          }));
        });
      ptor
        .findElements(protractor.By.datatable('documentIds')
        .column('part.documentIdSearch.file'))
        .then(function (elements) {
          protractor.promise.fullyResolved(elements.map(function (el) {
            expect(el.getText()).toEqual(['testName.pdf']);
          }));
        });
    });

    it('should delete a documentId', function() {
      var btnSelector = 'tbody tr:first-child td:nth-child(10)';
      ptor.findElement(protractor.By.css(btnSelector)).click();
      expect(ptor.findElement(protractor.By.css('td')).getText())
        .toEqual('Няма намерени резултати');
    });

    it('should go to edit page', function() {
      var btnSelector = 'tbody tr:first-child td:nth-child(9)';
      ptor.findElement(protractor.By.css(btnSelector))
        .click();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentIds/10');
    });
  });

} (protractor, describe, beforeEach, it, expect));