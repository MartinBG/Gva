/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScTextarea = require('../../../../scaffolding/pageObjects/ScTextarea'),
      ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature'),
      ScDate = require('../../../../scaffolding/pageObjects/ScDate'),
      Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function EmploymentPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.valid = new ScNomenclature('model.valid', context);
    this.employmentCategory = new ScNomenclature('model.employmentCategory', context);
    this.organization = new ScNomenclature('model.organization', context);
    this.country = new ScNomenclature('model.country', context);
    this.hiredate = new ScDate(context.findElement(by.css('div[name=hiredate]')), context);
    this.notes = new ScTextarea(context.findElement(by.css('textarea')));
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  EmploymentPO.prototype.save = function () {
    this.saveBtn.click();
  };

  EmploymentPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = EmploymentPO;
}(module, by, require));