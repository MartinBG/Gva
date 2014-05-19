/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {

  'use strict';
  
  describe('Users search page', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/usersPO'),
        usersPage;

    beforeEach(function() {
      ptor.get('#/users');
      usersPage = new Page(ptor);
    });

    it('should display all users', function() {
      expect(usersPage.datatable.getColumns('username', 'fullname', 'roles', 'isActive')).toEqual([
        [ 'admin', 'Administrator', 'Role1, Role2', 'Да' ],
        [ 'peter', 'Peter Ivanov', 'Role1, Role2', 'Да' ],
        [ 'georgi', 'Georgi Petrov', 'Role1, Role2', 'Да' ],
        [ 'test1', 'iztrit', 'Role1, Role2', 'Не' ]
      ]);
    });

    it('should search by username', function() {
      usersPage.searchForm.setFilter('username', 'peter');
      usersPage.searchForm.clickButton('search');

      usersPage = new Page(ptor);
      expect(usersPage.datatable.getColumn('username')).toEqual(['peter']);
    });

    it('should search by fullname', function() {
      usersPage.searchForm.addFilter('fullname');
      usersPage.searchForm.setFilter('fullname', 'Administrator');
      usersPage.searchForm.clickButton('search');

      usersPage = new Page(ptor);
      expect(usersPage.datatable.getColumn('fullname')).toEqual(['Administrator']);
    });

    it('should search by isActive', function() {
      usersPage.searchForm.addFilter('showActive');
      usersPage.searchForm.setFilter('showActive', 1);
      usersPage.searchForm.clickButton('search');

      usersPage = new Page(ptor);
      expect(usersPage.datatable.getColumn('isActive')).toEqual(['Да', 'Да', 'Да']);

      usersPage.searchForm.setFilter('showActive', 2);
      usersPage.searchForm.clickButton('search');

      usersPage = new Page(ptor);
      expect(usersPage.datatable.getColumn('isActive')).toEqual(['Не']);
    });
  });
} (protractor, describe, beforeEach, it, expect, require));