/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../../scaffolding/pageObjects/scText'),
    ScTextarea = require('../../../../scaffolding/pageObjects/ScTextarea'),
    ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../../scaffolding/pageObjects/ScDate'),
    Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function DocMedPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.documentNumberPrefix = new ScText(
      context.findElement(by.input('model.documentNumberPrefix')));
    this.documentNumber = new ScText(
      context.findElement(by.input('model.documentNumber')));
    this.documentNumberSuffix = new ScText(
      context.findElement(by.input('model.documentNumberSuffix')));
    this.medClass = new ScNomenclature('model.medClass', context);
    this.documentPublisher = new ScNomenclature('model.documentPublisher', context);
    this.limitations = new ScNomenclature('model.limitations', context);
    this.documentDateValidFrom = new ScDate(
      context.findElement(by.css('div[name=documentDateValidFrom]')), context);
    this.documentDateValidTo = new ScDate(
      context.findElement(by.css('div[name=documentDateValidTo]')), context);
    this.notes = new ScTextarea(context.findElement(by.css('textarea')));
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  DocMedPO.prototype.save = function () {
    this.saveBtn.click();
  };

  DocMedPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = DocMedPO;
}(module, by, require));