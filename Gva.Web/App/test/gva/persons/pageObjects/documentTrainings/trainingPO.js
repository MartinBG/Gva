/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../../scaffolding/pageObjects/scDate'),
    ScInt = require('../../../../scaffolding/pageObjects/scInt'),
    ScTextarea = require('../../../../scaffolding/pageObjects/ScTextarea'),
    Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function PersonDocumentTrainingPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.staffType = new ScNomenclature('model.staffType', context);
    this.documentNumber = new ScText(context.findElement(by.input('model.documentNumber')));
    this.documentPersonNumber =
      new ScInt(context.findElement(by.input('model.documentPersonNumber')));
    this.documentDateValidFrom =
      new ScDate(context.findElement(by.css('div[name=documentDateValidFrom]')), context);
    this.documentDateValidTo =
      new ScDate(context.findElement(by.css('div[name=documentDateValidTo]')), context);
    this.documentPublisher = new ScText(context.findElement(by.input('model.documentPublisher')));
    this.personOtherDocumentType = new ScNomenclature('model.personOtherDocumentType', context);
    this.personOtherDocumentRole = new ScNomenclature('model.personOtherDocumentRole', context);
    this.valid = new ScNomenclature('model.valid', context);
    this.notes = new ScTextarea(context.findElement(by.model('model.notes')));
    this.bookPageNumber = new ScText(context.findElement(by.input('model.bookPageNumber')));
    this.pageCount = new ScInt(context.findElement(by.input('model.pageCount')));


    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
    this.isSaveBtnDisabled = context.isElementPresent(by.css('button[disabled=disabled]'));
  }

  PersonDocumentTrainingPO.prototype.save = function () {
    this.saveBtn.click();
  };

  PersonDocumentTrainingPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = PersonDocumentTrainingPO;
}(module, by, require));