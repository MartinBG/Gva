/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var ScDatatable = require('../../../scaffolding/pageObjects/scDatatable'),
      ScSearch = require('../../../scaffolding/pageObjects/scSearch'),
      Breadcrumb = require('../../../scaffolding/pageObjects/breadcrumb');

  function PersonsPO(context) {
    this.datatable = new ScDatatable(context.findElement(by.css('div[ng-model="persons"]')));
    this.searchForm = new ScSearch(context.findElement(by.tagName('form')), context);
    this.breadcrumb = new Breadcrumb(context);
  }

  module.exports = PersonsPO;
}(module, by, require));