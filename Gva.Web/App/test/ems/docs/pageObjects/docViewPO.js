/*global module, by*/
(function (module, by) {
  'use strict';

  function DocViewPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));

    this.editBtn= context.findElement(by.name('editBtn'));
    this.selectUnitFromBtn = context.findElement(by.name('selectUnitFromBtn'));
    this.selectCorrBtn = context.findElement(by.name('selectCorrBtn'));
  }

  DocViewPO.prototype.edit = function () {
    this.editBtn.click();
  };

  DocViewPO.prototype.selectUnitFrom = function () {
    this.selectUnitFromBtn.click();
  };

  DocViewPO.prototype.selectCorr = function () {
    this.selectCorrBtn.click();
  };

  module.exports = DocViewPO;
}(module, by));
