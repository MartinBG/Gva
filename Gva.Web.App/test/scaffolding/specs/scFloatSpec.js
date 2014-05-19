/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('scFloat directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/testbeds/inputPO'),
        inputPage;

    beforeEach(function (){
      ptor.get('#/test/input');
      inputPage = new Page(ptor);
    });

    it('should set the text of the element to whatever number is passed.', function() {
      expect(inputPage.floatDirective.get()).toEqual('');
      inputPage.changeFloat();
      expect(inputPage.floatDirective.get()).toEqual('789,12');
    });

    it('should change the model to whatever number is typed.', function() {
      expect(inputPage.getFloatValue()).toEqual('');
      inputPage.floatDirective.set('134.56');
      expect(inputPage.getFloatValue()).toEqual('134.56');
    });

    it('should empty field on invalid user input.', function () {
      inputPage.floatDirective.set('a134asddas56');
      expect(inputPage.floatDirective.get()).toEqual('');
    });

    it('should try to parse user input.', function () {
      inputPage.floatDirective.set('134.12asddas56');
      expect(inputPage.floatDirective.get()).toEqual('134,12');
    });

    it('should round user input.', function () {
      inputPage.floatDirective.set('13345.656');
      expect(inputPage.floatDirective.get()).toEqual('13345,66');
    });
    
    it('should represent the float as number.', function() {
      inputPage.floatDirective.set('123.449999');
      expect(inputPage.isFloat()).toEqual('true');
    });

    it('should validate the model if changed.', function() {
      expect(inputPage.hasFloatMaxErr()).toEqual('false');
      inputPage.changeFloat();
      expect(inputPage.hasFloatMaxErr()).toEqual('true');
    });

    it('should validate value for min.', function() {
      expect(inputPage.hasFloatMinErr()).toEqual('false');
      inputPage.floatDirective.set('-1');
      expect(inputPage.hasFloatMinErr()).toEqual('true');
    });

    it('should validate value for max.', function() {
      expect(inputPage.hasFloatMaxErr()).toEqual('false');
      inputPage.floatDirective.set('501');
      expect(inputPage.hasFloatMaxErr()).toEqual('true');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));
