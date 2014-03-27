/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../scaffolding/pageObjects/scDatatable');

  function SearchDocumentPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.datatable = new ScDatatable(context.findElement(by.css('div[items="docs"]')));
    this.tableBody = context.findElement(by.css('td'));
    this.firstEditBtn = context
      .findElement(by.css('tbody tr:first-child button[name=viewButton]'));
  }

  module.exports = SearchDocumentPO;
}(module, by, require));
