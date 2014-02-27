/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature');

  function DocumentPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.docTypeGroup = new ScNomenclature('docModel.docTypeGroup', context);
    this.docType = new ScNomenclature('docModel.docType', context);
    this.docSubject = new ScText(context.findElement(by.input('docModel.doc.docSubject')));

    this.createBtn = context.findElement(by.name('createBtn'));
    this.registerBtn = context.findElement(by.name('registerBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  DocumentPO.prototype.create = function () {
    this.createBtn.click();
  };

  DocumentPO.prototype.register = function () {
    this.registerBtn.click();
  };

  DocumentPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = DocumentPO;
}(module, by, require));
