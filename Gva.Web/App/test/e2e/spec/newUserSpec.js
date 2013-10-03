/*global protractor, describe, beforeEach, it, expect*/
(function (protractor) {
  'use strict';

  describe('New user page', function () {
    var ptor = protractor.getInstance();

    beforeEach(function () {
      ptor.get('#/users/new');
    });

    it('should update breadcrumb text', function () {
      /*jshint quotmark:false */
      ptor.findElement(protractor.By.xpath("//ul[@class='breadcrumb']/li[last()]"))
        .getText().then(function (text) {
            expect(text).toEqual('Нов потребител');
          });
    });

    it('should validate username', function () {
      var usernameElem = ptor.findElement(protractor.By.input('user.username')),
          usernameExistsElem = ptor.findElement(protractor.By.id('usernameExists')),
          usernameInvalidElem = ptor.findElement(protractor.By.id('usernameInvalid')),
          saveBtnElem = ptor.findElement(protractor.By.id('saveBtn'));
      
      saveBtnElem.click().then(function () {
        expect(usernameExistsElem.isDisplayed()).toBe(false);
        expect(usernameInvalidElem.isDisplayed()).toBe(true);
      });
      
      usernameElem.sendKeys('123');
      saveBtnElem.click().then(function () {
        expect(usernameExistsElem.isDisplayed()).toBe(false);
        expect(usernameInvalidElem.isDisplayed()).toBe(true);
      });

      usernameElem.clear();
      usernameElem.sendKeys('georgi');
      saveBtnElem.click().then(function () {
        expect(usernameExistsElem.isDisplayed()).toBe(true);
        expect(usernameInvalidElem.isDisplayed()).toBe(false);
      });
    });

    it('should validate password', function () {
      var passwordElem = ptor.findElement(protractor.By.input('password')),
          passInvalidElem = ptor.findElement(protractor.By.id('passInvalid')),
          confirmPassElem = ptor.findElement(protractor.By.input('confirmPassword')),
          confirmPassInvalidElem = ptor.findElement(protractor.By.id('confirmPassInvalid')),
          saveBtnElem = ptor.findElement(protractor.By.id('saveBtn'));

      ptor.findElement(protractor.By.input('setPassword')).click();
      
      saveBtnElem.click().then(function () {
        expect(passInvalidElem.isDisplayed()).toBe(true);
      });
      
      passwordElem.sendKeys('1234567');
      saveBtnElem.click().then(function () {
        expect(passInvalidElem.isDisplayed()).toBe(true);
      });

      passwordElem.sendKeys('8');
      saveBtnElem.click().then(function () {
        expect(passInvalidElem.isDisplayed()).toBe(false);
        expect(confirmPassInvalidElem.isDisplayed()).toBe(true);
      });

      confirmPassElem.sendKeys('12345678');
      saveBtnElem.click().then(function () {
        expect(passInvalidElem.isDisplayed()).toBe(false);
        expect(confirmPassInvalidElem.isDisplayed()).toBe(false);
      });
    });

    it('should be able to create new user', function () {
      ptor.findElement(protractor.By.input('user.username')).sendKeys('atanas');

      ptor.findElement(protractor.By.input('user.fullname')).sendKeys('Atanas Spasov');

      ptor.findElement(protractor.By.input('setPassword')).click();
      ptor.findElement(protractor.By.input('password')).sendKeys('12345678');
      ptor.findElement(protractor.By.input('confirmPassword')).sendKeys('12345678');
      
      ptor.findElement(protractor.By.input('user.isActive')).click();
      
      ptor.findElement(protractor.By.id('saveBtn')).click().then(function () {
        ptor.getCurrentUrl().then(function (url) {
          expect(url).toEqual('http://localhost:52560/#/users');
        });
      });
    });
  });
}(protractor));