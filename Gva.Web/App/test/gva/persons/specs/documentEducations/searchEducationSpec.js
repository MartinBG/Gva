/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document education search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentEducations/searchEducationPO'),
        EditPage = require('../../pageObjects/documentEducations/educationPO'),
        personDocEduPage,
        editDocEduPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentEducations');
      personDocEduPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personDocEduPage.breadcrumb.get()).toEqual('Образования');
    });

    it('should display data correctly', function () {
      expect(personDocEduPage.datatable.getColumns(
          'part_documentNumber',
          'part_completionDate',
          'part_speciality',
          'part_school_name',
          'part_graduation_name',
          'part_bookPageNumber',
          'part_pageCount'
          )).toEqual([
        ['1', '04.04.1981', 'пилот', 'Български въздухоплавателен център',
          'Висше образование (бакалавър)', '2', '1']
      ]);
    });
   
    it('should delete an document education', function () {
      personDocEduPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[1].click();
        personDocEduPage = new Page(ptor);
        expect(personDocEduPage.tableBody.getText()).toEqual('Няма намерени резултати');
      });
    });

    it('should go to edit page', function () {
      personDocEduPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[0].click();
        editDocEduPage = new EditPage(ptor);
        expect(editDocEduPage.breadcrumb.get()).toEqual('Редакция на образование');
      });
    });
  });

} (protractor, describe, beforeEach, it, expect));