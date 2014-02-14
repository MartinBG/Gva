/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../scaffolding/pageObjects/scText'),
    ScTextarea = require('../../../scaffolding/pageObjects/ScTextarea'),
    ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../scaffolding/pageObjects/scDate');

  function NewDocMedPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.documentNumberPrefix = new ScText(
      context.findElement(by.input('model.documentNumberPrefix')));
 /*   this.documentNumber = new ScText(
      context.findElement(by.input('model.documentNumber')));
    this.documentNumberSuffix = new ScText(
      context.findElement(by.input('model.documentNumberSuffix')));
    this.medClassType = new ScNomenclature('model.medClassType', context);
    this.documentPublisher = new ScNomenclature('model.documentPublisher', context);
    this.limitationsTypes = new ScNomenclature('model.limitationsTypes', context);
    this.documentDateValidFrom = new ScDate(
      context.findElement(by.css('div[name=documentDateValidFrom]')), context);
    this.documentDateValidTo = new ScDate(
      context.findElement(by.css('div[name=documentDateValidTo]')), context);
    this.notes = new ScTextarea(context.findElement(by.css('textarea')));
    this.bookPageNumber = new ScText(context.findElement(by.input('model.bookPageNumber')));
    this.pageCount = new ScText(context.findElement(by.input('model.pageCount')));
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));*/
  }

  NewDocMedPO.prototype.save = function () {
    this.saveBtn.click();
  };

  NewDocMedPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = NewDocMedPO;
}(module, by, require));