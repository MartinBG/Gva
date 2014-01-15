/*global module, by*/
(function (module, by){
  'use strict';

  function ScFiles(element, context) {
    this.element = element;
    this.context = context;
  }

  ScFiles.prototype.get = function () {
    return this.element.findElement(by.className('form-control')).getText();
  };

  ScFiles.prototype.openModal = function () {
    this.element.findElement(by.className('glyphicon-file')).click();
  };

  ScFiles.prototype.closeModal = function () {
    this.context.findElement(by.className('test-button-close')).click();
  };

  ScFiles.prototype.addFile = function (text) {
    this.context.findElement(by.className('test-file-upload-button')).sendKeys(text);
  };

  ScFiles.prototype.uploadFiles = function () {
    this.context.findElement(by.className('btn-primary')).click();
  };

  module.exports = ScFiles;
}(module, by));