/*global module, by*/
(function (module, by) {
  'use strict';

  function Breadcrumb(context) {
    this.element = context.findElement(by.css('ul[class*="breadcrumb"] li:last-child'));
  }

  Breadcrumb.prototype.get = function () {
    return this.element.getText();
  };

  module.exports = Breadcrumb;
}(module, by));