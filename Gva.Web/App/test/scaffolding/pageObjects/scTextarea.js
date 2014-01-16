/*global module*/
(function (module){
  'use strict';

  function ScTextarea(element) {
    this.element = element;
  }

  ScTextarea.prototype.get = function () {
    return this.element.getValue();
  };

  ScTextarea.prototype.set = function (text) {
    this.element.clear();
    this.element.sendKeys(text);
  };

  module.exports = ScTextarea;
}(module));