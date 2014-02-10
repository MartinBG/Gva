/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Типове състояния на Физичеко лице
  module.exports = [
      { nomTypeValueId: 6589, code: 'FD', name: 'Освободен', nameAlt: 'Освободен', alias: '' },
      { nomTypeValueId: 6590, code: 'МA', name: 'Майчинство', nameAlt: 'Майчинство', alias: 'maternity leave' },
      { nomTypeValueId: 6591, code: 'PS', name: 'Пенсионер', nameAlt: 'Пенсионер', alias: '' },
      { nomTypeValueId: 6592, code: 'TU', name: 'Временно негоден', nameAlt: 'Временно негоден', alias: 'temporary unfit' },
      { nomTypeValueId: 6593, code: 'UF', name: 'Негоден', nameAlt: 'Негоден', alias: 'permanently unfit' }
  ];
})(typeof module === 'undefined' ? (this['personStatusType'] = {}) : module);
