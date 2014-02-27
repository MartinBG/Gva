/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Групи ВС
  module.exports = [
    {
      nomValueId: 8031, code: 'RT1', name: 'Radio Altimeter', nameAlt: null, parentValueId: null, alias: 'RT1'
    },
    {
      nomValueId: 8032, code: 'RT2', name: 'Satellite Communication', nameAlt: null, parentValueId: null, alias: 'RT2'
    }
  ];
})(typeof module === 'undefined' ? (this['aircraftRadioType'] = {}) : module);
