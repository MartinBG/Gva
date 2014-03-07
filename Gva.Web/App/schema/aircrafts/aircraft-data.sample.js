/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Data: {
      model: 'Beech Super King',
      modelAlt: 'Beech Super King',
      manSN: 'Air-200',
      engine: 'Trent 1000-TEN',
      engineAlt: 'Trent 1000-TEN',
      propeller: 'Rolls-Royce Griffon',
      propellerAlt: 'Rolls-Royce Griffon',
      ModifOrWingColor: 'Rolls-Royce Griffon',
      ModifOrWingColorAlt: 'Rolls-Royce Griffon',
      aircraftCategory: nomenclatures.get('aircraftCategories', 'A2'),
      aircraftProducer: nomenclatures.get('aircraftProducers', 'P1'),
      outputDate: '1975-01-01T00:00',
      maxMassT: 5670,
      maxMassL: 5670,
      seats: 123,
      icao: 'BE20',
      docRoom: '307',
      cofAType: nomenclatures.get('cofATypes', 'E25'),
      easaType: nomenclatures.get('easaTypes', 'BA'),
      euRegType: nomenclatures.get('euRegTypes', 'EU'),
      easaCategory: nomenclatures.get('easaCategories', 'AW'),
      tcds: ''
    },
    aircraft2Data: {
      model: 'A109E',
      modelAlt: 'A109E',
      manSN: 'Air-200',
      engine: 'Trent 1000-TEN',
      engineAlt: 'Trent 1000-TEN',
      propeller: 'Rolls-Royce Griffon',
      propellerAlt: 'Rolls-Royce Griffon',
      ModifOrWingColor: 'Rolls-Royce Griffon',
      ModifOrWingColorAlt: 'Rolls-Royce Griffon',
      aircraftCategory: nomenclatures.get('aircraftCategories', 'A2'),
      aircraftProducer: nomenclatures.get('aircraftProducers', 'P1'),
      outputDate: '1985-03-21T00:00',
      maxMassT: 5670,
      maxMassL: 5670,
      seats: 222,
      icao: 'BE20',
      docRoom: '307',
      cofAType: nomenclatures.get('cofATypes', 'E24'),
      easaType: nomenclatures.get('easaTypes', 'CO'),
      euRegType: nomenclatures.get('euRegTypes', 'EUR'),
      easaCategory: nomenclatures.get('easaCategories', 'COM'),
      tcds: ''
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-data.sample'] = {}) : module);
