/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Полове
  module.exports = [
    { nomTypeValueId: 1, code: 'M', name: 'Мъж', nameAlt: 'Male', alias: 'male' },
    { nomTypeValueId: 2, code: 'W', name: 'Жена', nameAlt: 'Female', alias: 'female' },
    { nomTypeValueId: 3, code: 'U', name: 'Неизвестен', nameAlt: 'Unknown', alias: 'unknown' },
  ];
})(typeof module === 'undefined' ? (this['gender'] = {}) : module);
