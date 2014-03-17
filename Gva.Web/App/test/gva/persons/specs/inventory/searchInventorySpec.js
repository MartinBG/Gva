/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person inventory search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/inventory/inventoryPO'),
        SearchPage = require('../../pageObjects/documentEducations/searchEducationPO'),
        inventoryPage,
        searchDocEduPage;

    beforeEach(function () {
      ptor.get('#/persons/1/inventory');
      inventoryPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(inventoryPage.breadcrumb.get()).toEqual('Опис');
    });

    it('should display data correctly', function () {
      expect(inventoryPage.datatable.getColumns(
          'bookPageNumber',
          'name',
          'type',
          'number',
          'date',
          'publisher',
          'valid',
          'fromDate',
          'toDate',
          'pageCount'
          )).toEqual([
        ['2', 'Образование', 'Висше образование (бакалавър)', '1', '04.04.1981',
          'Български въздухоплавателен център', '', '', '', '1'],
        ['3', 'Документ за самоличност', 'Лична карта', '6765432123', '04.04.2010', 'МВР София',
          'Да', '04.04.2010', '04.04.2020', '1'],
        ['3', 'Документ за самоличност', 'Лична карта', '6765432123', '04.04.2010', 'МВР София',
          'Да', '04.04.2010', '04.04.2020', '1'],
        ['3', 'Документ за самоличност', 'Лична карта', '6765432123', '04.04.2010', 'МВР София',
          'Да', '04.04.2010', '04.04.2020', '1'],
        ['87', 'Обучение', 'Контролен талон', 'BG FCL/CPA-00001-11232', '27.02.2013',
          'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', 'Да',
          '27.02.2013', '', '1'],
        ['81', 'Обучение', 'Контролен талон', 'BG CPA 00185-11232', '12.03.2012',
          'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', 'Да',
          '12.03.2012', '02.03.2016', '1'],
        ['79', 'Обучение', 'Контролна карта', '5-6448', '12.03.2012',
          'Инспектор: Георги Мишев Христов Код:46', 'Не', '12.03.2012', '', '1'],
        ['79', 'Теоретично обучение', 'Свидетелство', '80', '10.03.2012', 'УЦ: Ратан',
          'Да', '10.03.2012', '', '1'],
        ['1', 'Медицинско свидетелство', '', 'MED BG-1-11232-99994', '04.04.2010',
          '[object Object]', '', '04.04.2010', '04.08.2010', '3'],
        ['3', 'Медицинско свидетелство', '', 'MED BG2-3244-11232-9934', '04.04.2005',
          '[object Object]', '', '04.04.2005', '06.09.2015', '5']
      ]);
    });

    it('should go to edit page', function () {
      inventoryPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[0].click();
        searchDocEduPage = new SearchPage(ptor);
        expect(searchDocEduPage.breadcrumb.get()).toEqual('Образования');
      });
    });
  });

} (protractor, describe, beforeEach, it, expect));