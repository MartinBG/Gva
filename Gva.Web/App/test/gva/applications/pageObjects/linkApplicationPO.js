/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature');

  function LinkApplicationPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.person = new ScNomenclature('application.person.id', context);

    this.selectDocBtn = context.findElement(by.name('selectDocBtn'));
    this.linkBtn = context.findElement(by.name('linkBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  LinkApplicationPO.prototype.selectDoc = function () {
    this.selectDocBtn.click();
  };

  LinkApplicationPO.prototype.link = function () {
    this.linkBtn.click();
  };

  LinkApplicationPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = LinkApplicationPO;
}(module, by, require));
