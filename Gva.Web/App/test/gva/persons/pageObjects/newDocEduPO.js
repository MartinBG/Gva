/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../scaffolding/pageObjects/scText'),
    ScTextarea = require('../../../scaffolding/pageObjects/ScTextarea'),
    ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../scaffolding/pageObjects/scDate');

  function NewDocEduPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.speciality = new ScText(context.findElement(by.input('model.speciality')));
    this.graduation = new ScNomenclature('model.graduation', context);
    this.school = new ScNomenclature('model.school', context);
    this.documentNumber = new ScText(context.findElement(by.input('model.documentNumber')));
    this.completionDate = new ScDate(
      context.findElement(by.css('div[name=completionDate]')), context);
    this.notes = new ScTextarea(context.findElement(by.css('textarea')));
    this.bookPageNumber = new ScText(context.findElement(by.input('model.bookPageNumber')));
    this.pageCount = new ScText(context.findElement(by.input('model.pageCount')));
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  NewDocEduPO.prototype.save = function () {
    this.saveBtn.click();
  };

  NewDocEduPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = NewDocEduPO;
}(module, by, require));