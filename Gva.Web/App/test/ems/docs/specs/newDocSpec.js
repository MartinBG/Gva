/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('New document page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../pageObjects/newDocumentPO'),
       SearchPage = require('../pageObjects/searchDocumentPO'),
       newDocumentPage,
       searchCorrPage;

    beforeEach(function () {
      ptor.get('#/docs/new');
      newDocumentPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newDocumentPage.breadcrumb.getText())
        .toEqual('Нов документ');
    });

    it('should go to docs search on cancel click', function () {
      newDocumentPage.cancel();

      searchCorrPage = new SearchPage(ptor);

      expect(searchCorrPage.breadcrumb.getText())
        .toEqual('Документи');
    });

    it('should create and register new document correctly', function () {
      newDocumentPage.docTypeGroup.set('Общи');
      newDocumentPage.docType.set('Резолюция');
      newDocumentPage.docSubject.set('Относно');

      newDocumentPage.register();

      searchCorrPage = new SearchPage(ptor);

      expect(searchCorrPage.breadcrumb.getText())
        .toEqual('Документи');

      expect(searchCorrPage.datatable.getColumns(
          'docSubject',
          'docDirectionName',
          'docStatusName'
          )).toEqual([
        ['Относно', 'Входящ', 'Чернова']
      ]);
    });

    it('should attach new document correctly', function () {
      ptor.get('#/docs/new?parentDocId=1');
      newDocumentPage = new Page(ptor);

      newDocumentPage.docTypeGroup.set('Общи');
      newDocumentPage.docType.set('Резолюция');
      newDocumentPage.docSubject.set('Относно');

      newDocumentPage.register();

      searchCorrPage = new SearchPage(ptor);

      expect(searchCorrPage.breadcrumb.getText())
        .toEqual('Документи');

      expect(searchCorrPage.datatable.getColumns(
          'docSubject',
          'docDirectionName',
          'docStatusName'
          )).toEqual([
        ['Относно', 'Входящ', 'Чернова']
      ]);
    });
  });

} (protractor, describe, beforeEach, it, expect));
