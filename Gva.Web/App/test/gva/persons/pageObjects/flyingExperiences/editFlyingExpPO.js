/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../../scaffolding/pageObjects/scText'),
      ScInt = require('../../../../scaffolding/pageObjects/scInt'),
      ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature'),
      Breadcrumb = require('../../../../scaffolding/pageObjects/breadcrumb');

  function EditFlyingExpPO(context) {
    this.breadcrumb = new Breadcrumb(context);
    this.staffType = new ScNomenclature('model.staffType', context);
    this.month = new ScText(context.findElement(by.input('model.period.month')));
    this.year = new ScInt(context.findElement(by.input('model.period.year')));
    this.organization = new ScNomenclature('model.organization', context);
    this.aircraft = new ScNomenclature('model.aircraft', context);
    this.ratingType = new ScNomenclature('model.ratingType', context);
    this.ratingClass = new ScNomenclature('model.ratingClass', context);
    this.licenceType = new ScNomenclature('model.licenceType', context);
    this.authorization = new ScNomenclature('model.authorization', context);
    this.experienceRole = new ScNomenclature('model.experienceRole', context);
    this.experienceMeasure = new ScNomenclature('model.experienceMeasure', context);
    this.dayIfrHours = new ScInt(context.findElement(by.input('model.dayIFR.hours')));
    this.dayIfrMinutes = new ScInt(context.findElement(by.input('model.dayIFR.minutes')));
    this.nightIfrHours = new ScInt(context.findElement(by.input('model.nightIFR.hours')));
    this.nightIfrMinutes = new ScInt(context.findElement(by.input('model.nightIFR.minutes')));
    this.dayVfrHours = new ScInt(context.findElement(by.input('model.dayVFR.hours')));
    this.dayVfrMinutes = new ScInt(context.findElement(by.input('model.dayVFR.minutes')));
    this.nightVfrHours = new ScInt(context.findElement(by.input('model.nightVFR.hours')));
    this.nightVfrMinutes = new ScInt(context.findElement(by.input('model.nightVFR.minutes')));
    this.dayLandings = new ScInt(context.findElement(by.input('model.dayLandings')));
    this.nightLandings = new ScInt(context.findElement(by.input('model.nightLandings')));
    this.totalDocHours = new ScInt(context.findElement(by.input('model.totalDoc.hours')));
    this.totalDocMinutes = new ScInt(context.findElement(by.input('model.totalDoc.minutes')));
    this.totalLastMonthsHours = new ScInt(
      context.findElement(by.input('model.totalLastMonths.hours'))
    );
    this.totalLastMonthsMinutes = new ScInt(
      context.findElement(by.input('model.totalLastMonths.minutes'))
    );
    this.totalHours = new ScInt(context.findElement(by.input('model.total.hours')));
    this.totalMinutes = new ScInt(context.findElement(by.input('model.total.minutes')));

    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  EditFlyingExpPO.prototype.save = function () {
    this.saveBtn.click();
  };

  EditFlyingExpPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = EditFlyingExpPO;
}(module, by, require));