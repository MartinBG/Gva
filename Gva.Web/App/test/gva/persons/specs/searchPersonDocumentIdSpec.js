/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document Ids search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/searchDocumentIdPO'),
        personDocumentIdsPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentIds');
      personDocumentIdsPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personDocumentIdsPage.breadcrumb.getText()).toEqual('Документи за самоличност');
    });

    it('should display data correctly', function () {
      expect(personDocumentIdsPage.datatable.getColumns(
          'part_personDocumentIdType_name',
          'part_documentNumber',
          'part_documentDateValidFrom',
          'part.documentDateValidTo',
          'part_documentPublisher',
          'part_valid_name',
          'part_bookPageNumber',
          'part_pageCount',
          'file'
          )).toEqual([
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да', '3', '1'],
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да', '3', '1'],
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да', '3', '1']
      ]);
    });
   
    it('should delete a documentId', function () {
      personDocumentIdsPage.firstDeleteBtn.click();
      personDocumentIdsPage = new Page(ptor);
      expect(personDocumentIdsPage.datatable.getColumns(
          'part_personDocumentIdType_name',
          'part_documentNumber',
          'part_documentDateValidFrom',
          'part.documentDateValidTo',
          'part_documentPublisher',
          'part_valid_name',
          'part_bookPageNumber',
          'part_pageCount',
          'file'
          )).toEqual([
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да', '3', '1'],
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да', '3', '1']
      ]);
    });

    it('should go to edit page', function () {
      personDocumentIdsPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[0].click();
        expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentIds/10');
      });
    });
  });

} (protractor, describe, beforeEach, it, expect));