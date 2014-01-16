/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Edit user page', function () {
    var ptor = protractor.getInstance();

    beforeEach(function () {
      ptor.get('#/users/2');
    });

    it('should update breadcrumb text', function () {
      /*jshint quotmark:false */
      ptor.findElement(protractor.By.xpath("//ul[@class='breadcrumb']/li[last()]"))
        .getText().then(function (text) {
          expect(text).toEqual('Редакция');
        });
    });

    it('should display user', function () {
      var usernameElem = ptor.findElement(protractor.By.input('user.username')),
          fullnameElem = ptor.findElement(protractor.By.input('user.fullname'));

      expect(usernameElem.isEnabled()).toBe(false);
      usernameElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('peter');
      });

      fullnameElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('Peter Ivanov');
      });

      expect(ptor.findElement(protractor.By.input('setPassword')).isSelected()).toBe(true);

      expect(ptor.findElement(protractor.By.input('user.isActive')).isSelected()).toBe(true);
    });

    it('should be able to change user', function () {
      var fullnameElem = ptor.findElement(protractor.By.input('user.fullname'));

      fullnameElem.clear();
      fullnameElem.sendKeys('Peter Atanasov');

      expect(fullnameElem.getAttribute('value')).toEqual('Peter Atanasov');

      ptor.findElement(protractor.By.id('saveBtn')).click().then(function () {
        ptor.getCurrentUrl().then(function (url) {
          expect(url).toEqual('http://localhost:52560/#/users');
        });
      });
    });

    it('should be able to return to all users page', function () {
      ptor.findElement(protractor.By.id('cancelBtn')).click().then(function () {
        ptor.getCurrentUrl().then(function (url) {
          expect(url).toEqual('http://localhost:52560/#/users');
        });
      });
    });
  });
}(protractor, describe, beforeEach, it, expect));