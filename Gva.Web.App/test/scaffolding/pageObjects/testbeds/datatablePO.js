﻿/*global module, by, require*/
( function (module, by, require) {
  'use strict';

  var ScDatatable = require('../scDatatable');

  function DatatablePO( context ) {
    this.context = context;
    this.datatable1 = new ScDatatable(context.findElement(by.css('div[items="users"]')));
    this.datatable2 = new ScDatatable(context.findElement(by.css('div[items="users2"]')));
    this.datatable3 = new ScDatatable(context.findElement(by.css('div[items="users3"]')));
    this.datatable4 = new ScDatatable(context.findElement(by.css('div[items="documents"]')));
    this.loadManyInTable1Btn = context.findElement(by.id('loadManyInTable1Btn'));
    this.loadManyInTable2Btn = context.findElement(by.id('loadManyInTable2Btn'));
    this.loadManyInTable3Btn = context.findElement(by.id('loadManyInTable3Btn'));
    this.selectedUser = context.findElement(by.model('selectedUser'));
  }

  DatatablePO.prototype.loadManyEntriesInTable1 = function () {
    return this.loadManyInTable1Btn.click();
  };

  DatatablePO.prototype.loadManyEntriesInTable2 = function () {
    return this.loadManyInTable2Btn.click();
  };

  DatatablePO.prototype.loadManyEntriesInTable3 = function () {
    return this.loadManyInTable3Btn.click();
  };

  module.exports = DatatablePO;
}(module, by, require));