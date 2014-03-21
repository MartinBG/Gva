/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    organization1Amendment1: {
      organizationType: nomenclatures.get('organizationTypes', 'LAP'),
      documentNumber: 'fs32',
      documentDateIssue: '2010-04-06T00:00',
      changeNum: 2,
      lims147: [
        {
          lim147limitation: nomenclatures.get('lim147limitations', 'A1'),
          lim147limitationText: 'тест4, тест3',
          sortOrder: 1
        },
        {
          lim147limitation: nomenclatures.get('lim147limitations', 'A2'),
          lim147limitationText: 'тест1, тест2',
          sortOrder: 2
        }
      ],
      lims145: [{
        lim145limitation: nomenclatures.get('lim145limitations', 'A2'),
        lim145limitationText: '',
        base: nomenclatures.get('boolean', 'true'),
        line: nomenclatures.get('boolean', 'true')
      }],
      limsMG: [{
        typeAC: 'тестов тип',
        qualitySystem: 'огранизация 1',
        awapproval: nomenclatures.get('boolean', 'true'),
        pfapproval: nomenclatures.get('boolean', 'true')
      }],
      includedDocuments: [
        {
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          approvalDate: '2011-04-06T00:00',
          linkedLim: 2,
          linkedDocumentId: 4
        },
        {
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          approvalDate: '2011-04-06T00:00',
          linkedLim: 3,
          linkedDocumentId: 5
        }
      ]
    },
    organization1Amendment2: {
      organizationType: nomenclatures.get('organizationTypes', 'LAP'),
      documentNumber: '67',
      documentDateIssue: '2012-06-07T00:00',
      changeNum: 2,
      lims147: [],
      lims145: [],
      limsMG: [],
      includedDocuments: []
    }
  };
})(typeof module === 'undefined' ? (this['organization-amendment.sample'] = {}) : module);