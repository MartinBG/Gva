﻿/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('./directives/scDatatable');

  function searchPersonStatusesPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));

    this.datatable = new ScDatatable(context.findElement(by.css('div[ng-model="statuses"]')));
  }

  module.exports = searchPersonStatusesPO;
}(module, by, require));