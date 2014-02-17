/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var PersonDataPO = require('./personDataPO'),
    PersonAddressPO = require('./addresses/addressPO'),
    PersonDocumentIdPO = require('./personDocumentIdPO');

  function NewPersonPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.personAddress = new PersonAddressPO(context);
    this.personDocumentId = new PersonDocumentIdPO(context);
    this.personData = new PersonDataPO(context);
    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
    this.isSaveBtnDisabled = context.isElementPresent(by.css('button[disabled=disabled]'));
  }

  NewPersonPO.prototype.save = function () {
    this.saveBtn.click();
  };

  NewPersonPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = NewPersonPO;
}(module, by, require));
