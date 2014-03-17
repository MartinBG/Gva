/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document employment search page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentEmployments/searchEmploymentPO'),
        EditPage = require('../../pageObjects/documentEmployments/employmentPO'),
        personDocEmplPage,
        editDocEmplPage;

    beforeEach(function () {
      ptor.get('#/persons/1/employments');
      personDocEmplPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(personDocEmplPage.breadcrumb.get()).toEqual('Месторабота');
    });

    it('should display data correctly', function () {
      expect(personDocEmplPage.datatable.getColumns(
          'part_hiredate',
          'part_employmentCategory_name',
          'part_organization_name',
          'part_country_name',
          'part_valid_name',
          'part_notes',
          'part_bookPageNumber',
          'part_pageCount'
          )).toEqual([
        ['20.09.2013', 'Ученик Ръководител Полети', 'AAK Progres', 'Кувейт', 'Да', '', '1', '1']
      ]);
    });
   
    it('should delete an employment', function () {
      personDocEmplPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[1].click();
        personDocEmplPage = new Page(ptor);
        expect(personDocEmplPage.tableBody.getText()).toEqual('Няма намерени резултати');
      });
    });

    it('should go to edit page', function () {
      personDocEmplPage.datatable.getRowButtons(1).then(function (buttons) {
        buttons[0].click();
        editDocEmplPage = new EditPage(ptor);
        expect(editDocEmplPage.breadcrumb.get()).toEqual('Редакция на месторабота');
      });
    });
  });

} (protractor, describe, beforeEach, it, expect));