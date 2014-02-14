/*global protractor, describe, beforeEach, it, expect, require*/
(function (protractor, describe, beforeEach, it, expect, require) {
  'use strict';

  describe('scTabs directive', function () {
    var ptor = protractor.getInstance(),
        Page = require('../pageObjects/testbeds/tabsPO'),
        tabsPage;

    beforeEach(function () {
      ptor.get('#/test/sctabs');
      tabsPage = new Page(ptor);
    });

    it('should navigate to state when tab is clicked.', function () {
      tabsPage.scTabs.clickTab('Tab2');

      expect(tabsPage.currentStateSpan.getText()).toBe('root.scaffoldingTestbed.sctabs.tab2');
    });

    it('should display second navbar.', function () {
      expect(tabsPage.scTabs.getNavBar()).toEqual([['Tab1', 'Tab2', 'Tab3'], []]);

      tabsPage.scTabs.clickTab('Tab3');

      expect(tabsPage.scTabs.getNavBar())
        .toEqual([['Tab1', 'Tab2', 'Tab3'], ['Subtab1', 'Subtab2', 'Subtab3']]);
    });

    it ('should change active tab when navigating to other page', function () {
      tabsPage.changeState('tab2');

      expect(tabsPage.scTabs.getActiveTab()).toBe('Tab2');
    });

  });
}(protractor, describe, beforeEach, it, expect, require));
