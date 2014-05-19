/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person document check new page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentChecks/newCheckPO'),
        SearchPage = require('../../pageObjects/documentChecks/searchCheckPO'),
        newCheckPage,
        searchCheckPage;

    beforeEach(function () {
      ptor.get('#/persons/1/checks/new');
      newCheckPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newCheckPage.breadcrumb.get()).toEqual('Нова проверка');
    });

    it('should create new check correctly', function () {
      newCheckPage.chooseStaffType('Членове на екипажа').then(function () {
        newCheckPage.personCheckDocumentType.set('Протокол');
        newCheckPage.personCheckDocumentRole.set('Тренажор');
        newCheckPage.documentDateValidFrom.set('12.10.2013');
        newCheckPage.documentPublisher.set('алабала');
        newCheckPage.valid.set('Да');

        newCheckPage.save();
        
        searchCheckPage = new SearchPage(ptor);
        
        expect(searchCheckPage.breadcrumb.get()).toEqual('Проверки');

        expect(searchCheckPage.datatable.getColumns(
          'part_documentType_name',
          'part_documentRole_name',
          'part_documentDateValidFrom',
          'part_documentPublisher',
          'part_valid_name'
        )).toEqual([
          ['Свидетелство', 'Летателна проверка', '02.05.1970', 'Проверяващ', 'Да'],
          ['Справка', 'Практическа проверка', '02.05.2000', 'МВР Бургас', 'Не'],
          ['Протокол', 'Тренажор', '12.10.2013', 'алабала', 'Да']
        ]);
      });
    });

    it('should go to search view at clicking on cancel button', function () {
      newCheckPage.cancel();
      
      searchCheckPage = new SearchPage(ptor);
      
      expect(searchCheckPage.breadcrumb.get()).toEqual('Проверки');
    });
  });

}(protractor, describe, beforeEach, it, expect));