/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document Ids edit page', function () {
    var ptor = protractor.getInstance(),
       Page = require('../pageObjects/editDocumentIdPO'),
       SearchPage = require('../pageObjects/searchDocumentIdPO'),
       editPersonDocumentIdPage,
       searchPersonDocumentIdPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentIds/10');
      editPersonDocumentIdPage = new Page(ptor);
    });
    
    it('should update breadcrumb text', function () {
      expect(editPersonDocumentIdPage.breadcrumb.getText())
        .toEqual('Редакция на документ за самоличност');
    });

    it('should display correct filled out data', function () {
      expect(editPersonDocumentIdPage.personDocumentIdType.get()).toEqual('Лична карта');
      expect(editPersonDocumentIdPage.valid.get()).toEqual('Да');
      expect(editPersonDocumentIdPage.bookPageNumber.get()).toEqual('3');
      expect(editPersonDocumentIdPage.pageCount.get()).toEqual('1');
      expect(editPersonDocumentIdPage.documentNumber.get()).toEqual('6765432123');
      expect(editPersonDocumentIdPage.documentPublisher.get()).toEqual('МВР София');
      expect(editPersonDocumentIdPage.fileSpan.getText()).toEqual('testName.pdf');
      expect(editPersonDocumentIdPage.documentDateValidFrom.get()).toEqual('04.04.2010');
      expect(editPersonDocumentIdPage.documentDateValidTo.get()).toEqual('04.04.2020');
      expect(editPersonDocumentIdPage.datatable.getColumns(
          'applicationId',
          'applicationName'
          )).toEqual([
          ['1', 'application1'],
          ['2', 'application2']
        ]);
    });
   
    it('should change document id data correctly', function () {
      editPersonDocumentIdPage.personDocumentIdType.set('Задграничен паспорт');
      editPersonDocumentIdPage.documentNumber.set('1000');
      editPersonDocumentIdPage.documentDateValidFrom.set('22.12.2014');
      editPersonDocumentIdPage.documentPublisher.set('МВР Бургас');
      editPersonDocumentIdPage.pageCount.set('5');
      editPersonDocumentIdPage.bookPageNumber.set('3');
      editPersonDocumentIdPage.valid.set('Не');
      editPersonDocumentIdPage.documentDateValidTo.set('01.08.2018');

      editPersonDocumentIdPage.save();

      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentIds');
      searchPersonDocumentIdPage = new SearchPage(ptor);

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
          ['Задграничен паспорт', '1000', '22.12.2014',
            '01.08.2018', 'МВР Бургас', 'Не', '3', '5', 'testName.pdf']
        ]);
    });
  });

} (protractor, describe, beforeEach, it, expect));