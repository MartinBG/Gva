/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person inventory search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/inventory/inventoryPO'),
        inventoryPage;

    beforeEach(function () {
      ptor.get('#/persons/1/inventory');
      inventoryPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(inventoryPage.breadcrumb.getText()).toEqual('Опис');
    });

    it('should display data correctly', function () {
      expect(inventoryPage.datatable.getColumns(
          'name',
          'type',
          'number',
          'date',
          'publisher',
          'valid',
          'fromDate',
          'toDate'
          )).toEqual([
        ['Образование', 'Висше образование (бакалавър)', '1', '04.04.1981',
          'Български въздухоплавателен център', '', '', ''],
        ['Документ за самоличност', 'Лична карта', '6765432123', '04.04.2010', 'МВР София',
          'Да', '04.04.2010', '04.04.2020'],
        ['Документ за самоличност', 'Лична карта', '6765432123', '04.04.2010', 'МВР София',
          'Да', '04.04.2010', '04.04.2020'],
        ['Документ за самоличност', 'Лична карта', '6765432123', '04.04.2010', 'МВР София',
          'Да', '04.04.2010', '04.04.2020'],
        ['Обучение', 'Контролен талон', 'BG FCL/CPA-00001-11232', '27.02.2013',
          'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', 'Да',
          '27.02.2013', ''],
        ['Обучение', 'Контролен талон', 'BG CPA 00185-11232', '12.03.2012',
          'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', 'Да',
          '12.03.2012', '02.03.2016'],
        ['Обучение', 'Контролна карта', '5-6448', '12.03.2012',
          'Инспектор: Георги Мишев Христов Код:46', 'Не', '12.03.2012', ''],
        ['Теоретично обучение', 'Свидетелство', '80', '10.03.2012', 'УЦ: Ратан',
          'Да', '10.03.2012', ''],
        ['Медицинско свидетелство', '', 'MED BG-1-11232-99994', '04.04.2010',
          '[object Object]', '', '04.04.2010', '04.08.2010'],
        ['Медицинско свидетелство', '', 'MED BG2-3244-11232-9934', '04.04.2005',
          '[object Object]', '', '04.04.2005', '06.09.2015']
      ]);
    });

    it('should go to edit page', function () {
      inventoryPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[0].click();
        expect(ptor.getCurrentUrl())
          .toEqual('http://localhost:52560/#/persons/1/documentEducations/9');
      });
    });
  });

} (protractor, describe, beforeEach, it, expect));