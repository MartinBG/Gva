﻿/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Sc-date directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/inputPO'),
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
      expect(inputPage.dateLabel.getText()).toEqual('');
      inputPage.dateDirective.set('12.10.2013');
      expect(inputPage.dateLabel.getText()).toEqual('2013-10-12T00:00:00');
    });

    it('should allow clearing the date.', function () {
      inputPage.dateDirective.set('12.10.2013');
      inputPage.dateDirective.clear();
      expect(inputPage.dateLabel.getText()).toEqual('');
    });

    it('should change the model to whatever date is selected.', function() {
      var currentDate = new Date(),
          ISOdate = currentDate.toISOString().replace(/T.*/, 'T00:00:00');

      inputPage.dateDirective.selectCurrentDate();
      expect(inputPage.dateLabel.getText()).toEqual(ISOdate);
    });

    it('should validate user input.', function () {
      inputPage.dateDirective.set('alabala');
      expect(inputPage.dateDirective.get()).toEqual('');
      expect(inputPage.dateLabel.getText()).toEqual('');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));