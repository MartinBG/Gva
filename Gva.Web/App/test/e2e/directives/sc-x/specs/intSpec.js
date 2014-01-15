/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Sc-int directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/inputPO'),
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
  });
}(protractor, describe, beforeEach, it, expect, require));