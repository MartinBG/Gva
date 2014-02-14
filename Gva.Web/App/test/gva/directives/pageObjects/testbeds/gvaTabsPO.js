/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var GvaTabs = require('../gvaTabs');

  function GvaTabsPO(context) {
    this.gvaTabs = new GvaTabs(context.findElement(by.css('div[tab-list]')));
    this.currentStateSpan = context.findElement(by.id('currentState'));
    this.select2 = context.findElement(by.css('.select2-container'));
    this.select2Dropdown = context.findElement(by.css('.select2-drop'));
  }

  GvaTabsPO.prototype.changeState = function (stateName) {
    var dropdown = this.select2Dropdown;

    return this.select2.findElement(by.css('.select2-arrow'))
      .then(function (arrow) {
        return arrow.click().then(function () {
          return dropdown.findElement(by.tagName('input')).then(function (input) {
            return input.sendKeys(stateName + '\n');
          });
        });
      });
  };

  module.exports = GvaTabsPO;
}(module, by, require));