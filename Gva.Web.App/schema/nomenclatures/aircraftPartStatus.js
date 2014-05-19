/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Групи ВС
  module.exports = [
  {
    nomValueId: 1, code: 'P1', name: 'Нов', nameAlt: null, alias: 'P1'
  },
  {
    nomValueId: 2, code: 'P2', name: 'Ново ремонтиран', nameAlt: null, alias: 'P2'
  },
  {
    nomValueId: 3, code: 'P3', name: 'Използван', nameAlt: null, alias: 'P3'
  }
  ];
})(typeof module === 'undefined' ? (this['aircraftPartStatus'] = {}) : module);
