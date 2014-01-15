/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('./directives/scDatatable');

  function SearchPersonStatusesPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));

    this.datatable = new ScDatatable(context.findElement(by.css('div[ng-model="statuses"]')));
  }

  module.exports = SearchPersonStatusesPO;
}(module, by, require));