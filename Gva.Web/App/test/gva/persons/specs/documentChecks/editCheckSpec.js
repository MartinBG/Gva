/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person document check edit page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentChecks/editCheckPO'),
        SearchPage = require('../../pageObjects/documentChecks/searchCheckPO'),
        editCheckPage,
        searchCheckPage;

    beforeEach(function () {
      ptor.get('#/persons/1/checks/20');
      editCheckPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editCheckPage.breadcrumb.get()).toEqual('Редакция на проверка');
    });

    it('should display correct filled out data', function () {
      expect(editCheckPage.staffType.get()).toEqual('Наземен авиационен персонал за TO на ВС');
      expect(editCheckPage.documentNumber.get()).toEqual('11232');
      expect(editCheckPage.documentPersonNumber.get()).toEqual('7005159385');
      expect(editCheckPage.personCheckDocumentType.get()).toEqual('Свидетелство');
      expect(editCheckPage.personCheckDocumentRole.get()).toEqual('Летателна проверка');
      expect(editCheckPage.documentDateValidFrom.get()).toEqual('02.05.1970');
      expect(editCheckPage.documentDateValidTo.get()).toEqual('15.05.1970');
      expect(editCheckPage.documentPublisher.get()).toEqual('Проверяващ');
      expect(editCheckPage.valid.get()).toEqual('Да');
    });

    it('should change check data correctly', function () {
      editCheckPage.personCheckDocumentType.set('Писмо');
      editCheckPage.personCheckDocumentRole.set('Тренажор');
      editCheckPage.valid.set('Не');

      editCheckPage.save();

      searchCheckPage = new SearchPage(ptor);
      expect(searchCheckPage.breadcrumb.get()).toEqual('Проверки');

      expect(searchCheckPage.datatable.getColumns(
          'part_documentType_name',
          'part_documentRole_name',
          'part_valid_name'
          )).toEqual([
          ['Писмо', 'Тренажор', 'Не'],
          ['Справка', 'Практическа проверка', 'Не']
        ]);
    });
  });

}(protractor, describe, beforeEach, it, expect));