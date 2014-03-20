/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../../scaffolding/pageObjects/scText'),
      ScInt = require('../../../../scaffolding/pageObjects/scInt'),
      ScDate = require('../../../../scaffolding/pageObjects/scDate'),
      ScTextarea = require('../../../../scaffolding/pageObjects/scTextarea'),
      ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature'),
      Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function NewCheckPO(context) {
    this.context = context;
    this.breadcrumb = new Breadcrumb(context);

    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  NewCheckPO.prototype.chooseStaffType = function (value) {
    var context = this.context,
        self = this;

    return this.context.findElement(by.css('div.select2-container')).then(function (container) {
      return container.click().then(function () {
        return context.findElement(by.css('div.select2-drop-active input.select2-input'))
            .then(function (input) {
          return input.sendKeys(value).then(function () {
            return input.sendKeys('\n').then(function () {
              self.aircraftTypeGroup = new ScNomenclature('model.aircraftTypeGroup', context);
              self.ratingType = new ScNomenclature('model.ratingType', context);
              self.locationIndicator = new ScNomenclature('model.locationIndicator', context);
              self.sector = new ScText(context.findElement(by.input('model.sector')));
              self.documentNumber =
                new ScText(context.findElement(by.input('model.documentNumber')));
              self.documentPersonNumber = new ScInt(
                context.findElement(by.input('model.documentPersonNumber'))
              );
              self.personCheckDocumentType =
                new ScNomenclature('model.documentType', context);
              self.personCheckDocumentRole =
                new ScNomenclature('model.documentRole', context);
              self.documentDateValidFrom = new ScDate(
                context.findElement(by.name('documentDateValidFrom')),
                context);
              self.documentDateValidTo = new ScDate(
                context.findElement(by.name('documentDateValidTo')),
                context);
              self.documentPublisher =
                new ScText(context.findElement(by.input('model.documentPublisher')));
              self.ratingClass = new ScNomenclature('model.ratingClass', context);
              self.authorization = new ScNomenclature('model.authorization', context);
              self.licenceType = new ScNomenclature('model.licenceType', context);
              self.valid = new ScNomenclature('model.valid', context);
              self.notes = new ScTextarea(context.findElement(by.name('notes')));
            });
          });
        });
      });
    });
  };

  NewCheckPO.prototype.save = function () {
    this.saveBtn.click();
  };

  NewCheckPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = NewCheckPO;
}(module, by, require));