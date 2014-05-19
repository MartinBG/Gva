/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document Ids edit page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../../pageObjects/documentIds/documentIdPO'),
       SearchPage = require('../../pageObjects/documentIds/searchDocumentIdPO'),
       editPersonDocumentIdPage,
       searchPersonDocumentIdPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentIds/10');
      editPersonDocumentIdPage = new Page(ptor);
    });
    
    it('should update breadcrumb text', function () {
      expect(editPersonDocumentIdPage.breadcrumb.get())
        .toEqual('Редакция на документ за самоличност');
    });

    it('should display correct filled out data', function () {
      expect(editPersonDocumentIdPage.personDocumentIdType.get()).toEqual('Лична карта');
      expect(editPersonDocumentIdPage.valid.get()).toEqual('Да');
      expect(editPersonDocumentIdPage.documentPublisher.get()).toEqual('МВР София');
      expect(editPersonDocumentIdPage.documentDateValidFrom.get()).toEqual('04.04.2010');
      expect(editPersonDocumentIdPage.documentDateValidTo.get()).toEqual('04.04.2020');
    });
   
    it('should change document id data correctly', function () {
      editPersonDocumentIdPage.personDocumentIdType.set('Задграничен паспорт');
      editPersonDocumentIdPage.documentNumber.set('1000');
      editPersonDocumentIdPage.documentDateValidFrom.set('22.12.2014');
      editPersonDocumentIdPage.documentPublisher.set('МВР Бургас');
      editPersonDocumentIdPage.valid.set('Не');
      editPersonDocumentIdPage.documentDateValidTo.set('01.08.2018');

      editPersonDocumentIdPage.save();

      searchPersonDocumentIdPage = new SearchPage(ptor);
      expect(searchPersonDocumentIdPage.breadcrumb.get()).toEqual('Документи за самоличност');

      expect(searchPersonDocumentIdPage.datatable.getColumns(
          'part_documentType_name',
          'part_documentNumber',
          'part_documentDateValidFrom',
          'part.documentDateValidTo',
          'part_documentPublisher',
          'part_valid_name'
          )).toEqual([
          ['Задграничен паспорт', '1000', '22.12.2014', '01.08.2018', 'МВР Бургас', 'Не'],
          ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да'],
          ['Лична карта', '6765432123', '04.04.2010', '04.04.2020', 'МВР София', 'Да']
        ]);
    });
  });

} (protractor, describe, beforeEach, it, expect));