/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Airworthiness1: {
      aircraftCertificateType: nomenclatures.get('aircraftCertificateTypes', 'CT1'),
      certId: '5',
      certNumber: 2231,
      refNumber: 'BG.MG.26.15a.33421.1',
      issueDate: '2013-09-04T00:00',
      validToDate: '2015-09-04T00:00',
      auditId: '',
      approvalId: '',
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      valid: '1995-09-04T00:00',
      ext1Date: '2012-09-04T00:00',
      ext1ValidToDate: '2013-09-04T00:00',
      ext1ApprovalId: '',
      ext1Inspector: '',
      ext2Date: '',
      ext2ValidToDate: '',
      ext2ApprovalId: '',
      ext2Inspector: '',
      country: '',
      exemptions: '',
      exemptionsAlt: '',
      specialReq: '',
      specialReqAlt: '',
      revokeDate: '',
      revokeInspector: '',
      revokeCause: ''
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-cert-airworthinesses.sample'] = {}) : module);
