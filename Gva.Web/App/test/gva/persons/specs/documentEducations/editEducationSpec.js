﻿/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document education edit page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentEducations/educationPO'),
        SearchPage = require('../../pageObjects/documentEducations/searchEducationPO'),
        editDocEduPage,
        searchDocEduPage;

    beforeEach(function() {
      ptor.get('#/persons/1/documentEducations/9');
      editDocEduPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editDocEduPage.breadcrumb.getText()).toEqual('Редакция на образование');
    });

    it('should display correct filled out data', function () {

      expect(editDocEduPage.completionDate.get()).toEqual('04.04.1981');
      expect(editDocEduPage.documentNumber.get()).toEqual('1');
      expect(editDocEduPage.speciality.get()).toEqual('пилот');
      expect(editDocEduPage.fileSpan.getText()).toEqual('testName.pdf');
      expect(editDocEduPage.graduation.get()).toEqual('Висше образование (бакалавър)');
      expect(editDocEduPage.school.get()).toEqual('Български въздухоплавателен център');
      expect(editDocEduPage.bookPageNumber.get()).toEqual('2');
      expect(editDocEduPage.pageCount.get()).toEqual('1');

    });

    it('should change education data correctly', function () {
      editDocEduPage.completionDate.set('20.10.2014');
      editDocEduPage.documentNumber.set('2324a');
      editDocEduPage.speciality.set('Професионална квалификация');
      editDocEduPage.graduation.set('');
      editDocEduPage.school.set('Нов Български Университет-София');
      editDocEduPage.bookPageNumber.set('2');
      editDocEduPage.pageCount.set('5');
      
      editDocEduPage.save();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentEducations');
      searchDocEduPage = new SearchPage(ptor);

      expect(searchDocEduPage.datatable.getColumns(
          'part_documentNumber',
          'part_completionDate',
          'part_speciality',
          'part_school_name',
          'part_graduation_name',
          'part_bookPageNumber',
          'part_pageCount'
          )).toEqual([
        ['2324a', '20.10.2014', 'Професионална квалификация',
          'Нов български университет-София', 'Висше образование (бакалавър)', '2', '5']
      ]);
    });
  });

} (protractor, describe, beforeEach, it, expect));