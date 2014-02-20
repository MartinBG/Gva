/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScText = require('../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../scaffolding/pageObjects/ScDate');

  function AddPartApplicationCasePO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.personDocIdType = new ScNomenclature('model.personDocumentIdType', context);
    this.documentNumber = new ScText(context.findElement(by.input('model.documentNumber')));
    this.valid = new ScNomenclature('model.valid', context);
    this.documentDateValidFrom =
      new ScDate(context.findElement(by.css('div[name=documentDateValidFrom]')), context);
    this.documentDateValidTo =
      new ScDate(context.findElement(by.css('div[name=documentDateValidTo]')), context);
    this.documentPublisher = new ScText(context.findElement(by.input('model.documentPublisher')));
    this.bookPageNumber = new ScText(context.findElement(by.input('model.bookPageNumber')));

    this.saveBtn = context.findElement(by.name('saveBtn'));
  }

  AddPartApplicationCasePO.prototype.save = function () {
    this.saveBtn.click();
  };

  module.exports = AddPartApplicationCasePO;
}(module, by, require));
