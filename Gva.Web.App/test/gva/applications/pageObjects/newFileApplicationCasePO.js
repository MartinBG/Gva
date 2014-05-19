/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScNomenclature = require('../../../scaffolding/pageObjects/scNomenclature');

  function NewFileApplicationCasePO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));
    this.docPartType = new ScNomenclature('docPartType', context);

    this.addPartBtn = context.findElement(by.name('addPartBtn'));
  }

  NewFileApplicationCasePO.prototype.addPart = function () {
    this.addPartBtn.click();
  };

  module.exports = NewFileApplicationCasePO;
}(module, by, require));
