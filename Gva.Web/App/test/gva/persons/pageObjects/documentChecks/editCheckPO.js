/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../../scaffolding/pageObjects/scText'),
      ScInt = require('../../../../scaffolding/pageObjects/scInt'),
      ScDate = require('../../../../scaffolding/pageObjects/scDate'),
      ScTextarea = require('../../../../scaffolding/pageObjects/scTextarea'),
      ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature'),
      Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function EditCheckPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.staffType = new ScNomenclature('model.staffType', context);
    this.aircraftTypeGroup = new ScNomenclature('model.aircraftTypeGroup', context);
    this.ratingType = new ScNomenclature('model.ratingType', context);
    this.locationIndicator = new ScNomenclature('model.locationIndicator', context);
    this.sector = new ScText(context.findElement(by.input('model.sector')));
    this.documentNumber = new ScText(context.findElement(by.input('model.documentNumber')));
    this.documentPersonNumber = new ScInt(
      context.findElement(by.input('model.documentPersonNumber'))
    );
    this.personCheckDocumentType = new ScNomenclature('model.documentType', context);
    this.personCheckDocumentRole = new ScNomenclature('model.documentRole', context);
    this.documentDateValidFrom = new ScDate(
      context.findElement(by.name('documentDateValidFrom')),
      context);
    this.documentDateValidTo = new ScDate(
      context.findElement(by.name('documentDateValidTo')),
      context);
    this.documentPublisher = new ScText(context.findElement(by.input('model.documentPublisher')));
    this.ratingClass = new ScNomenclature('model.ratingClass', context);
    this.authorization = new ScNomenclature('model.authorization', context);
    this.licenceType = new ScNomenclature('model.licenceType', context);
    this.valid = new ScNomenclature('model.valid', context);
    this.notes = new ScTextarea(context.findElement(by.name('notes')));

    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  EditCheckPO.prototype.save = function () {
    this.saveBtn.click();
  };

  EditCheckPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = EditCheckPO;
}(module, by, require));