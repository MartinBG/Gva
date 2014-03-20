/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document other new page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentOthers/docOtherPO'),
        SearchPage = require('../../pageObjects/documentOthers/searchDocOtherPO'),
        newDocOtherPage,
        searchDocOtherPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentOthers/new');
      newDocOtherPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newDocOtherPage.breadcrumb.get()).toEqual('Нов документ');
    });

    it('should create new document correctly', function () {
      newDocOtherPage.documentNumber.set('1');
      newDocOtherPage.documentPersonNumber.set('2324a');
      newDocOtherPage.documentDateValidFrom.set('20.10.2014');
      newDocOtherPage.documentDateValidTo.set('01.01.2020');
      newDocOtherPage.documentPublisher.set('AAK Progres');
      newDocOtherPage.personOtherDocumentType.set('Протокол');
      newDocOtherPage.personOtherDocumentRole.set('Нарушение');
      newDocOtherPage.notes.set('notes');

      newDocOtherPage.save();
      searchDocOtherPage = new SearchPage(ptor);
      expect(searchDocOtherPage.breadcrumb.get()).toEqual('Други документи');

      expect(searchDocOtherPage.datatable.getColumns(
          'part_documentNumber',
          'part_documentDateValidFrom',
          'part_documentDateValidTo',
          'part_documentPublisher'
          )).toEqual([
        ['1221', '06.03.2010', '06.03.2013', 'УЦ: Български въздухоплавателен център'],
        ['1', '20.10.2014', '01.01.2020', 'AAK Progres']
      ]);
    });

    it('should go to search view at clicking on cancel button', function () {
      newDocOtherPage.cancel();
      searchDocOtherPage = new SearchPage(ptor);
      expect(searchDocOtherPage.breadcrumb.get()).toEqual('Други документи');
    });
  });

} (protractor, describe, beforeEach, it, expect));