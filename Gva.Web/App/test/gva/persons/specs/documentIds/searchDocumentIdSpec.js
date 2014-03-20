/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person document Ids search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentIds/searchDocumentIdPO'),
        EditPage = require('../../pageObjects/documentIds/documentIdPO'),
        personDocumentIdsPage,
        editDocumentIdsPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentIds');
      personDocumentIdsPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personDocumentIdsPage.breadcrumb.get()).toEqual('Документи за самоличност');
    });

    it('should display data correctly', function () {
      expect(personDocumentIdsPage.datatable.getColumns(
          'part_documentType_name',
          'part_documentNumber',
          'part_documentDateValidFrom',
          'part.documentDateValidTo',
          'part_documentPublisher',
          'part_valid_name'
          )).toEqual([
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да'],
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да'],
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да']
      ]);
    });

    it('should delete a documentId', function () {
      personDocumentIdsPage.firstDeleteBtn.click();
      personDocumentIdsPage = new Page(ptor);
      expect(personDocumentIdsPage.datatable.getColumns(
          'part_documentType_name',
          'part_documentNumber',
          'part_documentDateValidFrom',
          'part.documentDateValidTo',
          'part_documentPublisher',
          'part_valid_name'
          )).toEqual([
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да'],
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да']
      ]);
    });

    it('should go to edit page', function () {
      personDocumentIdsPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[0].click();
        editDocumentIdsPage = new EditPage(ptor);
        expect(editDocumentIdsPage.breadcrumb.get()).toEqual('Редакция на документ за самоличност');
      });
    });
  });

} (protractor, describe, beforeEach, it, expect));
