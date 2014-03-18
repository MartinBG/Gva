/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document education search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentEducations/searchEducationPO'),
        personDocEduPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentEducations');
      personDocEduPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personDocEduPage.breadcrumb.getText()).toEqual('Образования');
    });

    it('should display data correctly', function () {
      expect(personDocEduPage.datatable.getColumns(
          'part_documentNumber',
          'part_completionDate',
          'part_speciality',
          'part_school_name',
          'part_graduation_name'
          )).toEqual([
        ['1', '04.04.1981', 'пилот', 'Български въздухоплавателен център',
          'Висше образование (бакалавър)']
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
        expect(ptor.getCurrentUrl())
          .toEqual('http://localhost:52560/#/persons/1/documentEducations/9');
      });
    });
  });

} (protractor, describe, beforeEach, it, expect));