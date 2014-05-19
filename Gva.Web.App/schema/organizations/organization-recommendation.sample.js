/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    recommendation1: {
      auditorsReview: {
        auditDetails: [],
        disparities: []
      },
      descriptionReview: {
        auditDetails: [],
        disparities: []
      },
      part1: { examiners: [] },
      part2: { examiners: [] },
      part3: { examiners: [] },
      part4: { examiners: [] },
      part5: { examiners: [] },
      includedAudits: [],
      recommendationPart: nomenclatures.get('auditParts', 'BC - ACAM'),
      formDate: '2000-04-04T00:00',
      formText: 'a23',
      interviewedStaff: 'Маги Дерменджиева',
      fromDate: '2005-04-07T00:00',
      toDate: '2008-04-07T00:00',
      documentDescription: 'Описание тест',
      recommendation: 'Препоръка тест'
    },
    recommendation2: {
      auditorsReview: {
        auditDetails: [],
        disparities: []
      },
      descriptionReview: {
        auditDetails: [],
        disparities: []
      },
      part1: { examiners: [] },
      part2: { examiners: [] },
      part3: { examiners: [] },
      part4: { examiners: [] },
      part5: { examiners: [] },
      includedAudits: [],
      recommendationPart: nomenclatures.get('auditParts', 'm/f'),
      formDate: '2000-04-04T00:00',
      formText: '3',
      interviewedStaff: 'Иван Иванов',
      fromDate: '2000-05-07T00:00',
      toDate: '2001-06-07T00:00',
      documentDescription: 'Описание тест 2',
      recommendation: 'Препоръка тест 2'
    }
  };
})(typeof module === 'undefined' ? (this['organization-recommendation.sample'] = {}) : module);