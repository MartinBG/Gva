/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable'),
    Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function InventoryPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.datatable = new ScDatatable(
      context.findElement(by.css('div[ng-model="inventory"]')));
  }

  module.exports = InventoryPO;
}(module, by, require));