/*global protractor, describe, beforeEach, it, expect*/
(function (protractor) {
  'use strict';

  describe('Sc-text directive', function() {
    var ptor = protractor.getInstance(),
        textDirectiveElem,
        textInputElem;

    beforeEach(function (){
      ptor.get('#/test/input');

      textDirectiveElem = ptor.findElement(protractor.By.name('textDir'));
      textInputElem = ptor.findElement(protractor.By.name('textInput'));
    });

    it('should set the text of the element to whatever is passed.', function() {
      textDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      textInputElem.sendKeys('text');

      textDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('text');
      });
    });

    it('should change the model to whatever is typed.', function() {
      textInputElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      textDirectiveElem.sendKeys('text');

      textInputElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('text');
      });
    });
  });
}(protractor));