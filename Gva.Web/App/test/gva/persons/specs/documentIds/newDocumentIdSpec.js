/*global protractor, describe, beforeEach, it, expect, require, xit*/
(function (protractor, describe, beforeEach, it, expect, xit) {
  'use strict';

  describe('Person document Ids new page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../../pageObjects/documentIds/documentIdPO'),
       SearchPage = require('../../pageObjects/documentIds/searchDocumentIdPO'),
       newPersonDocumentIdPage,
       searchPersonDocumentIdPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentIds/new');
      newPersonDocumentIdPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newPersonDocumentIdPage.breadcrumb.get())
        .toEqual('Нов документ за самоличност');
    });

    it('should create new document id correctly', function () {
      newPersonDocumentIdPage.personDocumentIdType.set('Задграничен паспорт');
      newPersonDocumentIdPage.documentNumber.set('1000');
      newPersonDocumentIdPage.documentDateValidFrom.set('22.12.2014');
      newPersonDocumentIdPage.documentPublisher.set('МВР Бургас');
      newPersonDocumentIdPage.pageCount.set('5');
      newPersonDocumentIdPage.bookPageNumber.set('3');
      newPersonDocumentIdPage.valid.set('Не');
      newPersonDocumentIdPage.documentDateValidTo.set('01.08.2018');
      newPersonDocumentIdPage.save();

      searchPersonDocumentIdPage = new SearchPage(ptor);
      expect(searchPersonDocumentIdPage.breadcrumb.get()).toEqual('Документи за самоличност');

      expect(searchPersonDocumentIdPage.datatable.getColumns(
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
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да', '3', '1'],
        ['Задграничен паспорт', '1000', '22.12.2014', '01.08.2018', 'МВР Бургас', 'Не', '3', '5']
      ]);
    });

    xit('should disable save button unless all required fields are filled out', function() {
      expect(newPersonDocumentIdPage.isSaveBtnDisabled).toEqual(true);

      newPersonDocumentIdPage.personDocumentIdType.set('Паспорт');
      newPersonDocumentIdPage.documentNumber.set('1000');
      newPersonDocumentIdPage.documentPublisher.set('МВР Бургас');
      newPersonDocumentIdPage.valid.set('Не');

      newPersonDocumentIdPage = new Page(ptor);
      expect(newPersonDocumentIdPage.isSaveBtnDisabled).toEqual(true);

      newPersonDocumentIdPage.pageCount.set('5');
      newPersonDocumentIdPage.bookPageNumber.set('3');

      newPersonDocumentIdPage = new Page(ptor);
      expect(newPersonDocumentIdPage.isSaveBtnDisabled).toEqual(true);

      newPersonDocumentIdPage.documentDateValidFrom.set('22.12.2011');
      newPersonDocumentIdPage.documentDateValidTo.set('01.08.2012');

      newPersonDocumentIdPage = new Page(ptor);
      expect(newPersonDocumentIdPage.isSaveBtnDisabled).toEqual(false);

    });

    it('should go to search view at clicking on cancel button', function () {
      newPersonDocumentIdPage.cancel();
      searchPersonDocumentIdPage = new SearchPage(ptor);
      expect(searchPersonDocumentIdPage.breadcrumb.get()).toEqual('Документи за самоличност');
    });
  });

} (protractor, describe, beforeEach, it, expect, xit));
