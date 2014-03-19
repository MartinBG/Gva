/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var Q = require('q'),
      _ = require('lodash');

  function ScNomenclature(model, context) {
    /*jshint -W101*/
    /* jshint -W109*/
    var containerXPath = ".//div[contains(@class,'select2-container') and following-sibling::*[1][self::input and @ng-model='" + model + "']]";
    this.dropdownInputXPath = "//div[@id='select2-drop']/div[contains(@class, 'select2-search')]/input[contains(@class, 'select2-input')]";
    this.dropdownResultsXpath = "//div[@id='select2-drop']/ul[contains(@class, 'select2-results')]/li[contains(@class, 'select2-result') and contains(@class, 'select2-result-selectable') and not(contains(@class, 'select2-selected'))]";
    this.dropdownResultByTextXpath = this.dropdownResultsXpath + "/div/text()[normalize-space(.)='$TERM$']/parent::*";
    /* jshint +W101*/
    /* jshint +W109*/
    this.context = context;
    this.container = this.context.findElement(by.xpath(containerXPath));
    this.multiple = this.container.getAttribute('class').then(function (classes) {
      //multiple if class contains 'select2-container-multi'
      return (classes || '').indexOf('select2-container-multi') !== -1;
    });
  }

  ScNomenclature.prototype.get = function () {
    var self = this;
    return self.multiple.then(function (multiple) {
      if (multiple) {
        return self.container
          .findElements(by.className('select2-search-choice'))
          .then(function (lis) {
            return Q.all(_.map(lis, function (li) {
              return li.getText();
            }));
          });
      } else {
        return self.container.findElement(by.className('select2-chosen')).then(function (span) {
          return span.getText();
        });
      }
    });
  };

  ScNomenclature.prototype.dropdownInput = function () {
    return this.context.findElement(by.xpath(this.dropdownInputXPath));
  };
  
  ScNomenclature.prototype.getDropdownResults = function () {
    var self = this;
    return self.container.click().then(function () {
      return self.context
        .findElements(by.xpath(self.dropdownResultsXpath))
        .then(function (results) {
          return Q.all(_.map(results, function (result) {
            return result.getText();
          }));
        });
    });
  };

  ScNomenclature.prototype.set = function (text) {
    var context = this.context,
        container = this.container,
        dropdownResultByTextXpath = this.dropdownResultByTextXpath.replace('$TERM$', text);

    return this.multiple.then(function (multiple) {
      var clickPromise;

      if (multiple) {
        clickPromise = container.findElement(by.css('li.select2-search-field input')).click();
      } else {
        clickPromise = container.click();
      }

      return clickPromise.then(function () {
        return context.findElement(by.xpath(dropdownResultByTextXpath)).then(function (result) {
          return result.click();
        });
      });
    });
  };

  ScNomenclature.prototype.clear = function () {
    return this.container
      .findElement(by.css('.select2-search-choice-close'))
      .then(function (deselectBtn) {
        return deselectBtn.click();
      });
  };

  module.exports = ScNomenclature;
}(module, by, require));