/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('scPromiseState directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/testbeds/promiseStatePO'),
        promiseStatePage;

    beforeEach(function (){
      ptor.get('#/test/promise');
      promiseStatePage = new Page(ptor);
    });

    it('should show the loading icon when the promise is not null and not resolved.', function() {
      expect(promiseStatePage.promiseStateDir.isActive()).toBe(false);

      promiseStatePage.create();
      expect(promiseStatePage.promiseStateDir.isActive()).toBe(true);
      expect(promiseStatePage.promiseStateDir.isPending()).toBe(true);
    });

    it('should show the resolved icon with the correct title' +
      ' when the promise is resolved.', function () {
      expect(promiseStatePage.promiseStateDir.isActive()).toBe(false);

      promiseStatePage.create();
      promiseStatePage.resolve();

      expect(promiseStatePage.promiseStateDir.isActive()).toBe(true);
      expect(promiseStatePage.promiseStateDir.isResolved()).toBe(true);
      expect(promiseStatePage.promiseStateDir.title()).toEqual('Hurray, resolved :)');
    });

    it('should show the rejected icon with the correct title' +
      ' when the promise is rejected.', function () {
      expect(promiseStatePage.promiseStateDir.isActive()).toBe(false);

      promiseStatePage.create();
      promiseStatePage.reject();

      expect(promiseStatePage.promiseStateDir.isActive()).toBe(true);
      expect(promiseStatePage.promiseStateDir.isRejected()).toBe(true);
      expect(promiseStatePage.promiseStateDir.title()).toEqual('Oh no, rejected :(');
    });

    it('should hide the element when the promise is set to null.', function () {
      expect(promiseStatePage.promiseStateDir.isActive()).toBe(false);

      promiseStatePage.create();
      promiseStatePage.resolve();
      expect(promiseStatePage.promiseStateDir.isActive()).toBe(true);

      promiseStatePage.destroy();
      expect(promiseStatePage.promiseStateDir.isActive()).toBe(false);
    });

    it('should show directly the resolved icon with the correct title' +
      ' when the promise is already resolved when set.', function () {
      expect(promiseStatePage.promiseStateDir.isActive()).toBe(false);

      promiseStatePage.createResolved();
      expect(promiseStatePage.promiseStateDir.isActive()).toBe(true);
      expect(promiseStatePage.promiseStateDir.isResolved()).toBe(true);
      expect(promiseStatePage.promiseStateDir.title()).toEqual('Already resolved ;)');
    });

    it('should not react to a previous promise being rejected' +
      ' when a new promise is set.', function () {
      expect(promiseStatePage.promiseStateDir.isActive()).toBe(false);

      // create first promise
      promiseStatePage.create();

      // create a second resolved promise
      promiseStatePage.createResolved();

      //reject the first promise
      promiseStatePage.reject();

      expect(promiseStatePage.promiseStateDir.isActive()).toBe(true);
      expect(promiseStatePage.promiseStateDir.isResolved()).toBe(true);
      expect(promiseStatePage.promiseStateDir.title()).toEqual('Already resolved ;)');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));
