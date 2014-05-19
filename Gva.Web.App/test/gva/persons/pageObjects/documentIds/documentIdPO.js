/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../../scaffolding/pageObjects/ScDate'),
    Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function DocumentIdPO(context) {
    this.context = context;
    this.breadcrumb = new Breadcrumb(context);
    this.personDocumentIdType = new ScNomenclature('model.documentType', context);
    this.documentNumber = new ScText(context.findElement(by.input('model.documentNumber')));
    this.valid = new ScNomenclature('model.valid',context);
    this.documentDateValidFrom =
      new ScDate(context.findElement(by.css('div[name=documentDateValidFrom]')), context);
    this.documentDateValidTo =
      new ScDate(context.findElement(by.css('div[name=documentDateValidTo]')), context);
    this.documentPublisher = new ScText(context.findElement(by.input('model.documentPublisher')));
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
    this.isSaveBtnDisabled = context.isElementPresent(by.css('button[disabled=disabled]'));
  }

  DocumentIdPO.prototype.save = function () {
    this.saveBtn.click();
  };

  DocumentIdPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = DocumentIdPO;
}(module, by, require));