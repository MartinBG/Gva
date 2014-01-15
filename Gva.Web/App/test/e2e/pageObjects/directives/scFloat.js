/*global module*/
(function (module) {
  'use strict';

  function ScFloat(element) {
    this.element = element;
  }

  ScFloat.prototype.get = function () {
    return this.element.getAttribute('value');
  };

  ScFloat.prototype.set = function (text) {
    this.element.clear();
    this.element.sendKeys(text);
  };

  module.exports = ScFloat;
}(module));