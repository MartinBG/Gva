/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable'),
    Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function SearchAddressPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.datatable = new ScDatatable(context.findElement(by.css('div[items="addresses"]')));
    this.firstDeleteBtn = context
      .findElement(by.css('tbody tr:first-child button[name=deleteBtn]'));
    this.firstEditBtn = context
      .findElement(by.css('tbody tr:first-child button[name=editBtn]'));
  }

  module.exports = SearchAddressPO;
}(module, by, require));