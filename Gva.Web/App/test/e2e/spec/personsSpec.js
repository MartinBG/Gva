/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Persons search page', function () {
    var ptor = protractor.getInstance(),
    Page = require('../pageObjects/personsPO'),
    personsPage;

    beforeEach(function () {
      ptor.get('#/persons');
      personsPage = new Page(ptor);
    });

    it('should display all persons', function () {
      expect(personsPage.datatable.getColumns('names', 'lin', 'uin', 'age')).toEqual([
        ['Иван Иванов Иванов', '11232', '7005159385', '43'],
        ['Атанас Иванов Иванов', '12345', '7903245888', '34'],
        ['Петър Петров Петров', '11111', '6904245664', '44']
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
      ptor.getCurrentUrl().then(function (url) {
        expect(url).toEqual('http://localhost:52560/#/persons/new');
      });
    });

  });
}(protractor, describe, beforeEach, it, expect, require));