/*global module*/
(function (module){
  'use strict';

  function ScTextarea(element) {
    this.element = element;
    this.rows = element.getAttribute('rows');
    this.columns = element.getAttribute('cols');
  }

  ScTextarea.prototype.get = function () {
    return this.element.getAttribute('value');
  };

  ScTextarea.prototype.set = function (text) {
    this.element.clear();
    this.element.sendKeys(text);
  };

  module.exports = ScTextarea;
}(module));