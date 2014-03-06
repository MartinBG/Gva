/*global module, protractor, by, require*/
(function (module, protractor, by, require){
  'use strict';

  var Q = require('q'),
      _ = require('lodash');

  function ScSearch(element, context) {
    this.element = element;
    this.context = context;
  }

  ScSearch.prototype.clickButton = function (btnAction) {
    return this.element.findElement(by.css('div[action="' + btnAction + '()"] button'))
                .then(function (btn) {
      return btn.click();
    });
  };

  ScSearch.prototype.getButton = function (btnName) {
    return this.element.findElement(by.css('div[name=' + btnName +'] button'));
  };

  ScSearch.prototype.addFilter = function (filterName) {
    var searchForm = this.element;

    return this.element.findElement(by.css('div[action=add] button')).then(function (addBtn) {
      return addBtn.click().then(function () {
        return searchForm.findElement(by.css('div[action=add] li.dpf-'+ filterName))
          .then(function (filter) {
          return filter.click();
        });
      });
    });
  };

  ScSearch.prototype.removeFilter = function (fieldName) {
    return this.element.findElement(by.css('div[name=' + fieldName + ']'))
          .then(function (container) {
      return container.findElement(by.css('button.close')).then(function (closeBtn) {
        closeBtn.click();
      });
    });
  };

  ScSearch.prototype.setFilter = function (fieldName, value) {
    var context = this.context;

    return this.element.findElement(by.css('div[name=' + fieldName + ']'))
              .then(function (container) {
      return container.getAttribute('type').then(function (type) {
        if (type === 'text') {
          return container.findElement(by.tagName('input')).then(function (input) {
            return input.sendKeys(value);
          });
        }

        if (type === 'select' || type === 'nomenclature') {
          return container.findElement(by.css('div.select2-container'))
                  .then(function (selectContainer) {
            return selectContainer.click().then(function () {
              return context.findElement(by.css('#select2-drop ul > li:nth-child(' + value + ')'))
                        .then(function (option) {
                return option.click();
              });
            });
          });
        }
      });
    });
  };

  ScSearch.prototype.getFilterContainer = function (filterName) {
    return this.element.findElement(protractor.By.name(filterName));
  };

  ScSearch.prototype.getFilterOptions = function (filterName) {
    var context = this.context;

    return this.element.findElement(by.css('div[name='+ filterName + '] div.select2-container'))
          .then(function (arrow) {
      return arrow.click().then(function () {
        return context.findElements(by.css('#select2-drop ul > li')).then(function (options) {
          return Q.all(_.map(options, function (option) {
            return option.getText();
          }));
        });
      });
    });
  };

  ScSearch.prototype.getVisibleFilters = function () {
    return this.element.findElements(by.css('div.form-group:not(.ng-hide)'))
                .then(function (filters) {
      return Q.all(_.map(filters, function (filter) {
        return filter.getAttribute('name');
      }));
    });
  };

  module.exports = ScSearch;
}(module, protractor, by, require));