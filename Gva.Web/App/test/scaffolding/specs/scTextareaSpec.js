/*global protractor, describe, beforeEach, it, expect*/
(function (protractor, describe, beforeEach, it, expect) {
  'use strict';

  describe('scTextarea directive', function() {
    var ptor = protractor.getInstance(),
        textareaDirectiveElem,
        textareaInputElem;

    beforeEach(function (){
      ptor.get('#/test/input');

      textareaDirectiveElem = ptor.findElement(protractor.By.name('textareaDir'));
      textareaInputElem = ptor.findElement(protractor.By.name('textareaInput'));
    });

    it('should set the textarea of the element to whatever is passed.', function() {
      textareaDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      textareaInputElem.sendKeys('text');

      textareaDirectiveElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('text');
      });
    });

    it('should change the model to whatever is typed.', function() {
      textareaInputElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('');
      });

      textareaDirectiveElem.sendKeys('text');

      textareaInputElem.getAttribute('value').then(function (value) {
        expect(value).toEqual('text');
      });
    });

    it('should set properly rows and cols attributes.', function() {
      textareaInputElem.getAttribute('rows').then(function (value) {
        expect(value).toEqual('5');
      });

      textareaInputElem.getAttribute('cols').then(function (value) {
        expect(value).toEqual('40');
      });
    });
  });
}(protractor, describe, beforeEach, it, expect));