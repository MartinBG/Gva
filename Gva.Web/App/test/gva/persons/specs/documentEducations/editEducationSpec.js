/*global protractor, require, describe, beforeEach, it, expect*/
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
      expect(editDocEduPage.graduation.get()).toEqual('Висше образование (бакалавър)');
      expect(editDocEduPage.school.get()).toEqual('Български въздухоплавателен център');

    });

    it('should change education data correctly', function () {
      editDocEduPage.completionDate.set('20.10.2014');
      editDocEduPage.documentNumber.set('2324a');
      editDocEduPage.speciality.set('Професионална квалификация');
      editDocEduPage.graduation.set('Висше образование (магистър)');
      editDocEduPage.school.set('Български въздухоплавателен център');
      
      editDocEduPage.save();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentEducations');
      searchDocEduPage = new SearchPage(ptor);

      expect(searchDocEduPage.datatable.getColumns(
          'part_documentNumber',
          'part_completionDate',
          'part_speciality',
          'part_school_name',
          'part_graduation_name'
          )).toEqual([
        ['2324a', '20.10.2014', 'Професионална квалификация',
          'Български въздухоплавателен център', 'Висше образование (магистър)']
      ]);
    });
  });

} (protractor, describe, beforeEach, it, expect));