/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScNomenclature = require('../scNomenclature');

  function NomenclaturePO(context) {
    this.nomenclature = new ScNomenclature('gender', context);
    this.selectedNomIdSpan = context.findElement(by.id('selectedNomId'));
    this.selectedNomNameSpan = context.findElement(by.id('selectedNomName'));
    this.changeBtn = context.findElement(by.id('changeBtn'));
  }

  NomenclaturePO.prototype.selectedNomId = function () {
    return this.selectedNomIdSpan.getText();
  };

  NomenclaturePO.prototype.selectedNomName = function () {
    return this.selectedNomNameSpan.getText();
  };

  NomenclaturePO.prototype.changeNomValue = function () {
    return this.changeBtn.click();
  };

  module.exports = NomenclaturePO;
}(module, by, require));