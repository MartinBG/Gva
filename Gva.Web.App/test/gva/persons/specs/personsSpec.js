/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Persons search page', function () {
    var ptor = protractor.getInstance(),
    Page = require('../pageObjects/personsPO'),
    NewPage = require('../pageObjects/newPersonPO'),
    personsPage,
    newPersonsPage;

    beforeEach(function () {
      ptor.get('#/persons');
      personsPage = new Page(ptor);
    });

    it('should display all persons', function () {
      expect(personsPage.datatable.getColumns('lin', 'names', 'uin', 'age')).toEqual([
        ['11232', 'Иван Иванов Иванов', '7005159385', '43'],
        ['12345', 'Атанас Иванов Иванов', '7903245888', '34'],
        ['11111', 'Петър Петров Петров', '6904245664', '44']
      ]);
    });

    it('should search by lin', function () {
      personsPage.searchForm.setFilter('lin', '11111');
      personsPage.searchForm.clickButton('search');

      personsPage = new Page(ptor);
      expect(personsPage.datatable.getColumn('names')).toEqual(['Петър Петров Петров']);
    });

    it('should search by uin', function () {
      personsPage.searchForm.setFilter('uin', '6904245664');
      personsPage.searchForm.clickButton('search');

      personsPage = new Page(ptor);
      expect(personsPage.datatable.getColumn('names')).toEqual(['Петър Петров Петров']);
    });

    it('should search by names', function () {
      personsPage.searchForm.addFilter('names');
      personsPage.searchForm.setFilter('names', 'Петър');
      personsPage.searchForm.clickButton('search');

      personsPage = new Page(ptor);
      expect(personsPage.datatable.getColumn('names')).toEqual(['Петър Петров Петров']);
    });

    it('should redirect to new person page', function () {
      personsPage.searchForm.clickButton('newPerson');
      newPersonsPage = new NewPage(ptor);
      expect(newPersonsPage.breadcrumb.get()).toEqual('Ново физическо лице');
    });

  });
}(protractor, describe, beforeEach, it, expect, require));