/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    organization1CertCertGroundServiceOperator1: {
      certNumber: 1,
      issueDate: '2010-03-06T00:00',
      validToDate: '2015-03-06T00:00',
      audit: nomenclatures.get('audits', 'A1'),
      organization: nomenclatures.get('organizations', 'AAK Progres'),
      airport: nomenclatures.get('airports', 'AAK A2'),
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      valid: nomenclatures.get('boolean', 'false'),
      ext: {
        date: '2011-02-06T00:00',
        validToDate: '2015-04-06T00:00',
        inspector: nomenclatures.get('inspectors', 'Vladimir')
      },
      groundServiceOperatorActivityTypes: [
        nomenclatures.get('groundServiceOperatorActivityTypes', 'A1'),
        nomenclatures.get('groundServiceOperatorActivityTypes', 'A2'),
      ],
      includedDocuments: [
        {
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          approvalDate: '2011-04-06T00:00',
          linkedDocumentId: 2
        },
        {
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          approvalDate: '2014-05-02T00:00',
          linkedDocumentId: 3
        }
      ],
      revokeDate: '2014-02-06T00:00',
      revokeinspector: nomenclatures.get('inspectors', 'Vladimir')
    },
    organization1CertGroundServiceOperator2: {
      certNumber: 1,
      issueDate: '2009-03-06T00:00',
      validToDate: '2015-03-06T00:00',
      audit: nomenclatures.get('audits', 'A1'),
      organization: nomenclatures.get('organizations', 'AAK Progres'),
      airport: nomenclatures.get('airports', 'AAK A2'),
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      valid: nomenclatures.get('boolean', 'true'),
      ext: {
        date: '2011-02-06T00:00',
        validToDate: '2015-04-06T00:00',
        inspector: nomenclatures.get('inspectors', 'Vladimir')
      },
      groundServiceOperatorActivityTypes: [
        nomenclatures.get('groundServiceOperatorActivityTypes', 'A1'),
        nomenclatures.get('groundServiceOperatorActivityTypes', 'A2'),
      ],
      includedDocuments: [
        {
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          approvalDate: '2011-04-06T00:00',
          linkedDocumentId: 2
        },
        {
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          approvalDate: '2014-05-02T00:00',
          linkedDocumentId: 3
        }
      ],
      revokeDate: '2014-02-06T00:00',
      revokeinspector: nomenclatures.get('inspectors', 'Vladimir')
    }
  };
})(typeof module === 'undefined' ? (this['organization-cert-groundserviceoperator.sample'] = {}) : module);