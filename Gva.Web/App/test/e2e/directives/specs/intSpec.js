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

    it('should change the model to whatever integer is typed.', function() {
      var intLabelElem = ptor.findElement(protractor.By.name('intLbl'));

      intLabelElem.getText().then(function (value) {
        expect(value).toEqual('');
      });

      intDirectiveElem.sendKeys('13456');

      intLabelElem.getText().then(function (value) {
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
  });
}(protractor));