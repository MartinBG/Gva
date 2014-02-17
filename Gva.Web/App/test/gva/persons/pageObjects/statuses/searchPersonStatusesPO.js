/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable');

  function SearchPersonStatusesPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));

    this.datatable = new ScDatatable(context.findElement(by.css('div[ng-model="statuses"]')));
    this.firstDeleteBtn = context
      .findElement(by.css('tbody tr:first-child button[name=deleteBtn]'));
    this.firstEditBtn = context
      .findElement(by.css('tbody tr:first-child button[name=editBtn]'));
  }

  module.exports = SearchPersonStatusesPO;
}(module, by, require));