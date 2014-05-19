/*global module, by, require*/
(function (module, by, require){
  'use strict';

  var ScText = require('../../../../scaffolding/pageObjects/scText'),
      ScTextarea = require('../../../../scaffolding/pageObjects/scTextarea'),
      ScDate = require('../../../../scaffolding/pageObjects/scDate'),
      ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature'),
      Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function PersonStatusPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.personStatusType = new ScNomenclature('model.personStatusType', context);
    this.documentNumber = new ScText(context.findElement(by.input('model.documentNumber')));
    this.documentDateValidFrom =
      new ScDate(context.findElement(by.css('div[name=documentDateValidFrom]')), context);
    this.documentDateValidTo =
      new ScDate(context.findElement(by.css('div[name=documentDateValidTo]')), context);
    this.notes = new ScTextarea(context.findElement(by.model('model.notes')));

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