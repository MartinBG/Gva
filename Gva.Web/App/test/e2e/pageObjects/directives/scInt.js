/*global module*/
(function (module) {
  'use strict';

  function ScInt(element) {
    this.element = element;
  }

  ScInt.prototype.get = function () {
    return this.element.getAttribute('value');
  };

  ScInt.prototype.set = function (text) {
    this.element.clear();
    this.element.sendKeys(text);
  };

  module.exports = ScInt;
}(module));