/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Exam1: {
      documentNumber: '11232dd',
      documentDateValidFrom: '1970-05-02T00:00',
      documentPublisher: 'Проверяващ',
      valid: nomenclatures.get('boolean', 'true'),
      notes: 'Test notes',
      bookPageNumber: '3',
      pageCount: '5'
    },
    person1Exam2: {
      documentNumber: '123232dd',
      documentDateValidFrom: '2000-05-02T00:00',
      documentPublisher: 'Проверяващ2',
      valid: nomenclatures.get('boolean', 'false'),
      notes: 'Test notes2',
      bookPageNumber: '4',
      pageCount: '7'
    }
  };
})(typeof module === 'undefined' ? (this['person-document-exam.sample'] = {}) : module);
