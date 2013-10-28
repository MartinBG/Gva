/*global protractor, describe, beforeEach, it, expect*/
(function (protractor) {
  'use strict';

  describe('Sc-int directive', function() {
    var ptor = protractor.getInstance(),
        intDirectiveElem,
        intInputElem;

    beforeEach(function (){
      ptor.get('#/test/input');

      intDirectiveElem = ptor.findElement(protractor.By.name('intDir'));
      intInputElem = ptor.findElement(protractor.By.name('intInput'));
    });

    it('should set the text of the element to whatever integer is passed.', function() {
      intDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      intInputElem.sendKeys('789');

      intDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('789');
      });
    });

    it('should change the model to whatever integer is typed.', function() {
      intInputElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      intDirectiveElem.sendKeys('13456');

      intInputElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('13456');
      });
    });

    it('should validate user input.', function() {
      intDirectiveElem.sendKeys('a134asddas56\t');

      intDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      intDirectiveElem.sendKeys('134asddas56\t');

      intDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('134');
      });
    });

    it('should format user input.', function() {
      intDirectiveElem.sendKeys('133456\t');

      intDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toMatch(/133[\s,]456/);
      });

      intDirectiveElem.sendKeys('7\t');

      intDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toMatch(/1[\s,]334[\s,]567/);
      });
    });
  });
}(protractor));