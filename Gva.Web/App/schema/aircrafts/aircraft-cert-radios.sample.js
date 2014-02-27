/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Radio1: {
      certId: '5',
      certNumber: 2231,
      issueDate: '2013-09-04T00:00',
      validToDate: '2015-09-04T00:00',
      valid: nomenclatures.get('boolean', 'false'),
      radios: {
        aircraftRadioType: nomenclatures.get('aircraftRadioTypes', 'RT1'),
        count: 3,
        producer: 'Pioneer',
        model: 'ACS-200r 2',
        power: '5000 W',
        'class': 'Висок',
        bandwidth: '10000 MHz',
      }
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-cert-radios.sample'] = {}) : module);
