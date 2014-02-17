/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person document check edit page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentChecks/checkPO'),
        SearchPage = require('../../pageObjects/documentChecks/searchCheckPO'),
        editCheckPage,
        searchCheckPage;

    beforeEach(function () {
      ptor.get('#/persons/1/checks/20');
      editCheckPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editCheckPage.breadcrumb.getText()).toEqual('Редакция на проверка');
    });

    it('should display correct filled out data', function () {
      expect(editCheckPage.staffType.get()).toEqual('Наземен авиационен персонал за TO на ВС');
      expect(editCheckPage.documentNumber.get()).toEqual('11232');
      expect(editCheckPage.documentPersonNumber.get()).toEqual('7005159385');
      expect(editCheckPage.personCheckDocumentType.get()).toEqual('Base training form');
      expect(editCheckPage.personCheckDocumentRole.get()).toEqual('Летателна проверка');
      expect(editCheckPage.documentDateValidFrom.get()).toEqual('02.05.1970');
      expect(editCheckPage.documentDateValidTo.get()).toEqual('15.05.1970');
      expect(editCheckPage.documentPublisher.get()).toEqual('Проверяващ');
      expect(editCheckPage.valid.get()).toEqual('Да');
      expect(editCheckPage.bookPageNumber.get()).toEqual('3');
      expect(editCheckPage.pageCount.get()).toEqual('5');
    });

    it('should change address data correctly', function () {
      editCheckPage.personCheckDocumentType.set('Описание');
      editCheckPage.personCheckDocumentRole.set('Тренажор');
      editCheckPage.valid.set('Не');
      editCheckPage.bookPageNumber.set('123');
      editCheckPage.pageCount.set('456');

      editCheckPage.save();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/checks');
      searchCheckPage = new SearchPage(ptor);

      expect(searchCheckPage.datatable.getColumns(
          'part_personCheckDocumentType_name',
          'part_personCheckDocumentRole_name',
          'part_valid_name',
          'part_bookPageNumber',
          'part_pageCount'
          )).toEqual([
          ['Описание', 'Тренажор', 'Не', '123', '456'],
          ['Authorisation', 'Практическа проверка', 'Не', '2', '6']
        ]);
    });
  });

}(protractor, describe, beforeEach, it, expect));