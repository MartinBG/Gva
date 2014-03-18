/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Групи ВС
  module.exports = [
  {
    nomValueId: 1, code: 'P1', name: 'Витла', nameAlt: null, alias: 'P1'
  },
  {
    nomValueId: 2, code: 'P2', name: 'Двигатели', nameAlt: null, alias: 'P2'
  },
  {
    nomValueId: 3, code: 'P3', name: 'Радиостанция', nameAlt: null, alias: 'P3'
  },
  {
    nomValueId: 4, code: 'P4', name: 'Emergency Location Transmitter', nameAlt: null, alias: 'P4'
  }
  ];
})(typeof module === 'undefined' ? (this['aircraftPart'] = {}) : module);
