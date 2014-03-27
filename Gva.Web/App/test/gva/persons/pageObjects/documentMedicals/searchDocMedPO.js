/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable'),
    Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function SearchDocMedPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.datatable = new ScDatatable(
      context.findElement(by.css('div[items="medicals"]')));
  }

  module.exports = SearchDocMedPO;
}(module, by, require));