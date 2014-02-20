/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../scaffolding/pageObjects/scDatatable');

  function SelectUnitPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));

    this.datatable = new ScDatatable(context.findElement(by.css('div[ng-model="units"]')));
    this.tableBody = context.findElement(by.css('td'));
    this.firstSelectBtn = context
      .findElement(by.css('tbody tr:first-child button[name=selectBtn]'));
  }


  module.exports = SelectUnitPO;
}(module, by, require));
