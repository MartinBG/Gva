/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../../scaffolding/pageObjects/scText'),
      ScInt = require('../../../../scaffolding/pageObjects/scInt'),
      ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature');

  function NewFlyingExpPO(context) {
    this.context = context;
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));

    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  NewFlyingExpPO.prototype.chooseStaffType = function (value) {
    var context = this.context,
        self = this;

    return this.context.findElement(by.css('div.select2-container')).then(function (container) {
      return container.click().then(function () {
        return context.findElement(by.css('div.select2-search input')).then(function (input) {
          return input.sendKeys(value).then(function () {
            return input.sendKeys('\n').then(function () {
              self.month = new ScText(context.findElement(by.input('model.period.month')));
              self.year = new ScInt(context.findElement(by.input('model.period.year')));
              self.organization = new ScNomenclature('model.organization', context);
              self.aircraft = new ScNomenclature('model.aircraft', context);
              self.ratingType = new ScNomenclature('model.ratingType', context);
              self.ratingClass = new ScNomenclature('model.ratingClass', context);
              self.licenceType = new ScNomenclature('model.licenceType', context);
              self.authorization = new ScNomenclature('model.authorization', context);
              self.experienceRole = new ScNomenclature('model.experienceRole', context);
              self.experienceMeasure = new ScNomenclature('model.experienceMeasure', context);
              self.dayIfrHours = new ScInt(context.findElement(by.input('model.dayIFR.hours')));
              self.dayIfrMinutes = new ScInt(context.findElement(by.input('model.dayIFR.minutes')));
              self.nightIfrHours = new ScInt(context.findElement(by.input('model.nightIFR.hours')));
              self.nightIfrMinutes = new ScInt(
                context.findElement(by.input('model.nightIFR.minutes'))
              );
              self.dayVfrHours = new ScInt(context.findElement(by.input('model.dayVFR.hours')));
              self.dayVfrMinutes = new ScInt(context.findElement(by.input('model.dayVFR.minutes')));
              self.nightVfrHours = new ScInt(context.findElement(by.input('model.nightVFR.hours')));
              self.nightVfrMinutes = new ScInt(
                context.findElement(by.input('model.nightVFR.minutes'))
              );
              self.dayLandings = new ScInt(context.findElement(by.input('model.dayLandings')));
              self.nightLandings = new ScInt(context.findElement(by.input('model.nightLandings')));
              self.totalDocHours = new ScInt(context.findElement(by.input('model.totalDoc.hours')));
              self.totalDocMinutes = new ScInt(
                context.findElement(by.input('model.totalDoc.minutes'))
              );
              self.totalLastMonthsHours = new ScInt(
                context.findElement(by.input('model.totalLastMonths.hours'))
              );
              self.totalLastMonthsMinutes = new ScInt(
                context.findElement(by.input('model.totalLastMonths.minutes'))
              );
              self.totalHours = new ScInt(context.findElement(by.input('model.total.hours')));
              self.totalMinutes = new ScInt(context.findElement(by.input('model.total.minutes')));
            });
          });
        });
      });
    });
  };

  NewFlyingExpPO.prototype.save = function () {
    this.saveBtn.click();
  };

  NewFlyingExpPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = NewFlyingExpPO;
}(module, by, require));