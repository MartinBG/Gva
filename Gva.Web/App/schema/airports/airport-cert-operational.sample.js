/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    airport1Oper1: {
      issueDate: '2013-09-04T00:00',
      issueNumber: '333',
      valiToDate: '2018-09-04T00:00',
      audit: '333',
      organization: nomenclatures.get('organizations', 'AAK Progres'),
      inspector: nomenclatures.get('inspectors', 'Vladimir'),
      valid: nomenclatures.get('boolean', 'false'),
      includedDocuments: [
        {
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          approvalDate: '2013-09-04T00:00', 
          linkedDocument: ''
        }
      ],
      ext: {
        date: '2013-07-24T00:00',
        validToDate: '2018-01-14T00:00',
        inspector: nomenclatures.get('inspectors', 'Vladimir'),
      },
      revokeDate: '2013-09-04T00:00',
      revokeInspector: nomenclatures.get('inspectors', 'Vladimir'),
      revokeCause: ''
    },
    airport1Oper2: {
      issueDate: '2014-01-14T00:00',
      issueNumber: '333',
      valiToDate: '2018-12-22T00:00',
      audit: '334',
      organization: nomenclatures.get('organizations', 'Wizz Air'),
      inspector: nomenclatures.get('inspectors', 'Georgi'),
      valid: nomenclatures.get('boolean', 'true'),
      includedDocuments: [
        {
          inspector: nomenclatures.get('inspectors', 'Georgi'),
          approvalDate: '2014-02-04T00:00', 
          linkedDocument: ''
        }
      ],
      ext: {
        date: '2014-03-14T00:00',
        validToDate: '2018-10-14T00:00',
        inspector: nomenclatures.get('inspectors', 'Georgi'),
      },
      revokeDate: '2014-03-24T00:00',
      revokeInspector: nomenclatures.get('inspectors', 'Georgi'),
      revokeCause: ''
    }
  };
})(typeof module === 'undefined' ? (this['airport-cert-operational.sample'] = {}) : module);
