/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document education new page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentEducations/еducationPO'),
        SearchPage = require('../../pageObjects/documentEducations/searchEducationPO'),
        newDocEduPage,
        searchDocEduPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentEducations/new');
      newDocEduPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newDocEduPage.breadcrumb.getText())
        .toEqual('Ново образование');
    });
    
    it('should create new document employment correctly', function () {
      newDocEduPage.completionDate.set('20.10.2014');
      newDocEduPage.documentNumber.set('2324a');
      newDocEduPage.speciality.set('Професионална квалификация');
      newDocEduPage.graduation.set('');
      newDocEduPage.school.set('Нов Български Университет-София');
      newDocEduPage.bookPageNumber.set('2');
      newDocEduPage.pageCount.set('5');
      
      newDocEduPage.save();
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
        ['1', '04.04.1981', 'пилот', 'Български въздухоплавателен център',
          'Висше образование (бакалавър)', '2', '1'],
        ['2324a', '20.10.2014', 'Професионална квалификация',
          'Нов български университет-София', 'Висше образование (магистър)', '2', '5']
      ]);
    });

    it('should go to search view at clicking on cancel button', function () {
      newDocEduPage.cancel();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/documentEducations');
    });
  });

} (protractor, describe, beforeEach, it, expect));