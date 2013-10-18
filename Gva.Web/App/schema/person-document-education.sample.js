/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Education: {
      documentNumber: '1',
      completionDate: '1981-04-04T00:00',
      speciality: 'пилот',
      schoolId: nomenclatures.getId('Schools', 'BAC'),
      graduationId: nomenclatures.getId('Graduations', 'PQ'),
      notes: '',
      
      bookPageNumber: '2',
      pageCount: 1
    }
  };
})(typeof module === 'undefined' ? (this['person-document-education.sample'] = {}) : module);