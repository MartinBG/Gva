﻿/*global module*/
(function (module){
  'use strict';

  function ScText(element) {
    this.element = element;
  }

  ScText.prototype.get = function () {
    return this.element.getAttribute('value');
  };

  ScText.prototype.set = function (text) {
    this.element.clear();
    this.element.sendKeys(text);
  };

  module.exports = ScText;
}(module));