/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample'),
    auditDetails1 = [
        {
          auditResult: nomenclatures.get('auditResults', 'Small corrective actions are required'),
          code: '14',
          subject: nomenclatures.get('auditPartRequirmants', 'Buildings'),
          disparities: [1, 2]
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '15',
          subject: nomenclatures.get('auditPartRequirmants', 'staff'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '16',
          subject: nomenclatures.get('auditPartRequirmants', 'cert staff'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '17',
          subject: nomenclatures.get('auditPartRequirmants', 'equipment'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '18',
          subject: nomenclatures.get('auditPartRequirmants', 'components acceptance'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '19',
          subject: nomenclatures.get('auditPartRequirmants', 'data for TO'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '20',
          subject: nomenclatures.get('auditPartRequirmants', 'production planning'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '21',
          subject: nomenclatures.get('auditPartRequirmants', 'cert for TO'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '22',
          subject: nomenclatures.get('auditPartRequirmants', 'technical notes'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '23',
          subject: nomenclatures.get('auditPartRequirmants', 'events announcement'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '24',
          subject: nomenclatures.get('auditPartRequirmants', 'procedures and quality'),
          disparities: []
        }
    ],
    disparities1 = [
      {
        subject: nomenclatures.get('auditPartRequirmants', 'Buildings'),
        sortOrder: 1,
        refNumber: '121',
        description: 'description test',
        disparityLevel: nomenclatures.get('disparityLevels', '0')
      },
      {
        subject: nomenclatures.get('auditPartRequirmants', 'Buildings'),
        refNumber: '122',
        sortOrder: 2,
        description: 'description test2',
        disparityLevel: nomenclatures.get('disparityLevels', '2')
      }
    ],
    disparities2 = [
      {
        subject: nomenclatures.get('auditPartRequirmants', 'Buildings'),
        sortOrder: 1,
        refNumber: '121a',
        description: 'description test2A',
        disparityLevel: nomenclatures.get('disparityLevels', '1')
      },
      {
        subject: nomenclatures.get('auditPartRequirmants', 'Buildings'),
        refNumber: '122b',
        sortOrder: 2,
        description: 'description test3A',
        disparityLevel: nomenclatures.get('disparityLevels', '1')
      }
    ],
    auditDetails2 = auditDetails1;

  module.exports = {
    organization1Inspection1: {
      documentNumber: '2341',
      auditPart: nomenclatures.get('auditParts', '145'),
      auditReason: nomenclatures.get('auditReasons', 'Change'),
      auditType: nomenclatures.get('auditTypes', 'Planned'),
      subject: 'Тестов предмет',
      auditState: nomenclatures.get('auditStates', 'Planned'),
      notification: nomenclatures.get('boolean', 'true'),
      startDate: '1981-04-04T00:00',
      endDate:'1982-04-04T00:00',
      inspectionPlace: 'София, кв. Света Тройца',
      inspectionFrom: '1991-04-04T00:00',
      inspectionTo: '1991-08-04T00:00',
      auditDetails: auditDetails1,
      examiners: [{
        sortOrder: 1,
        examiner: nomenclatures.get('examiners', '28')
      }],
      disparities: disparities1
    },
    organization1Inspection2: {
      documentNumber: '2341а',
      auditPart: nomenclatures.get('auditParts', 'Trial'),
      auditReason: nomenclatures.get('auditReasons', 'Superintendance'),
      auditType: nomenclatures.get('auditTypes', 'Planned'),
      subject: 'Тестов предмет',
      auditState: nomenclatures.get('auditStates', 'Running'),
      notification: nomenclatures.get('boolean', 'false'),
      startDate: '2001-04-04T00:00',
      endDate: '2002-04-04T00:00',
      inspectionPlace: 'София, кв. Модерни предградия',
      inspectionFrom: '2002-04-04T00:00',
      inspectionTo: '2002-08-04T00:00',
      auditDetails: auditDetails2,
      examiners: [{
        sortOrder: 2,
        examiner: nomenclatures.get('examiners', '28')
      }],
      disparities: disparities2
    }
  };
})(typeof module === 'undefined' ? (this['organization-inspections.sample'] = {}) : module);
