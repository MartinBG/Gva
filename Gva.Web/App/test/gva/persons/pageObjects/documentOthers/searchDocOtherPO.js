/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScDatatable = require('../../../../scaffolding/pageObjects/scDatatable'),
    Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function SearchDocOtherPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.datatable = new ScDatatable(
      context.findElement(by.css('div[ng-model="documentOthers"]')));
  }

  module.exports = SearchDocOtherPO;
}(module, by, require));