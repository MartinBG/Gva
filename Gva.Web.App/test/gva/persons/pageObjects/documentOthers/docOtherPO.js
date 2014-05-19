/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../../scaffolding/pageObjects/scText'),
    ScTextarea = require('../../../../scaffolding/pageObjects/ScTextarea'),
    ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../../scaffolding/pageObjects/ScDate'),
    Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function DocOtherPO(context) {
    this.breadcrumb = new Breadcrumb(context);

    this.documentNumber = new ScText(
      context.findElement(by.input('model.documentNumber')));

    this.documentPersonNumber = new ScText(
      context.findElement(by.input('model.documentPersonNumber')));

    this.documentPublisher = new ScText(
      context.findElement(by.input('model.documentPublisher')));

    this.documentDateValidFrom = new ScDate(
      context.findElement(by.css('div[name=documentDateValidFrom]')), context);

    this.documentDateValidTo = new ScDate(
      context.findElement(by.css('div[name=documentDateValidTo]')), context);

    this.personOtherDocumentType = new ScNomenclature('model.documentType', context);
    this.personOtherDocumentRole = new ScNomenclature('model.documentRole', context);

    this.notes = new ScTextarea(context.findElement(by.css('textarea')));
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  DocOtherPO.prototype.save = function () {
    this.saveBtn.click();
  };

  DocOtherPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = DocOtherPO;
}(module, by, require));