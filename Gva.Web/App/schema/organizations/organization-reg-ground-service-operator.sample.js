/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    register1: {
      certNumber: 3,
      issueDate: '2002-04-04T00:00',
      validToDate: '2014-04-04T00:00',
      organization: nomenclatures.get('organizations', 'AAK Progres'),
      address: 'адрес1 ',
      revokeDate: '2002-04-04T00:00',
      revokeCause: 'Причина 1'
    },
    register2: {
      certNumber: 5,
      issueDate: '2008-04-04T00:00',
      validToDate: '2009-05-04T00:00',
      organization: nomenclatures.get('organizations', 'AAK Progres'),
      address: 'адрес2 ',
    }
  };
})(typeof module === 'undefined' ? (this['organization-reg-ground-service-operator.sample'] = {}) : module);