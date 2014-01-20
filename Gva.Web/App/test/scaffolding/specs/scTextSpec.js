/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('scText directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/testbeds/inputPO'),
        inputPage;

    beforeEach(function (){
      ptor.get('#/test/input');
      inputPage = new Page(ptor);
    });

    it('should set the text of the element to whatever is passed.', function () {
      expect(inputPage.textDirective.get()).toEqual('');
      inputPage.textInput.sendKeys('text');
      expect(inputPage.textDirective.get()).toEqual('text');
    });

    it('should change the model to whatever is typed.', function () {
      expect(inputPage.textInput.getAttribute('value')).toEqual('');
      inputPage.textDirective.set('text');
      expect(inputPage.textInput.getAttribute('value')).toEqual('text');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));
