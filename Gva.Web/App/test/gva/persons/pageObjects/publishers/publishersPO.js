/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable'),
      ScSearch = require('../../../../scaffolding/pageObjects/scSearch');

  function PublishersPO(context) {
    this.datatable = new ScDatatable(context.findElement(by.css('div[items="publishers"]')));
    this.searchForm = new ScSearch(context.findElement(by.name('publishersSearch')), context);
    this.firstSelectBtn = context
      .findElement(by.css('tbody tr:first-child button[name=selectBtn]'));
  }

  module.exports = PublishersPO;
}(module, by, require));