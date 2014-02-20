/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScText = require('../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature');

  function NewApplicationPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.person = new ScNomenclature('application.person.id', context);
    this.docTypeGroup = new ScNomenclature('application.docTypeGroup', context);
    this.docType = new ScNomenclature('application.docType', context);
    this.docSubject = new ScText(context.findElement(by.input('application.docSubject')));

    this.registerBtn = context.findElement(by.name('registerBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  NewApplicationPO.prototype.register = function () {
    this.registerBtn.click();
  };

  NewApplicationPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = NewApplicationPO;
}(module, by, require));
