/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../../scaffolding/pageObjects/ScDate'),
    ScDatatable = require('../../../../scaffolding/pageObjects/ScDatatable');

  function DocumentIdPO(context) {
    this.context = context;
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
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
    this.isSaveBtnDisabled = context.isElementPresent(by.css('button[disabled=disabled]'));
  }

  DocumentIdPO.prototype.getFile = function () {
    return this.context.findElement(by.className('test-single-file-span')).then(function (span) {
      return span.getText();
    });
  };

  DocumentIdPO.prototype.getApplications = function () {
    return this.context.findElement(by.css('div[filterable="false"]')).then(function (datatable) {
      var appDatatable = new ScDatatable(datatable);

      return appDatatable.getColumns('applicationId', 'applicationName');
    });
  };

  DocumentIdPO.prototype.save = function () {
    this.saveBtn.click();
  };

  DocumentIdPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = DocumentIdPO;
}(module, by, require));