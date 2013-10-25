/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Id: {
      documentNumber: '6765432123',
      documentDateValidFrom: '2010-04-04T00:00',
      documentDateValidTo: '2020-04-04T00:00',
      documentPublisher: 'МВР София',
      personDocumentIdTypeId: nomenclatures.getId('personIdDocumentTypes', 'Id'),
      valid: true,
      notes: '',
      bookPageNumber: '3',
      pageCount: 1
    }
  };
})(typeof module === 'undefined' ? (this['person-document-id.sample'] = {}) : module);
