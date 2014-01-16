/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('scPromiseState directive', function() {
    var ptor = protractor.getInstance(),
        promiseStateDirectiveElem;

    beforeEach(function (){
      ptor.get('#/test/promise');

      promiseStateDirectiveElem = ptor.findElement(protractor.By.id('promiseStateDir'));
    });

    it('should show the loading icon when the promise is not null and not resolved.', function() {
      var createBtnElem = ptor.findElement(protractor.By.id('createBtn'));

      promiseStateDirectiveElem.isDisplayed().then(function (value) {
        expect(value).toBe(false);
      });

      createBtnElem.click();

      promiseStateDirectiveElem.isDisplayed().then(function (value) {
        expect(value).toBe(true);
      });

      promiseStateDirectiveElem.getAttribute('class').then(function (value) {
        expect(value.split(' ').indexOf('glyphicon-loading')).toBeGreaterThan(0);
      });
    });

    it('should show the resolved icon with the correct title' +
      ' when the promise is resolved.', function () {
      var createBtnElem = ptor.findElement(protractor.By.id('createBtn'));
      var resolveBtnElem = ptor.findElement(protractor.By.id('resolveBtn'));

      promiseStateDirectiveElem.isDisplayed().then(function (value) {
        expect(value).toBe(false);
      });

      createBtnElem.click();
      resolveBtnElem.click();

      promiseStateDirectiveElem.isDisplayed().then(function (value) {
        expect(value).toBe(true);
      });

      promiseStateDirectiveElem.getAttribute('class').then(function (value) {
        expect(value.split(' ').indexOf('glyphicon-ok')).toBeGreaterThan(0);
      });

      promiseStateDirectiveElem.getAttribute('title').then(function (value) {
        expect(value).toBe('Hurray, resolved :)');
      });
    });

    it('should show the rejected icon with the correct title' +
      ' when the promise is rejected.', function () {
      var createBtnElem = ptor.findElement(protractor.By.id('createBtn'));
      var rejectBtnElem = ptor.findElement(protractor.By.id('rejectBtn'));

      promiseStateDirectiveElem.isDisplayed().then(function (value) {
        expect(value).toBe(false);
      });

      createBtnElem.click();
      rejectBtnElem.click();

      promiseStateDirectiveElem.isDisplayed().then(function (value) {
        expect(value).toBe(true);
      });

      promiseStateDirectiveElem.getAttribute('class').then(function (value) {
        expect(value.split(' ').indexOf('glyphicon-ban-circle')).toBeGreaterThan(0);
      });

      promiseStateDirectiveElem.getAttribute('title').then(function (value) {
        expect(value).toBe('Oh no, rejected :(');
      });
    });

    it('should hide the element when the promise is set to null.', function () {
      var createBtnElem = ptor.findElement(protractor.By.id('createBtn'));
      var resolveBtnElem = ptor.findElement(protractor.By.id('resolveBtn'));
      var destroyBtnElem = ptor.findElement(protractor.By.id('destroyBtn'));

      promiseStateDirectiveElem.isDisplayed().then(function (value) {
        expect(value).toBe(false);
      });

      createBtnElem.click();
      resolveBtnElem.click();

      promiseStateDirectiveElem.isDisplayed().then(function (value) {
        expect(value).toBe(true);
      });

      destroyBtnElem.click();

      promiseStateDirectiveElem.isDisplayed().then(function (value) {
        expect(value).toBe(false);
      });
    });

    it('should show directly the resolved icon with the correct title' +
      ' when the promise is already resolved when set.', function () {
        var createResolvedBtnElem = ptor.findElement(protractor.By.id('createResolvedBtn'));

        promiseStateDirectiveElem.isDisplayed().then(function (value) {
          expect(value).toBe(false);
        });

        createResolvedBtnElem.click();

        promiseStateDirectiveElem.isDisplayed().then(function (value) {
          expect(value).toBe(true);
        });

        promiseStateDirectiveElem.getAttribute('class').then(function (value) {
          expect(value.split(' ').indexOf('glyphicon-ok')).toBeGreaterThan(0);
        });

        promiseStateDirectiveElem.getAttribute('title').then(function (value) {
          expect(value).toBe('Already resolved ;)');
        });
      });

    it('should not react to a previous promise being rejected' +
      ' when a new promise is set.', function () {
        var createBtnElem = ptor.findElement(protractor.By.id('createBtn'));
        var createResolvedBtnElem = ptor.findElement(protractor.By.id('createResolvedBtn'));
        var rejectBtnElem = ptor.findElement(protractor.By.id('rejectBtn'));

        promiseStateDirectiveElem.isDisplayed().then(function (value) {
          expect(value).toBe(false);
        });

        // create first promise
        createBtnElem.click();

        // create a second resolved promise
        createResolvedBtnElem.click();

        //reject the first promise
        rejectBtnElem.click();

        promiseStateDirectiveElem.isDisplayed().then(function (value) {
          expect(value).toBe(true);
        });

        promiseStateDirectiveElem.getAttribute('class').then(function (value) {
          expect(value.split(' ').indexOf('glyphicon-ok')).toBeGreaterThan(0);
        });

        promiseStateDirectiveElem.getAttribute('title').then(function (value) {
          expect(value).toBe('Already resolved ;)');
        });
      });
  });
}(protractor, describe, beforeEach, it, expect));