/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    organization1Approval1: {
      organizationType: nomenclatures.get('organizationTypes', 'LAP'),
      documentNumber: 'fs3',
      approvalState: nomenclatures.get('approvalStates', '1'),
      documentDateIssue: '2010-03-06T00:00',
      approvalStateDate: '2012-05-07T00:00',
      approvalStateNote: 'notes..',
    }
  };
})(typeof module === 'undefined' ? (this['organization-approval.sample'] = {}) : module);