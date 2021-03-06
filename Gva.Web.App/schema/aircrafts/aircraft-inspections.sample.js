﻿/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');
  var auditDetails = [
        {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '1',
          subject: nomenclatures.get('auditPartRequirements', 'Cockpit'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '2',
          subject: nomenclatures.get('auditPartRequirements', 'Facilities'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '3',
          subject: nomenclatures.get('auditPartRequirements', 'Fuselage'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '4',
          subject: nomenclatures.get('auditPartRequirements', 'Wings'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '5',
          subject: nomenclatures.get('auditPartRequirements', 'Stabilizers'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '6',
          subject: nomenclatures.get('auditPartRequirements', 'Landing'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '7',
          subject: nomenclatures.get('auditPartRequirements', 'Engines'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '8',
          subject: nomenclatures.get('auditPartRequirements', 'Baggage'),
          disparities: []
        },{
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '9',
          subject: nomenclatures.get('auditPartRequirements', 'Additional compartments'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '10',
          subject: nomenclatures.get('auditPartRequirements', 'CEA'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '11',
          subject: nomenclatures.get('auditPartRequirements', 'Accordance with the requirements'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '12',
          subject: nomenclatures.get('auditPartRequirements', 'Tests'),
          disparities: []
        }, {
          auditResult: nomenclatures.get('auditResults', 'Not executed'),
          code: '13',
          subject: nomenclatures.get('auditPartRequirements', 'Other specific'),
          disparities: []
        }
  ];

  module.exports = {
    aircraft1Inspection1: {
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
    aircraft1Inspection2: {
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
})(typeof module === 'undefined' ? (this['aircraft-inspections.sample'] = {}) : module);
