/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Check1: {
      documentNumber: '11232',
      documentPersonNumber: '7005159385',
      documentDateValidFrom: '1970-05-02T00:00',
      documentDateValidTo: '1970-05-15T00:00',
      documentPublisher: 'Проверяващ',
      staffType: nomenclatures.get('staffTypes', 'G'),
      ratingType: nomenclatures.get('ratingTypes', 'B737'),
      aircraftTypeGroup: nomenclatures.get('aircraftTypeGroups', 'NoType'),
      ratingClass: '',
      authorization: nomenclatures.get('authorizations', 'FI(A)'),
      licenceType: nomenclatures.get('licenceTypes', 'PPLA'),
      locationIndicator: nomenclatures.get('locationIndicators', 'LBBG'),
      sector: 'Сектор1',
      personCheckRatingValue: nomenclatures.get('personCheckRatingValues', 'good'),
      personCheckDocumentType: nomenclatures.get('personCheckDocumentTypes', 'BaseTrainingForm'),
      personCheckDocumentRole: nomenclatures.get('personCheckDocumentRoles', 'FlightTest'),
      valid: nomenclatures.get('boolean', 'true'),
      notes: 'Test notes',
      bookPageNumber: '3',
      pageCount: '5'
      
    },
    person1Check2: {
      documentNumber: '454',
      documentPersonNumber: '343224',
      documentDateValidFrom: '2000-05-02T00:00',
      documentDateValidTo: '2005-05-15T00:00',
      documentPublisher: 'МВР Бургас',
      staffType: nomenclatures.get('staffTypes', 'F'),
      ratingType: '',
      aircraftTypeGroup: nomenclatures.get('aircraftTypeGroups', 'PWCPT6'),
      ratingClass: nomenclatures.get('ratingClasses', 'C'),
      authorization: nomenclatures.get('authorizations', 'CATII'),
      licenceType: nomenclatures.get('licenceTypes', 'ATPL'),
      locationIndicator: nomenclatures.get('locationIndicators', 'LBBO'),
      sector: 'Сектор2',
      personCheckRatingValue: nomenclatures.get('personCheckRatingValues', 'unsufficient'),
      personCheckDocumentType: nomenclatures.get('personCheckDocumentTypes', 'Authorisation'),
      personCheckDocumentRole: nomenclatures.get('personCheckDocumentRoles', 'PracticalCheck'),
      valid: nomenclatures.get('boolean', 'false'),
      notes: 'Test notes 2',
      bookPageNumber: '2',
      pageCount: '6'
    }
  };
})(typeof module === 'undefined' ? (this['person-document-checks.sample'] = {}) : module);
