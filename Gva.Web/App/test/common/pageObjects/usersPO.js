/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var ScDatatable = require('../../scaffolding/pageObjects/scDatatable'),
      ScSearch = require('../../scaffolding/pageObjects/scSearch'),
      Breadcrumb = require('../../scaffolding/pageObjects/breadcrumb');

  function UsersPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.datatable = new ScDatatable(context.findElement(by.css('div[ng-model="users"]')));
    this.searchForm = new ScSearch(context.findElement(by.tagName('form')), context);
  }

  module.exports = UsersPO;
}(module, by, require));