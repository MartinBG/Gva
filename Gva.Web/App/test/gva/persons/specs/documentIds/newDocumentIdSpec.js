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
      expect(newPersonDocumentIdPage.breadcrumb.getText())
        .toEqual('Нов документ за самоличност');
    });

    it('should create new document id correctly', function () {
      newPersonDocumentIdPage.personDocumentIdType.set('Задграничен паспорт');
      newPersonDocumentIdPage.documentNumber.set('1000');
      newPersonDocumentIdPage.documentDateValidFrom.set('22.12.2014');
      newPersonDocumentIdPage.documentPublisher.set('МВР Бургас');
      newPersonDocumentIdPage.valid.set('Не');
      newPersonDocumentIdPage.documentDateValidTo.set('01.08.2018');
      newPersonDocumentIdPage.save();

      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentIds');
      searchPersonDocumentIdPage = new SearchPage(ptor);

      expect(searchPersonDocumentIdPage.datatable.getColumns(
          'part_documentType_name',
          'part_documentNumber',
          'part_documentDateValidFrom',
          'part.documentDateValidTo',
          'part_documentPublisher',
          'part_valid_name'
          )).toEqual([
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да'],
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да'],
        ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да'],
        ['Задграничен паспорт', '1000', '22.12.2014', '01.08.2018', 'МВР Бургас', 'Не']
      ]);
    }, 15000);

    xit('should disable save button unless all required fields are filled out', function() {
      expect(newPersonDocumentIdPage.isSaveBtnDisabled).toEqual(true);

      newPersonDocumentIdPage.personDocumentIdType.set('Паспорт');
      newPersonDocumentIdPage.documentNumber.set('1000');
      newPersonDocumentIdPage.documentPublisher.set('МВР Бургас');
      newPersonDocumentIdPage.valid.set('Не');

      newPersonDocumentIdPage = new Page(ptor);
      expect(newPersonDocumentIdPage.isSaveBtnDisabled).toEqual(true);

      newPersonDocumentIdPage.documentDateValidFrom.set('22.12.2011');
      newPersonDocumentIdPage.documentDateValidTo.set('01.08.2012');

      newPersonDocumentIdPage = new Page(ptor);
      expect(newPersonDocumentIdPage.isSaveBtnDisabled).toEqual(false);

    });

    it('should go to search view at clicking on cancel button', function () {
      newPersonDocumentIdPage.cancel();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentIds');
    });
  });

} (protractor, describe, beforeEach, it, expect, xit));
