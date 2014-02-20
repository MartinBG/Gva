/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../../scaffolding/pageObjects/scNomenclature');

  function AddressPO(context) {
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

  AddressPO.prototype.save = function () {
    this.saveBtn.click();
  };

  AddressPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = AddressPO;
}(module, by, require));