/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person document training edit page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentTrainings/trainingPO'),
        SearchPage = require('../../pageObjects/documentTrainings/searchTrainingPO'),
        editTrainingPage,
        searchTrainingPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentTrainings/14');
      editTrainingPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editTrainingPage.breadcrumb.get()).toEqual('Редакция на обучение');
    });

    it('should display correct filled out data', function () {
      expect(editTrainingPage.staffType.get()).toEqual('Членове на екипажа');
      expect(editTrainingPage.documentNumber.get()).toEqual('BG FCL/CPA-00001-11232');
      expect(editTrainingPage.documentDateValidFrom.get()).toEqual('27.02.2013');
      expect(editTrainingPage.documentDateValidTo.get()).toEqual('');
      expect(editTrainingPage.documentPublisher.get())
        .toEqual('ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)');
      expect(editTrainingPage.personOtherDocumentType.get()).toEqual('Контролен талон');
      expect(editTrainingPage.personOtherDocumentRole.get()).toEqual('Обучение');
      expect(editTrainingPage.valid.get()).toEqual('Да');
      expect(editTrainingPage.bookPageNumber.get()).toEqual('87');
      expect(editTrainingPage.pageCount.get()).toEqual('1');
    });

    it('should change training data correctly', function () {
      editTrainingPage.documentNumber.set('Тестов номер');
      editTrainingPage.documentDateValidFrom.set('12.02.2013');
      editTrainingPage.documentDateValidTo.set('22.10.2014');
      editTrainingPage.documentPublisher.set('тестов издател');
      editTrainingPage.personOtherDocumentType.set('Писмо');
      editTrainingPage.personOtherDocumentRole.set('Обучение');
      editTrainingPage.valid.set('Не');
      editTrainingPage.bookPageNumber.set('1a');
      editTrainingPage.pageCount.set('23');

      editTrainingPage.save();
      searchTrainingPage = new SearchPage(ptor);
      expect(searchTrainingPage.breadcrumb.get()).toEqual('Обучение');

      expect(searchTrainingPage.datatable.getColumns(
          'part.documentNumber',
          'part.documentDateValidFrom',
          'part.documentDateValidTo',
          'part.documentPublisher',
          'part.ratingType.name',
          'part.ratingClass.name',
          'part.authorization.name',
          'part.licenceType.name',
          'part.personOtherDocumentType.name',
          'part.personOtherDocumentRole.name',
          'part.valid',
          'part.bookPageNumber',
          'part.pageCount'
          )).toEqual([
        ['Тестов номер', '12.02.2013', '22.10.2014', 'тестов издател', 'Boeing 737', '',
          'Летателен инструктор на самолет', '', 'Писмо', 'Обучение', 'Не', '1a', '23'],
        ['BG CPA 00185-11232', '12.03.2012', '02.03.2016',
          'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', '',
          '', '', '', 'Контролен талон', 'Обучение', 'Да', '81', '1'],
        ['5-6448', '12.03.2012', '', 'Инспектор: Георги Мишев Христов Код:46', '', '',
          'Летателен инструктор на самолет', '', 'Контролна карта', 'Обучение', 'Не', '79', '1'],
        ['80', '10.03.2012', '', 'УЦ: Ратан', '', '', '',
          '', 'Свидетелство', 'Теоретично обучение', 'Да', '79', '1']
      ]);
    });

    it('should go to search view at clicking on cancel button', function () {
      editTrainingPage.cancel();
      searchTrainingPage = new SearchPage(ptor);
      expect(searchTrainingPage.breadcrumb.get()).toEqual('Обучение');
    });
  });

}(protractor, describe, beforeEach, it, expect));