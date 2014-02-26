/*global module, by*/
(function (module, by) {
  'use strict';

  function ScDate(element, context) {
    this.element = element;
    this.context = context;
  }

  ScDate.prototype.get = function () {
    return this.element.findElement(by.tagName('input')).then(function (dateInput) {
      return dateInput.getAttribute('value');
    });
  };

  ScDate.prototype.clear = function () {
    return this.element.findElement(by.tagName('input')).then(function (dateInput) {
      return dateInput.clear();
    });
  };

  ScDate.prototype.set = function (text) {
    return this.element.findElement(by.tagName('input')).then(function (dateInput) {
      return dateInput.clear().then(function () {
        return dateInput.sendKeys(text + '\t');
      });
    });
  };

  ScDate.prototype.selectSpecificDate = function () {
    var context = this.context;

    return this.element.findElement(by.className('glyphicon-calendar')).then(function (btn) {
      return btn.click().then(function () {
        return context.findElements(by.css('.day')).then(function (dayCells) {
          return dayCells[1].click();
        });
      });
    });
  };

  ScDate.prototype.clear = function () {
    return this.element.findElement(by.tagName('input')).then(function (dateInput) {
      dateInput.clear();
    });
  };

  module.exports = ScDate;
}(module, by));