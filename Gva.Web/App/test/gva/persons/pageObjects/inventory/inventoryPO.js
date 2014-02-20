/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable');

  function InventoryPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.datatable = new ScDatatable(
      context.findElement(by.css('div[ng-model="inventory"]')));
  }

  module.exports = InventoryPO;
}(module, by, require));