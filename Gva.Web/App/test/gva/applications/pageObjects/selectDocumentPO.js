/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../scaffolding/pageObjects/scDatatable');

  function SelectDocumentPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.datatable = new ScDatatable(context.findElement(by.css('div[ng-model="docs"]')));
    this.tableBody = context.findElement(by.css('td'));
    this.firstSelectBtn = context
      .findElement(by.css('tbody tr:first-child button[name=selectBtn]'));
  }

  SelectDocumentPO.prototype.selectFirstDoc = function () {
    this.firstSelectBtn.click();
  };

  module.exports = SelectDocumentPO;
}(module, by, require));
