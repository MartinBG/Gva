/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Организации
  module.exports = [
    { nomValueId: 203, code: '203', name: 'AAK Progres', nameAlt: 'AAK Progres', alias: 'AAK Progres' },
    { nomValueId: 204, code: '204', name: 'Wizz Air', nameAlt: 'Wizz Air', alias: 'Wizz Air' },
    { nomValueId: 205, code: '205', name: 'Fly Emirates', nameAlt: 'Fly Emirates', alias: 'Fly Emirates' }
  ];
})(typeof module === 'undefined' ? (this['organization'] = {}) : module);
