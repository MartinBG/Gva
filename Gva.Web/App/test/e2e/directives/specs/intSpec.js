/*global protractor, describe, beforeEach, it, expect*/
(function (protractor) {
  'use strict';

  describe('Sc-int directive', function() {
    var ptor = protractor.getInstance(),
        intDirectiveElem;

    beforeEach(function (){
      ptor.get('#/test/input');

      intDirectiveElem = ptor.findElement(protractor.By.name('intDir'));
    });

    it('should set the text of the element to whatever integer is passed.', function() {
      var intBtnElem = ptor.findElement(protractor.By.name('intBtn'));

      intDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      intBtnElem.click();

      intDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('789');
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