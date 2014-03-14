/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Debt1: {
      certId: '123',
      regDate: '2010-09-04T00:00',
      regTime: {
        hours: '12',
        minutes: '13'
      },
      aircraftDebtType:  nomenclatures.get('aircraftDebtTypes', 'DT1'),
      contractNumber: 'D-123',
      contractDate: '2012-03-14T00:00',
      startDate: '2012-03-14T00:00',
      startReason: '',
      startReasonAlt: '',
      notes: '',
      bookPageNumber: '1',
      pageCount: 1,
      ltrNumber: 'LTR-123',
      ltrDate: '2011-03-14T00:00',
      endReason: '',
      endDate: '',
      closeApplicationReason: '',
      closeApplicationDate: '',
      closeCaaApplicationReason: '',
      closeCaaApplicationDate: '',
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      closeInspector: '',
      creditorName: 'Петър Петров',
      creditorNameAlt: 'Petar Petrov',
      creditorAddress: '',
      creditorEmail: '',
      creditorContact: '',
      creditorPhone: ''
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-document-debts.sample'] = {}) : module);
