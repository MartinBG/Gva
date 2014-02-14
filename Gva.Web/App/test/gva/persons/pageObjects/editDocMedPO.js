/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../scaffolding/pageObjects/scText'),
    ScTextarea = require('../../../scaffolding/pageObjects/ScTextarea'),
    ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../scaffolding/pageObjects/ScDate');

  function EditDocMedPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.documentNumberPrefix = new ScText(
      context.findElement(by.input('model.documentNumberPrefix')));
    this.documentNumber = new ScText(
      context.findElement(by.input('model.documentNumber')));
    this.documentNumberSuffix = new ScText(
      context.findElement(by.input('model.documentNumberSuffix')));
    this.medClassType = new ScNomenclature('model.medClassType', context);
    this.documentPublisher = new ScNomenclature('model.documentPublisher', context);
    this.limitationsTypes = new ScNomenclature('model.limitationsTypes', context);
    this.model.documentDateValidFrom = new ScDate(
      context.findElement(by.css('div[name=model.documentDateValidFrom]')), context);
    this.model.documentDateValidTo = new ScDate(
      context.findElement(by.css('div[name=model.documentDateValidTo]')), context);
    this.notes = new ScTextarea(context.findElement(by.css('textarea')));
    this.bookPageNumber = new ScText(context.findElement(by.input('model.bookPageNumber')));
    this.pageCount = new ScText(context.findElement(by.input('model.pageCount')));
    this.fileSpan = context.findElement(by.className('test-single-file-span'));
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  EditDocMedPO.prototype.save = function () {
    this.saveBtn.click();
  };

  EditDocMedPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = EditDocMedPO;
}(module, by, require));