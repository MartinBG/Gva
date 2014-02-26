/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('scDate directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/testbeds/inputPO'),
        inputPage;

    beforeEach(function (){
      ptor.get('#/test/input');
      inputPage = new Page(ptor);
    });

    it('should set the date to whatever date is passed.', function() {
      expect(inputPage.dateDirective.get()).toEqual('');
      inputPage.changeDate();
      expect(inputPage.dateDirective.get()).toEqual('10.08.1990');
    });

    it('should change the model to whatever date is typed.', function () {
      expect(inputPage.getDateValue()).toEqual('');
      inputPage.dateDirective.set('12.10.2013');
      expect(inputPage.getDateValue()).toEqual('2013-10-12T00:00:00');
    });

    it('should allow clearing the date.', function () {
      inputPage.dateDirective.set('12.10.2013');
      inputPage.dateDirective.clear();
      expect(inputPage.getDateValue()).toEqual('');
    });

    it('should change the model to whatever date is selected.', function() {
      inputPage.changeDate();
      inputPage.dateDirective.selectSpecificDate();
      expect(inputPage.getDateValue()).toEqual('1990-07-30T00:00:00');
    });

    it('should validate user input.', function () {
      inputPage.dateDirective.set('alabala');
      expect(inputPage.dateDirective.get()).toEqual('');
      expect(inputPage.getDateValue()).toEqual('');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));
