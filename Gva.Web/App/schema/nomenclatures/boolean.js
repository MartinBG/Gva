﻿/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Булеви стойности
  module.exports = [
      { nomTypeValueId: 1, code: 'Y', name: 'Да', nameAlt: 'Yes', alias: 'true' },
      { nomTypeValueId: 2, code: 'N', name: 'Не', nameAlt: 'No', alias: 'false' }
  ];
})(typeof module === 'undefined' ? (this['boolean'] = {}) : module);
