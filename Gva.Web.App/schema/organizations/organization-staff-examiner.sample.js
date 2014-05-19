/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    staffExaminer1: {
      nomValueId: 56,
      code: '1a',
      name: 'Василка Жилева',
      nameAlt: 'Василка Жилева',
      alias: 'Wasi',
      valid: nomenclatures.get('boolean', 'true'),
      person: nomenclatures.get('persons', 'P1'),
      content: {
        stampNumber: '1a',
        organization: nomenclatures.get('organizations', 'AAK Progres'),
        permitedAW: nomenclatures.get('boolean', 'true'),
        permitedCheck: nomenclatures.get('boolean', 'false'),
      }
    },
    staffExaminer2: {
      nomValueId: 58,
      code: '2b',
      name: 'Васил Костов',
      nameAlt: 'Васил Костов',
      alias: 'WasKo',
      valid: nomenclatures.get('boolean', 'false'),
      person: nomenclatures.get('persons', 'P2'),
      content: {
        stampNumber: '2b',
        organization: nomenclatures.get('organizations', 'AAK Progres'),
        permitedAW: nomenclatures.get('boolean', 'false'),
        permitedCheck: nomenclatures.get('boolean', 'true'),
      }
    }
  };
})(typeof module === 'undefined' ? (this['organization-staff-examiner.sample'] = {}) : module);