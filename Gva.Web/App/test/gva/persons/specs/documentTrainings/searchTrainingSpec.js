/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('Person document training search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentTrainings/searchTrainingPO'),
        EditPage = require('../../pageObjects/documentTrainings/trainingPO'),
        personDocumentTrainingsPage,
        editTrainingPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentTrainings');
      personDocumentTrainingsPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personDocumentTrainingsPage.breadcrumb.get()).toEqual('Обучение');
    });

    it('should display data correctly', function () {
      expect(personDocumentTrainingsPage.datatable.getColumns(
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

    it('should delete a documentTraining', function () {
      personDocumentTrainingsPage.firstDeleteBtn.click();
      personDocumentTrainingsPage = new Page(ptor);
      expect(personDocumentTrainingsPage.datatable.getColumns(
          'part.staffType',
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
        ['BG CPA 00185-11232', '12.03.2012', '02.03.2016',
          'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', '',
          '', '', '', 'Контролен талон', 'Обучение', 'Да', '81', '1'],
        ['5-6448', '12.03.2012', '', 'Инспектор: Георги Мишев Христов Код:46', '', '',
          'Летателен инструктор на самолет', '', 'Контролна карта', 'Обучение', 'Не', '79', '1'],
        ['80', '10.03.2012', '', 'УЦ: Ратан', '', '', '',
          '', 'Свидетелство', 'Теоретично обучение', 'Да', '79', '1']
      ]);
    });

    it('should go to edit page', function () {
      personDocumentTrainingsPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[0].click();
        editTrainingPage = new EditPage(ptor);
        expect(editTrainingPage.breadcrumb.get()).toEqual('Редакция на обучение');
      });
    });
  });

}(protractor, describe, beforeEach, it, expect));