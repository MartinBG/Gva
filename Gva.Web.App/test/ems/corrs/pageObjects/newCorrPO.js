/*global module, by, require*/
(function (module, by, require) {
  'use strict';
  var ScText = require('../../../scaffolding/pageObjects/scText'),
    ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature');

  function NewCorrPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.correspondentGroup = new ScNomenclature('corr.correspondentGroup', context);
    this.correspondentType = new ScNomenclature('corr.correspondentType', context);
    this.email = new ScText(context.findElement(by.input('corr.email')));
    this.bgCitizenFirstName = new ScText(context.findElement(by.input('corr.bgCitizenFirstName')));
    this.bgCitizenLastName = new ScText(context.findElement(by.input('corr.bgCitizenLastName')));

    this.saveBtn = context.findElement(by.name('saveBtn'));
    this.cancelBtn = context.findElement(by.name('cancelBtn'));
  }

  NewCorrPO.prototype.save = function () {
    this.saveBtn.click();
  };

  NewCorrPO.prototype.cancel = function () {
    this.cancelBtn.click();
  };

  module.exports = NewCorrPO;
}(module, by, require));
