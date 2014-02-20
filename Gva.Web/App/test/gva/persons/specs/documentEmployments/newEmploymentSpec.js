/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Person document employment new page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentEmployments/employmentPO'),
        SearchPage = require('../../pageObjects/documentEmployments/searchEmploymentPO'),
        newDocEmplPage,
        searchDocEmplPage;

    beforeEach(function () {
      ptor.get('#/persons/1/employments/new');
      newDocEmplPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newDocEmplPage.breadcrumb.getText())
        .toEqual('Новa месторабота');
    });

    it('should create new document employment correctly', function () {
      newDocEmplPage.hiredate.set('20.10.2014');
      newDocEmplPage.valid.set('Не');
      newDocEmplPage.employmentCategory.set('Координатор по УВД');
      newDocEmplPage.organization.set('Wizz Air');
      newDocEmplPage.country.set('Малави');
      newDocEmplPage.notes.set('some notes..');
      newDocEmplPage.bookPageNumber.set('2');
      newDocEmplPage.pageCount.set('5');

      newDocEmplPage.save();
      searchDocEmplPage = new SearchPage(ptor);

      expect(searchDocEmplPage.datatable.getColumns(
          'part_hiredate',
          'part_employmentCategory_name',
          'part_organization_name',
          'part_country_name',
          'part_valid_name',
          'part_notes',
          'part_bookPageNumber',
          'part_pageCount'
          )).toEqual([
          ['20.09.2013', 'Ученик Ръководител Полети', 'AAK Progres', 'Кувейт', 'Да', '', '1', '1'],
          ['20.10.2014', 'Координатор по УВД', 'Wizz Air', 'Малави', 'Не', 'some notes..', '2', '5']
        ]);
    });

    it('should go to search view at clicking on cancel button', function () {
      newDocEmplPage.cancel();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/persons/1/employments');
    });
  });

} (protractor, describe, beforeEach, it, expect));