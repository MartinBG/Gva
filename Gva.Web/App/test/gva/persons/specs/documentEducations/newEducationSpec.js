/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document education new page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentEducations/educationPO'),
        SearchPage = require('../../pageObjects/documentEducations/searchEducationPO'),
        newDocEduPage,
        searchDocEduPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentEducations/new');
      newDocEduPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newDocEduPage.breadcrumb.get())
        .toEqual('Ново образование');
    });
    
    it('should create new document employment correctly', function () {
      newDocEduPage.completionDate.set('20.10.2014');
      newDocEduPage.documentNumber.set('2324a');
      newDocEduPage.speciality.set('Професионална квалификация');
      newDocEduPage.graduation.set('Висше образование (магистър)');
      newDocEduPage.school.set('Български въздухоплавателен център');
      
      newDocEduPage.save();

      searchDocEduPage = new SearchPage(ptor);
      expect(searchDocEduPage.breadcrumb.get()).toEqual('Образования');

      expect(searchDocEduPage.datatable.getColumns(
          'part_documentNumber',
          'part_completionDate',
          'part_speciality',
          'part_school_name',
          'part_graduation_name'
          )).toEqual([
        ['1', '04.04.1981', 'пилот', 'Български въздухоплавателен център',
          'Висше образование (бакалавър)'],
        ['2324a', '20.10.2014', 'Професионална квалификация',
          'Български въздухоплавателен център', 'Висше образование (магистър)']
      ]);
    });

    it('should go to search view at clicking on cancel button', function () {
      newDocEduPage.cancel();
      SearchPage = new SearchPage(ptor);
      expect(SearchPage.breadcrumb.get()).toEqual('Образования');
    });
  });

} (protractor, describe, beforeEach, it, expect));