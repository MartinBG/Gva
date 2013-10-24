/*global protractor, describe, beforeEach, it, expect*/
(function (protractor) {
  'use strict';
  
  describe('Users search page', function() {
    var ptor = protractor.getInstance();
    
    beforeEach(function() {
      ptor.get('#/users');
    });

    it('should display all users', function() {
      ptor
        .findElements(
          protractor.By.repeater('user in users')
          .column('username'))
        .then(function (elements) {
          var usernames = protractor.promise.fullyResolved(
            elements.map(function (el) {
              return el.getText();
            }));
          expect(usernames).toEqual(['admin', 'peter', 'georgi', 'test1']);
        });
    });
    
    it('should search by username', function() {
      ptor.findElement(protractor.By.input('username')).sendKeys('peter');
      ptor.findElement(protractor.By.id('searchBtn')).click().then(function() {
        ptor
          .findElement(
            protractor.By.repeater('user in users')
            .row(0)
            .column('username'))
          .getText()
          .then(function(text) {
            expect(text).toEqual('peter');
          });
      });
    });

    it('should search by fullname', function() {
      ptor.findElement(protractor.By.input('fullname')).sendKeys('Administrator');
      ptor.findElement(protractor.By.id('searchBtn')).click().then(function() {
        ptor
          .findElement(
            protractor.By.repeater('user in users')
            .row(0)
            .column('fullname'))
          .getText()
          .then(function(text) {
            expect(text).toEqual('Administrator');
          });
      });
    });

    it('should search by isActive', function() {
      ptor.findElement(protractor.By.select('showActive'))
          .findElement(protractor.By.css('option[value="true"]')).click();
      ptor.findElement(protractor.By.id('searchBtn')).click().then(function() {
        ptor
          .findElement(
            protractor.By.repeater('user in users')
            .row(0)
            .column('fullname'))
          .getText()
          .then(function(text) {
            expect(text).toEqual('Administrator');
          });
      });

      ptor.findElement(protractor.By.select('showActive'))
          .findElement(protractor.By.css('option[value="false"]')).click();
      ptor.findElement(protractor.By.id('searchBtn')).click().then(function() {
        ptor
          .findElement(
            protractor.By.repeater('user in users')
            .row(0)
            .column('fullname'))
          .getText()
          .then(function(text) {
            expect(text).toEqual('iztrit');
          });
      });
    });
  });
} (protractor));
