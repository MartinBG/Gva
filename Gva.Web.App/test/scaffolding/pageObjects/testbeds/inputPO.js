﻿/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScText = require('../scText'),
      ScTextarea = require('../scTextarea'),
      ScDate = require('../scDate'),
      ScInt = require('../scInt'),
      ScFloat = require('../scFloat');

  function InputPO(context) {
    this.textDirective = new ScText(context.findElement(by.name('textDir')));
    this.textInput = context.findElement(by.name('textInput'));

    this.textareaDirective = new ScTextarea(context.findElement(by.name('textareaDir')));
    this.textareaInput = context.findElement(by.name('textareaInput'));

    this.dateDirective = new ScDate(context.findElement(by.name('dateDir')), context);
    this.dateLabel = context.findElement(by.id('dateLbl'));
    this.dateBtn = context.findElement(by.name('dateBtn'));

    this.intDirective = new ScInt(context.findElement(by.name('intDir')));
    this.intLabel = context.findElement(by.id('intLbl'));
    this.intBtn = context.findElement(by.name('intBtn'));
    this.isIntLbl = context.findElement(by.id('isIntLbl'));
    this.hasIntMinErrLbl = context.findElement(by.id('hasIntMinErrLbl'));
    this.hasIntMaxErrLbl = context.findElement(by.id('hasIntMaxErrLbl'));
    this.isIntBtn = context.findElement(by.name('isIntBtn'));

    this.floatDirective = new ScFloat(context.findElement(by.name('floatDir')));
    this.floatLabel = context.findElement(by.id('floatLbl'));
    this.floatBtn = context.findElement(by.name('floatBtn'));
    this.isFloatLbl = context.findElement(by.id('isFloatLbl'));
    this.hasFloatMinErrLbl = context.findElement(by.id('hasFloatMinErrLbl'));
    this.hasFloatMaxErrLbl = context.findElement(by.id('hasFloatMaxErrLbl'));
    this.isFloatBtn = context.findElement(by.name('isFloatBtn'));
  }

  InputPO.prototype.changeDate = function () {
    return this.dateBtn.click();
  };

  InputPO.prototype.getDateValue = function () {
    return this.dateLabel.getText();
  };

  InputPO.prototype.changeInt = function () {
    return this.intBtn.click();
  };

  InputPO.prototype.getIntValue = function () {
    return this.intLabel.getText();
  };

  InputPO.prototype.isInt = function () {
    var isIntLbl = this.isIntLbl;

    return this.isIntBtn.click().then(function () {
      return isIntLbl.getText();
    });
  };

  InputPO.prototype.hasIntMinErr = function () {
    return this.hasIntMinErrLbl.getText();
  };

  InputPO.prototype.hasIntMaxErr = function () {
    return this.hasIntMaxErrLbl.getText();
  };

  InputPO.prototype.changeFloat = function () {
    return this.floatBtn.click();
  };

  InputPO.prototype.getFloatValue = function () {
    return this.floatLabel.getText();
  };

  InputPO.prototype.isFloat = function () {
    var isFloatLbl = this.isFloatLbl;

    return this.isFloatBtn.click().then(function () {
      return isFloatLbl.getText();
    });
  };

  InputPO.prototype.hasFloatMinErr = function () {
    return this.hasFloatMinErrLbl.getText();
  };

  InputPO.prototype.hasFloatMaxErr = function () {
    return this.hasFloatMaxErrLbl.getText();
  };

  module.exports = InputPO;
}(module, by, require));