/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Mark1: {
      ltrInNumber: '(1071)',
      ltrInDate: '1995-09-04T00:00',
      ltrCaaNumber: '(1071)',
      ltrCaaDate: '1995-09-04T00:00',
      mark: 'LZ-FED',
      valid: nomenclatures.get('boolean', 'false'),
    },
    aircraft1Mark2: {
      ltrInNumber: '(1115)',
      ltrInDate: '1996-09-04T00:00',
      ltrCaaNumber: '(1115)',
      ltrCaaDate: '1996-09-04T00:00',
      mark: 'LZ-YUK',
      valid: nomenclatures.get('boolean', 'true'),
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-cert-marks.sample'] = {}) : module);
