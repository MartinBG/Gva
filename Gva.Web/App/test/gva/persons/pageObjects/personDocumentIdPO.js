/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../scaffolding/pageObjects/scDate');

  function PersonDocumentIdPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.personDocumentIdType = new ScNomenclature('model.personDocumentIdType', context);
    this.valid = new ScNomenclature(
      'model.valid',
      context.findElement(by.name('personDocumentIdForm'))
      );
    this.bookPageNumber = new ScText(context.findElement(by.input('model.bookPageNumber')));
    this.documentNumber = new ScText(context.findElement(by.input('model.documentNumber')));
    this.documentPublisher = new ScText(context.findElement(by.input('model.documentPublisher')));
    this.documentDateValidFrom = new ScDate(
      context.findElement(by.css('div[name=documentDateValidFrom]')),
      context
      );
    this.documentDateValidTo = new ScDate(context.findElement(
      by.css('div[name=documentDateValidTo]')),
      context
      );
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
    this.isSaveBtnDisabled = context.isElementPresent(by.css('button[disabled=disabled]'));
  }

  PersonDocumentIdPO.prototype.save = function () {
    this.saveBtn.click();
  };

  PersonDocumentIdPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = PersonDocumentIdPO;
}(module, by, require));