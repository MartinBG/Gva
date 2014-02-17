/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';

  describe('Person document education search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentEducations/searchEducationPO'),
        personDocumentEducationsPage;

    beforeEach(function () {
      ptor.get('#/persons/1/documentEducations');
      personDocumentEducationsPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personDocumentEducationsPage.breadcrumb.getText()).toEqual('Образования');
    });

    it('should display data correctly', function () {
      expect(personDocumentEducationsPage.datatable.getColumns(
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

    it('should delete a documentEducation', function () {
      personDocumentEducationsPage.deleteFirst();
      personDocumentEducationsPage = new Page(ptor);
      expect(personDocumentEducationsPage.datatable.getColumns(
        'part.documentNumber',
        'part.completionDate',
        'part.speciality',
        'part.school.name',
        'part.graduation.name',
        'part.notes',
        'part.bookPageNumber',
        'part.pageCount'
        )).toEqual([[]]);
    });

    it('should go to edit page', function () {
      personDocumentEducationsPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[0].click();
        expect(ptor.getCurrentUrl())
          .toEqual('http://localhost:52560/#/persons/1/documentEducations/9');
      });
    });
  });

}(protractor, describe, beforeEach, it, expect));