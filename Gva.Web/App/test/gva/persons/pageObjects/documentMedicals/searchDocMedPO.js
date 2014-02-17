/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable');

  function SearchDocMedPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.datatable = new ScDatatable(
      context.findElement(by.css('div[ng-model="medicals"]')));
  }

  module.exports = SearchDocMedPO;
}(module, by, require));