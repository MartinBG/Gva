/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document other edit page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentOthers/docOtherPO'),
        SearchPage = require('../../pageObjects/documentOthers/searchDocOtherPO'),
        editDocOtherPage,
        searchDocOtherPage;

    beforeEach(function() {
      ptor.get('#/persons/1/documentOthers/13');
      editDocOtherPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editDocOtherPage.breadcrumb.get()).toEqual('Редакция на документ');
    });

    it('should display correct filled out data', function () {
      expect(editDocOtherPage.documentNumber.get()).toEqual('1221');
      expect(editDocOtherPage.documentPersonNumber.get()).toEqual('1221');
      expect(editDocOtherPage.documentPublisher.get())
        .toEqual('УЦ: Български въздухоплавателен център');
      expect(editDocOtherPage.documentDateValidFrom.get()).toEqual('06.03.2010');
      expect(editDocOtherPage.documentDateValidTo.get()).toEqual('06.03.2013');
      expect(editDocOtherPage.personOtherDocumentType.get()).toEqual('Протокол');
      expect(editDocOtherPage.personOtherDocumentRole.get()).toEqual('Летателна проверка');
      expect(editDocOtherPage.bookPageNumber.get()).toEqual('62');
      expect(editDocOtherPage.pageCount.get()).toEqual('1');
    });
    
    it('should change document data correctly', function () {
      editDocOtherPage.documentNumber.set('9800');
      editDocOtherPage.documentDateValidFrom.set('01.01.2010');
      editDocOtherPage.documentDateValidTo.set('01.01.2020');
      editDocOtherPage.bookPageNumber.set('1');
      editDocOtherPage.pageCount.set('2');

      editDocOtherPage.save();
      searchDocOtherPage = new SearchPage(ptor);
      expect(searchDocOtherPage.breadcrumb.get()).toEqual('Други документи');

      expect(searchDocOtherPage.datatable.getColumns(
          'part_documentNumber',
          'part_documentDateValidFrom',
          'part_documentDateValidTo',
          'part_documentPublisher',
          'part_bookPageNumber',
          'part_pageCount'
          )).toEqual([
        ['9800', '01.01.2010', '01.01.2020',
          'УЦ: Български въздухоплавателен център', '1', '2']
      ]);
    });
  });

} (protractor, describe, beforeEach, it, expect));