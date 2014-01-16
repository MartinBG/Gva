/*global module, protractor, by*/
(function (module, protractor, by){
  'use strict';

  function ScSearch(element, context) {
    this.element = element;
    this.context = context;
  }

  ScSearch.prototype.clickButton = function (btnName) {
    return this.element.findElement(by.css('div[action="' + btnName + '()"] > button'))
                .then(function (btn) {
      return btn.click();
    });
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

  ScSearch.prototype.setField = function (fieldName, value) {
    var context = this.context;

    return this.element.findElement(by.css('div[name=' + fieldName + ']'))
              .then(function (container) {
      return container.getAttribute('type').then(function (type) {
        if (type === 'text') {
          return container.findElement(by.tagName('input')).then(function (input) {
            return input.sendKeys(value);
          });
        }

        if (type === 'select') {
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

  module.exports = ScSearch;
}(module, protractor, by));