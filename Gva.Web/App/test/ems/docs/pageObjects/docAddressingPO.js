/*global module, by*/
(function (module, by) {
  'use strict';

  function DocAddressingPO(context) {
    this.breadcrumb = context.findElement(by.xpath('//ul[@class="breadcrumb"]/li[last()]'));

    this.editBtn= context.findElement(by.name('editBtn'));
    this.selectUnitFromBtn = context.findElement(by.name('selectUnitFromBtn'));
    this.selectCorrBtn = context.findElement(by.name('selectCorrBtn'));
  }

  DocAddressingPO.prototype.edit = function () {
    this.editBtn.click();
  };

  DocAddressingPO.prototype.selectUnitFrom = function () {
    this.selectUnitFromBtn.click();
  };

  DocAddressingPO.prototype.selectCorr = function () {
    this.selectCorrBtn.click();
  };

  module.exports = DocAddressingPO;
}(module, by));
