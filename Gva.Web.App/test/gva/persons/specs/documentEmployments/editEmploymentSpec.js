/*global protractor, require, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {

  'use strict';
  
  describe('Person document employment edit page', function() {
    var ptor = protractor.getInstance(),
        Page = require('../../pageObjects/documentEmployments/employmentPO'),
        SearchPage = require('../../pageObjects/documentEmployments/searchEmploymentPO'),
        editDocEmplPage,
        searchDocEmplPage;

    beforeEach(function() {
      ptor.get('#/persons/1/employments/8');
      editDocEmplPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editDocEmplPage.breadcrumb.get()).toEqual('Редакция на месторабота');
    });

    it('should display correct filled out data', function () {
      expect(editDocEmplPage.hiredate.get()).toEqual('20.09.2013');
      expect(editDocEmplPage.valid.get()).toEqual('Да');
      expect(editDocEmplPage.employmentCategory.get()).toEqual('Ученик Ръководител Полети');
      expect(editDocEmplPage.organization.get()).toEqual('AAK Progres');
      expect(editDocEmplPage.country.get()).toEqual('Кувейт');
      expect(editDocEmplPage.notes.get()).toEqual('');
    });
   
    it('should change employment data correctly', function () {
      editDocEmplPage.hiredate.set('20.10.2014');
      editDocEmplPage.valid.set('Не');
      editDocEmplPage.employmentCategory.set('Координатор по УВД');
      editDocEmplPage.organization.set('Wizz Air');
      editDocEmplPage.country.set('Малави');
      editDocEmplPage.notes.set('some notes..');
      
      editDocEmplPage.save();

      searchDocEmplPage = new SearchPage(ptor);
      expect(searchDocEmplPage.breadcrumb.get()).toEqual('Месторабота');

      expect(searchDocEmplPage.datatable.getColumns(
          'part_hiredate',
          'part_employmentCategory_name',
          'part_organization_name',
          'part_country_name',
          'part_valid_name',
          'part_notes'
          )).toEqual([
          ['20.10.2014', 'Координатор по УВД', 'Wizz Air', 'Малави', 'Не', 'some notes..']
        ]);
    });
  });

} (protractor, describe, beforeEach, it, expect));