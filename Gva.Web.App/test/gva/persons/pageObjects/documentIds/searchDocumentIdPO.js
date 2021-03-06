﻿/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable'),
    Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function SearchDocumentIdPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.datatable = new ScDatatable(context.findElement(by.css('div[items="documentIds"]')));
    this.firstDeleteBtn = context
      .findElement(by.css('tbody tr:first-child button[name=deleteBtn]'));
    this.firstEditBtn = context
      .findElement(by.css('tbody tr:first-child button[name=editBtn]'));
  }

  module.exports = SearchDocumentIdPO;
}(module, by, require));