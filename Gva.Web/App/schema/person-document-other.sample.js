/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Doc1: {
      documentNumber: '',
      documentDateValidFrom: '2010-03-06T00:00',
      documentDateValidTo: null,
      documentPublisher: 'УЦ: Български въздухоплавателен център',
      personOtherDocumentTypeId: nomenclatures.getId('documentTypes', 'Protocol'),
      personOtherDocumentRoleId: nomenclatures.getId('documentRoles', 'FlightTest'),
      valid: true,
      notes: '',
      bookPageNumber: '62',
      pageCount: 1
    },
    person1Doc2: {
      documentNumber: '24',
      documentDateValidFrom: '2010-09-17T00:00',
      documentDateValidTo: null,
      documentPublisher: 'Проверяващ: Кръстю Георгиев Нешев Код:11225',
      personOtherDocumentTypeId: nomenclatures.getId('documentTypes', 'Protocol'),
      personOtherDocumentRoleId: nomenclatures.getId('documentRoles', 'FlightTest'),
      valid: true,
      notes: '',
      bookPageNumber: '63',
      pageCount: 1
    },
    person1Doc3: {
      documentNumber: '24',
      documentDateValidFrom: '2010-09-17T00:00',
      documentDateValidTo: null,
      documentPublisher: 'Проверяващ: Кръстю Георгиев Нешев Код:11225',
      personOtherDocumentTypeId: nomenclatures.getId('documentTypes', 'CtrlTalon'),
      personOtherDocumentRoleId: nomenclatures.getId('documentRoles', 'FlightTest'),
      valid: true,
      notes: '',
      bookPageNumber: '63',
      pageCount: 1
    }
  };
})(typeof module === 'undefined' ? (this['person-document-other.sample'] = {}) : module);
