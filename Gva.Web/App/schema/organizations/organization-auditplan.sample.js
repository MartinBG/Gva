/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    organization1Auditplan1: {
      auditPartRequirement: nomenclatures.get('auditPartRequirements', 'Cockpit'),
      planYear: '2015',
      planMonth: 'Декември'
    },
    organization1Auditplan2: {
      auditPartRequirement: nomenclatures.get('auditPartRequirements', 'Facilities'),
      planYear: '2015',
      planMonth: 'Декември'
    }
  };
})(typeof module === 'undefined' ? (this['organization-auditplan.sample'] = {}) : module);