/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person document training new page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentTrainings/newTrainingPO'),
        SearchPage = require('../../pageObjects/documentTrainings/searchTrainingPO'),
        newTrainingPage,
        searchTrainingPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentTrainings/new');
      newTrainingPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newTrainingPage.breadcrumb.getText()).toEqual('Ново обучение');
    });

    it('should create new training document correctly', function () {
      newTrainingPage.chooseStaffType('Общ документ').then(function () {
        newTrainingPage.documentNumber.set('Тестов номер');
        newTrainingPage.documentDateValidFrom.set('12.02.2013');
        newTrainingPage.documentDateValidTo.set('22.10.2014');
        newTrainingPage.documentPublisher.set('тестов издател');
        newTrainingPage.personOtherDocumentType.set('Писмо');
        newTrainingPage.personOtherDocumentRole.set('Тренажор');
        newTrainingPage.valid.set('Не');
        newTrainingPage.bookPageNumber.set('1a');
        newTrainingPage.pageCount.set('23');

        newTrainingPage.save();
        expect(ptor.getCurrentUrl())
          .toEqual('http://localhost:52560/#/persons/1/documentTrainings');
        searchTrainingPage = new SearchPage(ptor);

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
          ['BG FCL/CPA-00001-11232', '27.02.2013', '',
            'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', 'Boeing 737', '',
            'Летателен инструктор на самолет', '', 'Контролен талон', 'Обучение', 'Да', '87', '1'],
          ['BG CPA 00185-11232', '12.03.2012', '02.03.2016',
            'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', '',
            '', '', '', 'Контролен талон', 'Обучение', 'Да', '81', '1'],
          ['5-6448', '12.03.2012', '', 'Инспектор: Георги Мишев Христов Код:46', '', '',
            'Летателен инструктор на самолет', '', 'Контролна карта', 'Обучение', 'Не', '79', '1'],
          ['80', '10.03.2012', '', 'УЦ: Ратан', '', '', '',
            '', 'Свидетелство', 'Теоретично обучение', 'Да', '79', '1'],
          ['Тестов номер', '12.02.2013', '22.10.2014', 'тестов издател', '', '', '',
            '', 'Писмо', 'Тренажор', 'Не', '1a', '23']
        ]);
      });
    }, 15000);

    it('should go to search view at clicking on cancel button', function () {
      newTrainingPage.chooseStaffType('Общ документ').then(function () {
        newTrainingPage.documentNumber.set('Тестов номер');
        newTrainingPage.documentDateValidFrom.set('12.02.2013');
        newTrainingPage.documentDateValidTo.set('22.10.2014');
        newTrainingPage.documentPublisher.set('тестов издател');
        newTrainingPage.personOtherDocumentType.set('Писмо');
        newTrainingPage.personOtherDocumentRole.set('Тренажор');
        newTrainingPage.valid.set('Не');
        newTrainingPage.bookPageNumber.set('1a');
        newTrainingPage.pageCount.set('23');

        newTrainingPage.cancel();
        expect(ptor.getCurrentUrl())
          .toEqual('http://localhost:52560/#/persons/1/documentTrainings');
        searchTrainingPage = new SearchPage(ptor);

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
          ['BG FCL/CPA-00001-11232', '27.02.2013', '',
            'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', 'Boeing 737', '',
            'Летателен инструктор на самолет', '', 'Контролен талон', 'Обучение', 'Да', '87', '1'],
          ['BG CPA 00185-11232', '12.03.2012', '02.03.2016',
            'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', '',
            '', '', '', 'Контролен талон', 'Обучение', 'Да', '81', '1'],
          ['5-6448', '12.03.2012', '', 'Инспектор: Георги Мишев Христов Код:46', '', '',
            'Летателен инструктор на самолет', '', 'Контролна карта', 'Обучение', 'Не', '79', '1'],
          ['80', '10.03.2012', '', 'УЦ: Ратан', '', '', '',
            '', 'Свидетелство', 'Теоретично обучение', 'Да', '79', '1']
        ]);
      });
    });
  });

}(protractor, describe, beforeEach, it, expect));