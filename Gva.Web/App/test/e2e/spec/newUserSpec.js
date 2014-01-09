/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('New user page', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/newUserPO'),
        newUserPage;

    beforeEach(function () {
      ptor.get('#/users/new');
      newUserPage = new Page(ptor);
    });

    it('should update breadcrumb text', function () {
      expect(newUserPage.breadcrumb.getText()).toEqual('Нов потребител');
    });

    it('should validate username', function () {
      newUserPage.save();
      expect(newUserPage.usernameExistsError.isDisplayed()).toBe(false);
      expect(newUserPage.usernameInvalidError.isDisplayed()).toBe(true);

      newUserPage.username.set('123');
      newUserPage.save();
      expect(newUserPage.usernameExistsError.isDisplayed()).toBe(false);
      expect(newUserPage.usernameInvalidError.isDisplayed()).toBe(true);

      newUserPage.username.set('georgi');
      newUserPage.save();
      expect(newUserPage.usernameExistsError.isDisplayed()).toBe(true);
      expect(newUserPage.usernameInvalidError.isDisplayed()).toBe(false);
    });

    it('should validate password', function () {
      newUserPage.hasPassword.click();
      newUserPage.save();
      expect(newUserPage.passInvalidError.isDisplayed()).toBe(true);

      newUserPage.password.sendKeys('1234567');
      newUserPage.save();
      expect(newUserPage.passInvalidError.isDisplayed()).toBe(true);

      newUserPage.password.sendKeys('8');
      newUserPage.save();
      expect(newUserPage.passInvalidError.isDisplayed()).toBe(false);
      expect(newUserPage.confirmPassInvalidError.isDisplayed()).toBe(true);

      newUserPage.confirmPassword.sendKeys('12345678');
      newUserPage.save();
      expect(newUserPage.passInvalidError.isDisplayed()).toBe(false);
      expect(newUserPage.confirmPassInvalidError.isDisplayed()).toBe(false);
    });

    it('should be able to create new user', function () {
      newUserPage.username.set('atanas');
      newUserPage.fullname.set('Atanas Spasov');
      newUserPage.hasPassword.click();
      newUserPage.password.sendKeys('12345678');
      newUserPage.confirmPassword.sendKeys('12345678');
      newUserPage.isActive.click();

      newUserPage.save();
      expect(ptor.getCurrentUrl()).toEqual('http://localhost:52560/#/users');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));