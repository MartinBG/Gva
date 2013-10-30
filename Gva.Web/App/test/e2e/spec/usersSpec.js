/*global protractor, describe, beforeEach, it, expect*/
(function (protractor) {
  'use strict';
  
  describe('Users search page', function() {
    var ptor = protractor.getInstance(),
        searchBtn,
        addBtn;

    beforeEach(function() {
      ptor.get('#/users');

      searchBtn = ptor.findElement(protractor.By.css('div[action=search] > button'));
      addBtn = ptor.findElement(protractor.By.css('div[action=add] button'));
    });

    it('should display all users', function() {
      var allUsernamesPromise = ptor.findElements(protractor.By.repeater('user in users')
        .column('username'));

      allUsernamesPromise.then(function (elements) {
        var usernames = protractor.promise.fullyResolved(elements.map(function (el) {
          return el.getText();
        }));

        expect(usernames).toEqual(['admin', 'peter', 'georgi', 'test1']);
      });
    });
    
    it('should search by username', function() {
      ptor.findElement(protractor.By.css('div[name=username] > input')).sendKeys('peter');

      searchBtn.click().then(function() {
        var firstUsername = ptor.findElement(protractor.By.repeater('user in users')
          .row(0)
          .column('username'));

        firstUsername.getText().then(function(text) {
          expect(text).toEqual('peter');
        });
      });
    });

    it('should search by fullname', function() {
      addBtn.click().then(function () {
        ptor.findElements(protractor.By.css('div[action=add] li')).then(function (filters) {
          filters[0].click().then(function () {
            ptor.findElement(protractor.By.css('div[name=fullname] > input'))
              .sendKeys('Administrator');

            searchBtn.click().then(function() {
              var firstFullname = ptor.findElement(protractor.By.repeater('user in users')
                .row(0)
                .column('fullname'));

              firstFullname.getText().then(function(text) {
                expect(text).toEqual('Administrator');
              });
            });
          });
        });
      });
    });

    it('should search by isActive', function() {
      addBtn.click().then(function () {
        ptor.findElements(protractor.By.css('div[action=add] li')).then(function (filters) {
          filters[1].click().then(function () {
            ptor.findElement(protractor.By.className('select2-container'))
                .click().then( function () {
              ptor.findElements(protractor.By.className('select2-result'))
                  .then(function (select2Opts) {
                select2Opts[0].click();
              });

              ptor.findElement(protractor.By.css('div[action=search] > button')).click()
                  .then(function() {
                var firstFullname = ptor.findElement(protractor.By.repeater('user in users')
                  .row(0)
                  .column('fullname'));

                firstFullname.getText().then(function(text) {
                  expect(text).toEqual('Administrator');
                });
              });
            });

            ptor.findElement(protractor.By.className('select2-container'))
                .click().then( function () {
              ptor.findElements(protractor.By.className('select2-result'))
                  .then(function (select2Opts) {
                select2Opts[1].click();
              });

              ptor.findElement(protractor.By.css('div[action=search] > button')).click()
                  .then(function() {
                var firstFullname = ptor.findElement(protractor.By.repeater('user in users')
                  .row(0)
                  .column('fullname'));

                firstFullname.getText().then(function(text) {
                  expect(text).toEqual('iztrit');
                });
              });
            });
          });
        });
      });
    });
  });
} (protractor));
