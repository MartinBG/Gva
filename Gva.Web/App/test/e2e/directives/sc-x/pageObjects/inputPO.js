/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScText = require('../../../pageObjects/directives/scText'),
      ScTextarea = require('../../../pageObjects/directives/scTextarea');

  function InputPO(context) {
    this.textDirective = new ScText(context.findElement(by.name('textDir')));
    this.textInput = context.findElement(by.name('textInput'));

    this.textareaDirective = new ScTextarea(context.findElement(by.name('textareaDir')));
    this.textareaInput = context.findElement(by.name('textareaInput'));
  }

  module.exports = InputPO;
}(module, by, require));