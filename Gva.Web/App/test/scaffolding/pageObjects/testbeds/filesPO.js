/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScFiles = require('../scFiles.js');

  function Files(context) {
    this.filesDirective = new ScFiles(context.findElement(by.name('filesDir')), context);
    this.singleFileDirective = new ScFiles(context.findElement(by.name('fileDir')), context);
    this.multipleFilesBtn = context.findElement(by.name('multipleFilesBtn'));
    this.singleFileBtn = context.findElement(by.name('singleFileBtn'));
    this.singleFileWithDelayBtn = context.findElement(by.name('loadFileWithDelayBtn'));
    this.deselectFilesBtn = context.findElement(by.name('noFilesBtn'));
  }

  Files.prototype.selectMultipleFiles = function () {
    return this.multipleFilesBtn.click();
  };

  Files.prototype.selectSingleFile = function () {
    return this.singleFileBtn.click();
  };

  Files.prototype.selectSingleFileWithDelay = function () {
    return this.singleFileWithDelayBtn.click();
  };

  Files.prototype.deselectFiles = function () {
    return this.deselectFilesBtn.click();
  };

  module.exports = Files;
}(module, by, require));