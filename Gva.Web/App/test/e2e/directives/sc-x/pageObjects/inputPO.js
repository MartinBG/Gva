/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScText = require('../../../pageObjects/directives/scText'),
      ScTextarea = require('../../../pageObjects/directives/scTextarea'),
      ScDate = require('../../../pageObjects/directives/scDate');

  function InputPO(context) {
    this.textDirective = new ScText(context.findElement(by.name('textDir')));
    this.textInput = context.findElement(by.name('textInput'));

    this.textareaDirective = new ScTextarea(context.findElement(by.name('textareaDir')));
    this.textareaInput = context.findElement(by.name('textareaInput'));

    this.dateDirective = new ScDate(context.findElement(by.name('dateDir')), context);
    this.dateLabel = context.findElement(by.name('dateLbl'));
    this.dateBtn = context.findElement(by.name('dateBtn'));
  }

  InputPO.prototype.changeDate = function () {
    return this.dateBtn.click();
  };

  module.exports = InputPO;
}(module, by, require));