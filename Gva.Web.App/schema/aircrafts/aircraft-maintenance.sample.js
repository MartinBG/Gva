/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraftMaintenance1: {
      lim145limitation: nomenclatures.get('lim145limitations', 'A1'),
      notes: 'Тестово описание',
      fromDate: '1970-05-02T00:00',
      toDate: '1971-05-02T00:00',
      organization: nomenclatures.get('organizations', 'Wizz Air'),
      person: nomenclatures.get('persons', 'P1')
    },
    aircraftMaintenance2: {
      lim145limitation: nomenclatures.get('lim145limitations', 'A2'),
      notes: 'Тестово описание 2',
      fromDate: '2000-05-02T00:00',
      toDate: '2001-05-02T00:00',
      organization: nomenclatures.get('organizations', 'Fly Emirates'),
      person: nomenclatures.get('persons', 'P2')
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-maintenance.sample'] = {}) : module);
