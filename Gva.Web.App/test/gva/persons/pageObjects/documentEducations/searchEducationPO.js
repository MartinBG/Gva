/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable'),
    Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function SearchDocEduPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.datatable = new ScDatatable(
      context.findElement(by.css('div[items="documentEducations"]')));
    this.tableBody = context.findElement(by.css('td'));
  }

  module.exports = SearchDocEduPO;
}(module, by, require));