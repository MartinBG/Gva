/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature'),
    ScDate = require('../../../../scaffolding/pageObjects/scDate'),
    ScInt = require('../../../../scaffolding/pageObjects/scInt'),
    ScTextarea = require('../../../../scaffolding/pageObjects/ScTextarea');

  function PersonDocumentTrainingPO(context) {
    this.context = context;
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));

    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
    this.isSaveBtnDisabled = context.isElementPresent(by.css('button[disabled=disabled]'));
  }

  PersonDocumentTrainingPO.prototype.chooseStaffType = function (value) {
    var context = this.context,
        self = this;

    return this.context.findElement(by.css('div.select2-container')).then(function (container) {
      return container.click().then(function () {
        return context.findElement(by.css('div.select2-drop-active input.select2-input'))
          .then(function (input) {
          return input.sendKeys(value).then(function () {
            return input.sendKeys('\n').then(function () {
              self.documentNumber =
                new ScText(context.findElement(by.input('model.documentNumber')));
              self.documentPersonNumber =
                new ScInt(context.findElement(by.input('model.documentPersonNumber')));
              self.documentDateValidFrom =
                new ScDate(context.findElement(by.css('div[name=documentDateValidFrom]')), context);
              self.documentDateValidTo =
                new ScDate(context.findElement(by.css('div[name=documentDateValidTo]')), context);
              self.documentPublisher =
                new ScText(context.findElement(by.input('model.documentPublisher')));
              self.personOtherDocumentType =
                new ScNomenclature('model.documentType', context);
              self.personOtherDocumentRole =
                new ScNomenclature('model.documentRole', context);
              self.valid = new ScNomenclature('model.valid', context);
              self.notes = new ScTextarea(context.findElement(by.model('model.notes')));
            });
          });
        });
      });
    });
  };

  PersonDocumentTrainingPO.prototype.save = function () {
    this.saveBtn.click();
  };

  PersonDocumentTrainingPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = PersonDocumentTrainingPO;
}(module, by, require));