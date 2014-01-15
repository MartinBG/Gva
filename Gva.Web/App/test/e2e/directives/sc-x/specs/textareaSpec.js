/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Sc-textarea directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/inputPO'),
        inputPage;

    beforeEach(function (){
      ptor.get('#/test/input');
      inputPage = new Page(ptor);
    });

    it('should set the textarea of the element to whatever is passed.', function () {
      expect(inputPage.textareaDirective.get()).toEqual('');
      inputPage.textareaInput.sendKeys('text');
      expect(inputPage.textareaDirective.get()).toEqual('text');
    });

    it('should change the model to whatever is typed.', function () {
      expect(inputPage.textareaInput.getAttribute('value')).toEqual('');
      inputPage.textareaDirective.set('text');
      expect(inputPage.textareaInput.getAttribute('value')).toEqual('text');
    });

    it('should set properly rows and cols attributes.', function () {
      expect(inputPage.textareaDirective.rows).toEqual('5');
      expect(inputPage.textareaDirective.columns).toEqual('40');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));