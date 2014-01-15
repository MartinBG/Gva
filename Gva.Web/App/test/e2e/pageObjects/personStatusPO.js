/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var ScText = require('./directives/scText');//,
      ScNomenclature = require('./directives/scNomenclature');

  function personStatusPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));

    this.personStatusType = new ScNomenclature('model.personStatusType',context);
    this.documentNumber = new ScText(context.findElement(by.input('model.documentNumber')));
    this.notes = new ScText(context.findElement(by.input('model.notes')));

    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  personStatusPO.prototype.save = function () {
    this.saveBtn.click();
  };

  personStatusPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = personStatusPO;
}(module, by, require));