/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var ScDatatable = require('./directives/scDatatable'),
      ScSearch = require('./directives/scSearch');

  function UsersPO(context) {
    this.datatable = new ScDatatable(context.findElement(by.css('div[ng-model="users"]')));
    this.searchForm = new ScSearch(context.findElement(by.tagName('form')), context);
  }

  module.exports = UsersPO;
}(module, by, require));