/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Smod1: {
      ltrInNumber: 'МИГР 04.09.2009',
      ltrInDate: '2009-09-04T00:00',
      ltrCaaNumber: 'МИГР 04.09.2009',
      ltrCaaDate: '2009-09-04T00:00',
      caaTo: '',
      caaToAddress: '',
      caaJob: '',
      scode: '010001010001110001101010',
      valid: nomenclatures.get('boolean', 'false')
    },
    aircraft1Smod2: {
      ltrInNumber: 'МИГР 04.09.2010',
      ltrInDate: '2010-09-04T00:00',
      ltrCaaNumber: 'МИГР 04.09.2010',
      ltrCaaDate: '2010-09-04T00:00',
      caaTo: '',
      caaToAddress: '',
      caaJob: '',
      scode: '010001010001110001101011',
      valid: nomenclatures.get('boolean', 'true')
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-cert-smods.sample'] = {}) : module);
