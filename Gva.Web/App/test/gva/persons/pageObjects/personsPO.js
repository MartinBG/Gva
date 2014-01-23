﻿/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var ScDatatable = require('../../../scaffolding/pageObjects/scDatatable'),
      ScSearch = require('../../../scaffolding/pageObjects/scSearch');

  function PersonsPO(context) {
    this.datatable = new ScDatatable(context.findElement(by.css('div[ng-model="persons"]')));
    this.searchForm = new ScSearch(context.findElement(by.tagName('form')), context);
  }

  module.exports = PersonsPO;
}(module, by, require));