/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');
  var auditDetails = [
        {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '14',
          subject: nomenclatures.get('auditPartRequirements', '"Buildings"')
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '15',
          subject: nomenclatures.get('auditPartRequirements', 'staff')
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '16',
          subject: nomenclatures.get('auditPartRequirements', 'cert staff')
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '17',
          subject: nomenclatures.get('auditPartRequirements', 'equipment')
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '18',
          subject: nomenclatures.get('auditPartRequirements', 'components acceptance')
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '19',
          subject: nomenclatures.get('auditPartRequirements', 'data for TO')
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '20',
          subject: nomenclatures.get('auditPartRequirements', 'production planning')
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '21',
          subject: nomenclatures.get('auditPartRequirements', 'cert for TO')
        },{
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '22',
          subject: nomenclatures.get('auditPartRequirements', 'technical notes')
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '23',
          subject: nomenclatures.get('auditPartRequirements', 'events announcement')
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '24',
          subject: nomenclatures.get('auditPartRequirements', 'procedures and quality')
        }
  ];

  module.exports = {
    organization1Inspection1: {
      documentNumber: '2341',
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
      auditDetails: auditDetails,
      examiners: [{
        sortOrder: 1,
        examiner: nomenclatures.get('examiners', '28')
      }],
      disparities: []
    },
    organization1Inspection2: {
      documentNumber: '2341а',
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
      auditDetails: auditDetails,
      examiners: [{
        sortOrder: 2,
        examiner: nomenclatures.get('examiners', '28')
      }],
      disparities: []
    }
  };
})(typeof module === 'undefined' ? (this['organization-inspections.sample'] = {}) : module);
