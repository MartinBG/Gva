/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Групи ВС
  module.exports = [
    {
      nomValueId: 8005, code: 'SC1', name: 'Complex Motor - Powered Aircraft', nameAlt: null, parentValueId: null, alias: 'CMPA'
    },
    {
      nomValueId: 8006, code: 'SC2', name: 'Non-Complex Motor - Powered Aircraft', nameAlt: null, parentValueId: null, alias: 'NCMPA'
    },
  ];
})(typeof module === 'undefined' ? (this['aircraftSCodeType'] = {}) : module);
