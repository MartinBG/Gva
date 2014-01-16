/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('./directives/scText'),
    ScNomenclature = require('./directives/scNomenclature');

  function PersonAddressPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.address = new ScText(context.findElement(by.input('model.address')));
    this.addressAlt = new ScText(context.findElement(by.input('model.addressAlt')));
    this.addressType = new ScNomenclature('model.addressType', context);
    this.valid = new ScNomenclature(
      'model.valid',
      context.findElement(by.name('personAddressForm'))
      );
    this.settlement = new ScNomenclature('model.settlement', context);
    this.postalCode = new ScText(context.findElement(by.input('model.postalCode')));
    this.phone = new ScText(context.findElement(by.input('model.phone')));
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
    this.isSaveBtnDisabled = context.isElementPresent(by.css('button[disabled=disabled]'));
  }

  PersonAddressPO.prototype.save = function () {
    this.saveBtn.click();
  };

  PersonAddressPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = PersonAddressPO;
}(module, by, require));