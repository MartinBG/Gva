/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Edit user page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/newUserPO'),
        SearchPage = require('../pageObjects/usersPO'),
        editUserPage,
        searchUserPage;

    beforeEach(function () {
      ptor.get('#/users/2');
      editUserPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(editUserPage.breadcrumb.get()).toEqual('Редакция');
    });

    it('should display user', function () {
      expect(editUserPage.username.isEnabled()).toBe(false);
      expect(editUserPage.username.get()).toEqual('peter');
      expect(editUserPage.fullname.get()).toEqual('Peter Ivanov');
      expect(editUserPage.hasPassword.isSelected()).toBe(true);
      expect(editUserPage.isActive.isSelected()).toBe(true);
    });

    it('should be able to change user', function () {
      editUserPage.fullname.set('Peter Atanasov');
      expect(editUserPage.fullname.get()).toEqual('Peter Atanasov');

      editUserPage.save();
      searchUserPage = new SearchPage(ptor);
      expect(searchUserPage.breadcrumb.get()).toEqual('Потребители');
    });

  });
}(protractor, describe, beforeEach, it, expect, require));