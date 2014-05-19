/*global module, by, require*/
(function (module, by, require) {
  'use strict';

  var ScSelect = require('../scSelect');

  function SelectPO(context) {
    this.selectDirective = new ScSelect('select', context);
    this.context = context;
  }

  SelectPO.prototype.selectOption = function (optionName) {
    return this.context.findElement(by.css('.radio-inline.' + optionName + ' input'))
            .then(function (option) {
      return option.click();
    });
  };

  SelectPO.prototype.isSelected = function (optionName) {
    return this.context.findElement(by.css('.radio-inline.' + optionName + ' input'))
        .then(function (option) {
      return option.getAttribute('checked');
    });
  };

  module.exports = SelectPO;
}(module, by, require));