/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Групи ВС
  module.exports = [
    {
      nomValueId: 1, code: 'PP1', name: 'Artex, USA', nameAlt: null, parentValueId: 1, alias: 'PP1'
    },
    {
      nomValueId: 2, code: 'PP2', name: 'MT-Propeller, USA', nameAlt: null, parentValueId: 1, alias: 'PP2'
    },
    {
      nomValueId: 3, code: 'PP3', name: 'Artex, USA', nameAlt: null, parentValueId: 2, alias: 'PP3'
    },
    {
      nomValueId: 4, code: 'PP4', name: 'Rotax, Austria', nameAlt: null, parentValueId: 2, alias: 'PP4'
    },
    {
      nomValueId: 5, code: 'PP5', name: 'Collins, USA', nameAlt: null, parentValueId: 3, alias: 'PP5'
    },
    {
      nomValueId: 6, code: 'PP6', name: 'Bendix-King, USA', nameAlt: null, parentValueId: 4, alias: 'PP6'
    }
  ];
})(typeof module === 'undefined' ? (this['aircraftPartProducer'] = {}) : module);
