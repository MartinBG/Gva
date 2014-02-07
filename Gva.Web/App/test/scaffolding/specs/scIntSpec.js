/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('scInt directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/testbeds/inputPO'),
        inputPage;

    beforeEach(function (){
      ptor.get('#/test/input');
      inputPage = new Page(ptor);
    });

    it('should set the text of the element to whatever integer is passed.', function() {
      expect(inputPage.intDirective.get()).toEqual('');
      inputPage.changeInt();
      expect(inputPage.intDirective.get()).toEqual('789');
    });

    it('should change the model to whatever integer is typed.', function() {
      expect(inputPage.getIntValue()).toEqual('');
      inputPage.intDirective.set('13456');
      expect(inputPage.getIntValue()).toEqual('13456');
    });

    it('should empty field on invalid user input.', function () {
      inputPage.intDirective.set('a134asddas56');
      expect(inputPage.intDirective.get()).toEqual('');
    });

    it('should try to parse user input.', function () {
      inputPage.intDirective.set('134asddas56');
      expect(inputPage.intDirective.get()).toEqual('134');
    });

    it('should represent the int as number.', function() {
      inputPage.intDirective.set('123');
      expect(inputPage.isInt()).toEqual('true');
    });

    it('should validate the model if changed.', function() {
      expect(inputPage.hasIntMaxErr()).toEqual('false');
      inputPage.changeInt();
      expect(inputPage.hasIntMaxErr()).toEqual('true');
    });

    it('should validate value for min.', function() {
      expect(inputPage.hasIntMinErr()).toEqual('false');
      inputPage.intDirective.set('-1');
      expect(inputPage.hasIntMinErr()).toEqual('true');
    });

    it('should validate value for max.', function() {
      expect(inputPage.hasIntMaxErr()).toEqual('false');
      inputPage.intDirective.set('501');
      expect(inputPage.hasIntMaxErr()).toEqual('true');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));
