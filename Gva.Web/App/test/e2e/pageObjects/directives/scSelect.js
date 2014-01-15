/*global module, by*/
(function (module, by) {
  'use strict';

  function ScSelect(model, context) {
    var containerXPath = './/div[contains(@class,"select2-container")' +
                         ' and following-sibling::*[1][self::input' +
                         ' and @ng-model="' + model + '"]]';

    this.element = context.findElement(by.xpath(containerXPath));
    this.context = context;
  }

  ScSelect.prototype.get = function () {
    return this.element.findElement(by.className('select2-chosen')).then(function (span) {
      return span.getText();
    });
  };

  ScSelect.prototype.set = function (index) {
    var context = this.context;

    return this.element.click().then(function () {
      return context.findElement(by.css('#select2-drop > ul > li:nth-child(' + index + ')'))
            .then(function (option) {
        return option.click();
      });
    });
  };

  module.exports = ScSelect;
}(module, by));