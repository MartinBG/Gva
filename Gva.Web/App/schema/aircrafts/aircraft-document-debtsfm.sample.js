/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Debt1: {
      certId: '123',
      regDate: '2005-09-04T00:00',
      regTime: {
        hours: '12',
        minutes: '13'
      },
      aircraftDebtType:  nomenclatures.get('aircraftDebtTypes', 'DT1'),
      documentNumber: 'D-123',
      documentDate: '2012-03-14T00:00',
      aircraftCreditor:  nomenclatures.get('aircraftCreditors', 'C1'),
      creditorDocument: '',
      inspector: nomenclatures.get('inspectors', 'Vladimir')
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-document-debtsfm.sample'] = {}) : module);
