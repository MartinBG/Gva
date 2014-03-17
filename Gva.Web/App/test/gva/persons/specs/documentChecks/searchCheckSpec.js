/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person document check search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentChecks/searchCheckPO'),
        EditPage = require('../../pageObjects/documentChecks/checkPO'),
        personChecksPage,
        editCheckPage;

    beforeEach(function () {
      ptor.get('#/persons/1/checks');
      personChecksPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personChecksPage.breadcrumb.get()).toEqual('Проверки');
    });

    it('should display data correctly', function () {
      expect(personChecksPage.datatable.getColumns(
          'part_personCheckDocumentType_name',
          'part_personCheckDocumentRole_name',
          'part_documentDateValidFrom',
          'part_documentPublisher',
          'part_valid_name',
          'part_bookPageNumber'
          )).toEqual([
          ['Base training form', 'Летателна проверка', '02.05.1970', 'Проверяващ', 'Да', '3'],
          ['Authorisation', 'Практическа проверка', '02.05.2000', 'МВР Бургас', 'Не', '2']
        ]);
    });

    it('should delete an address', function () {
      personChecksPage.firstDeleteBtn.click();
      personChecksPage = new Page(ptor);
      expect(personChecksPage.datatable.getColumns(
          'part_personCheckDocumentType_name',
          'part_personCheckDocumentRole_name',
          'part_documentDateValidFrom',
          'part_documentPublisher',
          'part_valid_name',
          'part_bookPageNumber'
          )).toEqual([
          ['Authorisation', 'Практическа проверка', '02.05.2000', 'МВР Бургас', 'Не', '2']
        ]);
    });

    it('should go to edit page', function () {
      personChecksPage.firstEditBtn.click();
      editCheckPage = new EditPage(ptor);
      expect(editCheckPage.breadcrumb.get()).toEqual('Редакция на проверка');
    });
  });

}(protractor, describe, beforeEach, it, expect));