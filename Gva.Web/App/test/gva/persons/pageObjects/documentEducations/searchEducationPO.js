/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable');

  function SearchDocEduPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.datatable = new ScDatatable(
      context.findElement(by.css('div[ng-model="documentEducations"]')));
    this.tableBody = context.findElement(by.css('td'));
  }

  module.exports = SearchDocEduPO;
}(module, by, require));