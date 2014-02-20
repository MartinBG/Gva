/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../scaffolding/pageObjects/scDatatable');

  function SearchCorrPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.datatable = new ScDatatable(context.findElement(by.css('div[ng-model="corrs"]')));
    this.tableBody = context.findElement(by.css('td'));
    this.firstEditBtn = context
      .findElement(by.css('tbody tr:first-child button[name=viewButton]'));
  }

  module.exports = SearchCorrPO;
}(module, by, require));
