/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../scaffolding/pageObjects/scText'),
    ScTextarea = require('../../../scaffolding/pageObjects/ScTextarea'),
    ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../scaffolding/pageObjects/scDate');

  function NewDocEduPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.valid = new ScNomenclature('model.valid', context);
    this.employmentCategory = new ScNomenclature('model.employmentCategory', context);
    this.organization = new ScNomenclature('model.organization', context);
    this.country = new ScNomenclature('model.country', context);
    this.hiredate = new ScDate(context.findElement(by.css('div[name=hiredate]')), context);
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