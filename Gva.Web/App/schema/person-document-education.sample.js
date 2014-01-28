/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Education: {
      documentNumber: '1',
      completionDate: '1981-04-04T00:00',
      speciality: 'пилот',
      school: nomenclatures.get('schools', 'BAC'),
      graduation: nomenclatures.get('graduations', 'HS'),
      notes: '',
      bookPageNumber: '2',
      pageCount: 1
    }
  };
})(typeof module === 'undefined' ? (this['person-document-education.sample'] = {}) : module);
