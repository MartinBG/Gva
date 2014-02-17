/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person document education edit page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentEducations/educationPO'),
        SearchPage = require('../../pageObjects/documentEducations/searchEducationPO'),
        editEducationPage,
        searchEducationPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentEducations/9');
      editEducationPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editEducationPage.breadcrumb.getText()).toEqual('Редакция на образование');
    });

    it('should display correct filled out data', function () {
      expect(editEducationPage.documentNumber.get()).toEqual('1');
      expect(editEducationPage.completionDate.get()).toEqual('04.04.1981');
      expect(editEducationPage.graduation.get()).toEqual('Висше образование (бакалавър)');
      expect(editEducationPage.school.get()).toEqual('Български въздухоплавателен център');
      expect(editEducationPage.speciality.get()).toEqual('пилот');
      expect(editEducationPage.bookPageNumber.get()).toEqual('2');
      expect(editEducationPage.pageCount.get()).toEqual('1');
    });

    it('should change education data correctly', function () {
      editEducationPage.documentNumber.set('Тестов номер');
      editEducationPage.completionDate.set('12.02.2013');
      editEducationPage.graduation.set('Висше образование (магистър)');
      editEducationPage.school.set('Нов български университет-София');
      editEducationPage.speciality.set('специалност');
      editEducationPage.bookPageNumber.set('12');
      editEducationPage.pageCount.set('23');

      editEducationPage.save();
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
        ['Тестов номер', '12.02.2013', 'специалност', 'Нов български университет-София',
          'Висше образование (магистър)', '12', '23']
      ]);
    });

    it('should go to search view at clicking on cancel button', function () {
      editEducationPage.cancel();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentEducations');
    });
  });

}(protractor, describe, beforeEach, it, expect));