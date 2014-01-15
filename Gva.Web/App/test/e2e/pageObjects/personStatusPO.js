/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var ScText = require('./directives/scText'),
      ScDate = require('./directives/scDate'),
      ScNomenclature = require('./directives/scNomenclature');

  function PersonStatusPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));

    this.personStatusType = new ScNomenclature('model.personStatusType', context);
    this.documentNumber = new ScText(context.findElement(by.input('model.documentNumber')));
    this.documentDateValidFrom =
      new ScDate(context.findElement(by.css('div[name=docDateValidFrom]')), context);
    this.documentDateValidTo =
      new ScDate(context.findElement(by.css('div[name=docDateValidTo]')), context);
    this.notes = new ScText(context.findElement(by.input('model.notes')));

    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  PersonStatusPO.prototype.save = function () {
    this.saveBtn.click();
  };

  PersonStatusPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = PersonStatusPO;
}(module, by, require));