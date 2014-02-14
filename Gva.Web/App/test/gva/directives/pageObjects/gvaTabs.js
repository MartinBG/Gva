/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var Q = require('q'),
      _ = require('lodash');

  function GvaTabs(element) {
    this.element = element;
  }

  GvaTabs.prototype.clickTab = function (tabName) {
    return this.element.findElement(by.css('li.gvat-' + tabName)).then(function (tab) {
      return tab.click();
    });
  };

  GvaTabs.prototype.getActiveTab = function () {
    return this.element.findElement(by.css('li.active')).then(function (activeTab) {
      return activeTab.getText();
    });
  };

  GvaTabs.prototype.getNavBar = function () {
    return this.element.findElements(by.tagName('ul')).then(function (navbars) {
      return Q.all(_.map(navbars, function (navbar) {
        return navbar.findElements(by.tagName('li'));
      })).then(function (resolvedNavbars) {
        return Q.all(_.map(resolvedNavbars, function (resolvedNavbar) {
          return Q.all(_.map(resolvedNavbar, function (tab) {
            return tab.getText();
          }));
        }));
      });
    });
  };

  module.exports = GvaTabs;
}(module, by, require));