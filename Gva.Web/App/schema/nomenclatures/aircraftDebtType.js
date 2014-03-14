/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Типове тежести върху ВС
  module.exports = [
    {
      nomValueId: 8403, code: 'DT1', name: 'Ипотека', nameAlt: null, parentValueId: null, alias: 'DT1'
    },
    {
      nomValueId: 8404, code: 'DT2', name: 'Запор', nameAlt: null, parentValueId: null, alias: 'DT2'
    },
  ];
})(typeof module === 'undefined' ? (this['aircraftDebtType'] = {}) : module);
