/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Въздухоплавателни средства
  module.exports = [
    { nomTypeValueId: 1, code: '', name: 'LZ-001 A-11', nameAlt: '', alias: 'LZ-001 A-11' },
    { nomTypeValueId: 2, code: '', name: 'LZ-002 Вива --56 V-56', nameAlt: '', alias: 'LZ-002 Viva 56' },
    { nomTypeValueId: 3, code: '', name: 'LZ-003 Cameron -A-210', nameAlt: '', alias: 'LZ-003 Cameron 210' }
  ];
})(typeof module === 'undefined' ? (this['aircraft'] = {}) : module);
