/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Ново ВС
  module.exports = [
    {
      nomValueId: 8024, code: 'N', name: 'Нов', nameAlt: null, parentValueId: null, alias: 'new'
    },
    {
      nomValueId: 8025, code: 'R', name: 'Ново ремонтиран', nameAlt: null, parentValueId: null, alias: 'rep'
    },
    {
      nomValueId: 8025, code: 'U', name: 'Използван', nameAlt: null, parentValueId: null, alias: 'used'
    }
  ];
})(typeof module === 'undefined' ? (this['aircraftNewOld'] = {}) : module);
