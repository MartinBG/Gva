/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Airworthiness1: {
      certId: '5',
      issueDate: '2013-09-04T00:00',
      validFromDate: '2013-09-04T00:00',
      validToDate: '2015-09-04T00:00',
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      incomingDocNumber: 'D-122а',
      incomingDocDate: '2013-09-05T00:00',
      EASA25IssueDate: '2013-09-06T00:00',
      EASA24IssueDate: '2013-09-07T00:00',
      EASA24IssueValidToDate: '2013-09-08T00:00',
      EASA15IssueDate: '2013-09-09T00:00',
      EASA15IssueValidToDate: '2013-09-10T00:00',
      EASA15IssueRefNo: '7013.15.2469.01'
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-cert-airworthinessesfm.sample'] = {}) : module);
