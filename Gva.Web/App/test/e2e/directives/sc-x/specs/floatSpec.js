/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('Sc-float directive', function() {
    var ptor = protractor.getInstance(),
        floatDirectiveElem;

    beforeEach(function (){
      ptor.get('#/test/input');

      floatDirectiveElem = ptor.findElement(protractor.By.name('floatDir'));
    });

    it('should set the text of the element to whatever number is passed.', function() {
      var floatBtnElem = ptor.findElement(protractor.By.name('floatBtn'));

      floatDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      floatBtnElem.click();
      floatDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('789,12');
      });
    });

    it('should change the model to whatever number is typed.', function() {
      var floatLabelElem = ptor.findElement(protractor.By.name('floatLbl'));

      floatLabelElem.getText().then(function (value) {
        expect(value).toEqual('');
      });

      floatDirectiveElem.sendKeys('134.56');
      floatLabelElem.getText().then(function (value) {
        expect(value).toEqual('134.56');
      });
    });

    it('should empty field on invalid user input.', function() {
      floatDirectiveElem.sendKeys('a134asddas56\t');

      floatDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });
    });

    it('should try to parse user input.', function() {
      floatDirectiveElem.sendKeys('134.12asddas56\t');

      floatDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('134,12');
      });
    });

    it('should round user input.', function() {
      floatDirectiveElem.sendKeys('13345.656\t');

      floatDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('13345,66');
      });
    });
    
    it('should represent the float as number.', function() {
      var isFloatLabelElem = ptor.findElement(protractor.By.name('isFloatLbl')),
          isFloatBtnElem = ptor.findElement(protractor.By.name('isFloatBtn'));

      floatDirectiveElem.sendKeys('123.449999');
      isFloatBtnElem.click();

      isFloatLabelElem.getText().then(function (value) {
        expect(value).toEqual('true');
      });
    });
  });
}(protractor, describe, beforeEach, it, expect));