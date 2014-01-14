/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Sc-button directive', function() {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/searchPO'),
        searchPage;

    beforeEach(function (){
      ptor.get('#/test/search');
      searchPage = new Page(ptor);
    });

    it('should execute the specified action when clicking the button.', function() {
      expect(searchPage.getNumClicks('btn1ClicksLbl')).toBe(0);
      searchPage.searchForm.clickButton('action1');
      expect(searchPage.getNumClicks('btn1ClicksLbl')).toBe(1);
    });

    it('should set the specified classes to the button.', function () {
      expect(searchPage.searchForm.getButton('btn1').getAttribute('class')).toContain('btn-sm');
      expect(searchPage.searchForm.getButton('btn1').getAttribute('class')).toContain('btn-info');
    });

    it('should set default classes to the button, when no other are specified.', function () {
      expect(searchPage.searchForm.getButton('btn2').getAttribute('class'))
        .toContain('btn-sm');
      expect(searchPage.searchForm.getButton('btn2').getAttribute('class'))
        .toContain('btn-default');
    });
  });
}(protractor, describe, beforeEach, it, expect, require));