/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../scaffolding/pageObjects/ScDate'),
    ScDatatable = require('../../../scaffolding/pageObjects/ScDatatable');

  function DocumentIdPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.personDocumentIdType = new ScNomenclature('model.personDocumentIdType', context);
    this.bookPageNumber = new ScText(context.findElement(by.input('model.bookPageNumber')));
    this.pageCount = new ScText(context.findElement(by.input('model.pageCount')));
    this.documentNumber = new ScText(context.findElement(by.input('model.documentNumber')));
    this.valid = new ScNomenclature('model.valid',context);
    this.documentDateValidFrom =
      new ScDate(context.findElement(by.css('div[name=documentDateValidFrom]')), context);
    this.documentDateValidTo =
      new ScDate(context.findElement(by.css('div[name=documentDateValidTo]')), context);
    this.documentPublisher = new ScText(context.findElement(by.input('model.documentPublisher')));
    this.fileSpan = context.findElement(by.className('test-single-file-span'));
    this.datatable = new ScDatatable(context.findElement(by.css('div[filterable="false"]')));
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  DocumentIdPO.prototype.save = function () {
    this.saveBtn.click();
  };

  DocumentIdPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = DocumentIdPO;
}(module, by, require));