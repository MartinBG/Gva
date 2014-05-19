/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Полове
  module.exports = [
    { nomValueId: 1, code: 'M', name: 'Мъж', nameAlt: 'Male', alias: 'male' },
    { nomValueId: 2, code: 'W', name: 'Жена', nameAlt: 'Female', alias: 'female' },
    { nomValueId: 3, code: 'U', name: 'Неизвестен', nameAlt: 'Unknown', alias: 'unknown' },
  ];
})(typeof module === 'undefined' ? (this['gender'] = {}) : module);
