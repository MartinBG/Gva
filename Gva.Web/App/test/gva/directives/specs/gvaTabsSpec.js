/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('Gva-tabs directive', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/testbeds/gvaTabsPO'),
        tabsPage;

    beforeEach(function () {
      ptor.get('#/test/gvatabs');
      tabsPage = new Page(ptor);
    });

    it('should navigate to state when tab is clicked.', function () {
      tabsPage.gvaTabs.clickTab('Tab2');

      expect(tabsPage.currentStateSpan.getText()).toBe('root.scaffoldingTestbed.gvatabs.tab2');
    });

    it('should display second navbar.', function () {
      expect(tabsPage.gvaTabs.getNavBar()).toEqual([['Tab1', 'Tab2', 'Tab3'], []]);

      tabsPage.gvaTabs.clickTab('Tab3');

      expect(tabsPage.gvaTabs.getNavBar())
        .toEqual([['Tab1', 'Tab2', 'Tab3'], ['Subtab1', 'Subtab2', 'Subtab3']]);
    });

    it ('should change active tab when navigating to other page', function () {
      tabsPage.changeState('tab2');

      expect(tabsPage.gvaTabs.getActiveTab()).toBe('Tab2');
    });

  });
}(protractor, describe, beforeEach, it, expect, require));
