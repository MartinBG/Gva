/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('Person document training search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentTrainings/searchTrainingPO'),
        personDocumentTrainingsPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentTrainings');
      personDocumentTrainingsPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personDocumentTrainingsPage.breadcrumb.getText()).toEqual('Обучение');
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
          'part.documentType.name',
          'part.documentRole.name',
          'part.valid'
          )).toEqual([
        ['BG FCL/CPA-00001-11232', '27.02.2013', '',
          'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', 'Boeing 737', '',
          'Летателен инструктор на самолет', '', 'Контролен талон', 'Обучение', 'Да'],
        ['BG CPA 00185-11232', '12.03.2012', '02.03.2016',
          'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', '',
          '', '', '', 'Контролен талон', 'Обучение', 'Да'],
        ['5-6448', '12.03.2012', '', 'Инспектор: Георги Мишев Христов Код:46', '', '',
          'Летателен инструктор на самолет', '', 'Контролна карта', 'Обучение', 'Не'],
        ['80', '10.03.2012', '', 'УЦ: Ратан', '', '', '',
          '', 'Свидетелство', 'Теоретично обучение', 'Да']
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
          'part.documentType.name',
          'part.documentRole.name',
          'part.valid'
          )).toEqual([
        ['BG CPA 00185-11232', '12.03.2012', '02.03.2016',
          'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)', '',
          '', '', '', 'Контролен талон', 'Обучение', 'Да'],
        ['5-6448', '12.03.2012', '', 'Инспектор: Георги Мишев Христов Код:46', '', '',
          'Летателен инструктор на самолет', '', 'Контролна карта', 'Обучение', 'Не'],
        ['80', '10.03.2012', '', 'УЦ: Ратан', '', '', '',
          '', 'Свидетелство', 'Теоретично обучение', 'Да']
      ]);
    });

    it('should go to edit page', function () {
      personDocumentTrainingsPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[0].click();
        expect(ptor.getCurrentUrl())
          .toEqual('http://localhost:52560/#/persons/1/documentTrainings/14');
      });
    });
  });

}(protractor, describe, beforeEach, it, expect));