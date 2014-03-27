/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable'),
      Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function SearchCheckPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.datatable = new ScDatatable(context.findElement(by.css('div[items="checks"]')));
    this.firstDeleteBtn = context
      .findElement(by.css('tbody tr:first-child button[name=deleteBtn]'));
    this.firstEditBtn = context
      .findElement(by.css('tbody tr:first-child button[name=editBtn]'));
  }

  module.exports = SearchCheckPO;
}(module, by, require));