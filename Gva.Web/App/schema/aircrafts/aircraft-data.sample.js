/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Data: {
      name: 'Beech Super King',
      nameAlt: 'Beech Super King',
      model: '',
      series: 'Air-200',
      aircraftTypeGroup: nomenclatures.get('aircraftTypeGroups', 'PWCPT6'),
      aircraftCategory: nomenclatures.get('aircraftCategories', 'A2'),
      aircraftProducer: nomenclatures.get('aircraftProducers', 'P1'),
      aircraftSCodeType: nomenclatures.get('aircraftSCodeTypes', 'CMPA'),
      manPlace: 'няма данни',
      manDate: '1975-01-01T00:00',
      manSN: 'BB-82',
      beaconCodeELT: 'BB-82',
      maxMassT: 5670,
      maxMassL: 5670,
      icao: 'BE20',
      docRoom: '307',
      mass: null,
      ultralight: {
        color: null,
        colorAlt: null,
        seats: null,
        payload: null
      },
      noise: {
        flyover: null,
        approach: null,
        lateral: null,
        overflight: 79.20,
        takeoff: null
      },
      radio: {
        approvalNumber: 'P-076',
        approvalDate: '2008-12-12T00:00',
        incommingApprovalNumber: '40-01-887',
        incommingApprovalDate: '2008-12-05T00:00'
      }
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-data.sample'] = {}) : module);
