/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    organization1Data: {
      name: 'AAK Progres',
      nameAlt: 'AAK Progres',
      code: '5',
      uin: '342',
      cao: '56a',
      dateCAOFirstIssue: '2010-03-06T00:00',
      dateCAOLastIssue: '2012-05-07T00:00',
      dateCaoValidTo: '2014-05-07T00:00',
      ICAO: '56as',
      IATA: 's32',
      SITA: '4a',
      organizationType: nomenclatures.get('organizationTypes', 'LAP'),
      organizationKind: nomenclatures.get('organizationKinds', '11'),
      phones: '3442223',
      webSite: 'www.example.com',
      notes: 'тестови...',
      valid: nomenclatures.get('boolean', 'true'),
      dateValidTo: '2014-06-07T00:00',
      docRoom: '401a'
    }
  };
})(typeof module === 'undefined' ? (this['organization-data.sample'] = {}) : module);