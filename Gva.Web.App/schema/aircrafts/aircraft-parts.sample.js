/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Part1: {
      aircraftPart: nomenclatures.get('aircraftParts', 'P1'),
      partProducer: nomenclatures.get('aircraftPartProducers', 'PP1'),
      model: 'Hartzell Propeller: HC-B3TN-3G/T10178B-3R',
      modelAlt: 'Hartzell Propeller: HC-B3TN-3G/T10178B-3R',
      sn: '123332',
      count: '1',
      aircraftPartStatus: nomenclatures.get('aircraftPartStatuses', 'P1'),
      manDate: '2013-07-09T00:00',
      manPlace: '',
      description: ''
    },
    aircraft1Part2: {
      aircraftPart: nomenclatures.get('aircraftParts', 'P2'),
      partProducer: nomenclatures.get('aircraftPartProducers', 'PP3'),
      model: 'Pratt & Whitney Canada: PT6A-41',
      modelAlt: 'Pratt & Whitney Canada: PT6A-41',
      sn: '31313131',
      count: '1',
      aircraftPartStatus: nomenclatures.get('aircraftPartStatuses', 'P3'),
      manDate: '2012-07-09T00:00',
      manPlace: '',
      description: ''
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-parts.sample'] = {}) : module);
