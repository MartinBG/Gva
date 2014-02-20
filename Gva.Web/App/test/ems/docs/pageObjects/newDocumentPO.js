/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature'),
    ScInt = require('../../../scaffolding/pageObjects/scInt');

  function DocumentPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.caseRegUri = new ScText(context.findElement(by.input('doc.caseRegUri')));
    this.docTypeGroup = new ScNomenclature('docTypeGroup', context);
    this.docType = new ScNomenclature('docType', context);
    this.docSubject = new ScText(context.findElement(by.input('doc.docSubject')));
    this.numberOfDocs = new ScInt(context.findElement(by.input('doc.numberOfDocs')));

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
