/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Status: {
      personStatusTypeId: nomenclatures.getId('PersonStatusTypes', 'Disabled'),
      documentNumber: '2',
      documentDateValidFrom: '1912-04-04T00:00',
      documentDateValidTo: '1912-05-04T00:00',
      notes: ''
    }
  };
})(typeof module === 'undefined' ? (this['person-status.sample'] = {}) : module);
