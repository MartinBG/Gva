/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Status1: {
      personStatusType: nomenclatures.get('personStatusTypes', 'permanently unfit'),
      documentNumber: '1',
      documentDateValidFrom: '1912-10-07T00:00',
      documentDateValidTo: '1912-12-24T00:00',
      notes: 'note1'
    },
    person1Status2: {
      personStatusType: nomenclatures.get('personStatusTypes', 'maternity leave'),
      documentNumber: '2',
      documentDateValidFrom: '1812-04-04T00:00',
      documentDateValidTo: '1812-05-04T00:00',
      notes: 'note2'
    },
    person1Status3: {
      personStatusType: nomenclatures.get('personStatusTypes', 'maternity leave'),
      documentNumber: '32',
      documentDateValidFrom: '1922-11-04T00:00',
      documentDateValidTo: '2012-12-15T00:00',
      notes: 'note3'
    },
    person1Status4: {
      personStatusType: nomenclatures.get('personStatusTypes', 'maternity leave'),
      documentNumber: '21',
      documentDateValidFrom: '2012-09-04T00:00',
      documentDateValidTo: '2812-05-14T00:00',
      notes: 'note4'
    }
  };
})(typeof module === 'undefined' ? (this['person-status.sample'] = {}) : module);
