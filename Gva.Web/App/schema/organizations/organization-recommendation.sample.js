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
      includedAudits: []
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
      includedAudits: []
    }
  };
})(typeof module === 'undefined' ? (this['organization-recommendation.sample'] = {}) : module);