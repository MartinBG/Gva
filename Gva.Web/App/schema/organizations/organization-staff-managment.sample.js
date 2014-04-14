/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    staffManagement1: {
      auditPartRequirement: nomenclatures.get('auditPartRequirements', 'Cockpit'),
      planYear: '2015',
      planMonth: 'Декември',
      position: 'тест длъжност',
      person: nomenclatures.get('persons', 'P1'),
      testDate: '2010-03-06T00:00',
      testScore: nomenclatures.get('boolean', 'true'),
      number: 2,
      valid: nomenclatures.get('boolean', 'true')
    },
    staffManagement2: {
      auditPartRequirement: nomenclatures.get('auditPartRequirements', 'Facilities'),
      planYear: '2012',
      planMonth: 'Април',
      position: 'тест длъжност2',
      person: nomenclatures.get('persons', 'P2'),
      testDate: '2010-03-06T00:00',
      testScore: nomenclatures.get('boolean', 'false'),
      number: 3,
      valid: nomenclatures.get('boolean', 'false')
    }
  };
})(typeof module === 'undefined' ? (this['organization-staff-managment.sample'] = {}) : module);