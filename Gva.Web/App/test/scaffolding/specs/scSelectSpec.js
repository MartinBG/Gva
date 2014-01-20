/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('scSelect directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/testbeds/selectPO'),
        selectPage;

    beforeEach(function (){
      ptor.get('#/test/select');
      selectPage = new Page(ptor);
    });

    it('should select the passed option.', function() {
      expect(selectPage.selectDirective.get()).toEqual('');
      selectPage.selectOption('option2');
      expect(selectPage.selectDirective.get()).toEqual('option2');
    });

    it('should change the model to whatever is selected.', function() {
      expect(selectPage.isSelected('none')).toBeTruthy();
      selectPage.selectDirective.set(3);
      expect(selectPage.isSelected('option3')).toBeTruthy();
    });
  });
}(protractor, describe, beforeEach, it, expect, require));
