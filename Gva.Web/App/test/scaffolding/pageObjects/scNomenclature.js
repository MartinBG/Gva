﻿/*global module, by*/
(function (module, by) {
  'use strict';

  function ScNomenclature(model, context) {
    /*jshint -W101*/
    /* jshint -W109*/
    var containerXPath = ".//div[contains(@class,'select2-container') and following-sibling::*[1][self::input and @ng-model='" + model + "']]",
      containerAnchorXPath = containerXPath + "/a[contains(@class, 'select2-choice')]",
      nameSpanXPath = containerAnchorXPath + "/span[contains(@class, 'select2-chosen')]";
    this.dropdownInputXPath = "//div[@id='select2-drop']/div[contains(@class, 'select2-search')]/input[contains(@class, 'select2-input')]";
    /* jshint +W101*/
    /* jshint +W109*/
    this.context = context;
    this.container = this.context.findElement(by.xpath(containerXPath));
    this.nameSpan = this.context.findElement(by.xpath(nameSpanXPath));
  }

  ScNomenclature.prototype.get = function () {
    return this.nameSpan.getText();
  };

  ScNomenclature.prototype.click = function () {
    return this.container.click();
  };

  ScNomenclature.prototype.dropdownInput = function () {
    return this.context.findElement(by.xpath(this.dropdownInputXPath));
  };

  ScNomenclature.prototype.set = function (text) {
    this.container.click();
    this.context.findElement(by.xpath(this.dropdownInputXPath)).sendKeys(text);
    this.context.findElement(by.xpath(this.dropdownInputXPath)).sendKeys('\n');
  };

  ScNomenclature.prototype.clear = function () {
    return this.container.findElement(by.css('.select2-search-choice-close'))
      .then(function (deselectBtn) {
      return deselectBtn.click();
    });
  };

  module.exports = ScNomenclature;
}(module, by));