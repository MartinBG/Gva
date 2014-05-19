/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    organization1CertAirportOperator1: {
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
      airportoperatorActivityTypes: [
        nomenclatures.get('airportoperatorActivityTypes', 'A1'),
        nomenclatures.get('airportoperatorActivityTypes', 'A2'),
      ],
      includedDocuments: [
        {
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          approvalDate: '2011-04-06T00:00',
          linkedDocument: 2
        },
        {
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          approvalDate: '2014-05-02T00:00',
          linkedDocument: 3
        }
      ],
      revokeDate: '2014-02-06T00:00',
      revokeinspector: nomenclatures.get('inspectors', 'Vladimir')
    },
    organization1CertAirportOperator2: {
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
      airportoperatorActivityTypes: [
        nomenclatures.get('airportoperatorActivityTypes', 'A1'),
        nomenclatures.get('airportoperatorActivityTypes', 'A2'),
      ],
      includedDocuments: [
        {
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          approvalDate: '2011-04-06T00:00',
          linkedDocument: 2
        },
        {
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          approvalDate: '2014-05-02T00:00',
          linkedDocument: 3
        }
      ],
      revokeDate: '2014-02-06T00:00',
      revokeinspector: nomenclatures.get('inspectors', 'Vladimir')
    }
  };
})(typeof module === 'undefined' ? (this['organization-cert-airportoperator.sample'] = {}) : module);