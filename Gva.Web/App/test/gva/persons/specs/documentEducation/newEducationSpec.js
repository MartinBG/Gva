/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person document education new page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentEducations/educationPO'),
        SearchPage = require('../../pageObjects/documentEducations/searchEducationPO'),
        newEducationPage,
        searchEducationPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentEducations/new');
      newEducationPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newEducationPage.breadcrumb.getText()).toEqual('Ново образование');
    });

    it('should create new education document correctly', function () {
      newEducationPage.documentNumber.set('Тестов номер');
      newEducationPage.completionDate.set('12.02.2013');
      newEducationPage.graduation.set('Висше образование (магистър)');
      newEducationPage.school.set('Нов български университет-София');
      newEducationPage.speciality.set('специалност');
      newEducationPage.bookPageNumber.set('12');
      newEducationPage.pageCount.set('23');

      newEducationPage.save();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentEducations');
      searchEducationPage = new SearchPage(ptor);

      expect(searchEducationPage.datatable.getColumns(
          'part.documentNumber',
          'part.completionDate',
          'part.speciality',
          'part.school.name',
          'part.graduation.name',
          'part.notes',
          'part.bookPageNumber',
          'part.pageCount'
          )).toEqual([
        ['1', '04.04.1981', 'пилот', 'Български въздухоплавателен център',
          'Висше образование (бакалавър)', '2', '1'],
        ['Тестов номер', '12.02.2013', 'специалност', 'Нов български университет-София',
          'Висше образование (магистър)', '12', '23']
      ]);
    });

    it('should go to search view at clicking on cancel button', function () {
      newEducationPage.documentNumber.set('Тестов номер');
      newEducationPage.completionDate.set('12.02.2013');
      newEducationPage.graduation.set('Висше образование (магистър)');
      newEducationPage.school.set('Нов български университет-София');
      newEducationPage.speciality.set('специалност');
      newEducationPage.bookPageNumber.set('12');
      newEducationPage.pageCount.set('23');

      newEducationPage.cancel();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentEducations');
      searchEducationPage = new SearchPage(ptor);

      expect(searchEducationPage.datatable.getColumns(
          'part.documentNumber',
          'part.completionDate',
          'part.speciality',
          'part.school.name',
          'part.graduation.name',
          'part.notes',
          'part.bookPageNumber',
          'part.pageCount'
          )).toEqual([
        ['1', '04.04.1981', 'пилот', 'Български въздухоплавателен център',
          'Висше образование (бакалавър)', '2', '1']
      ]);
    });
  });

}(protractor, describe, beforeEach, it, expect));