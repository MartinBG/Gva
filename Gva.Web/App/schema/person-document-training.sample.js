/*global module, require*/
/*jshint maxlen:false*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Training1: {
      documentNumber: 'BG FCL/CPA-00001-11232',
      documentPersonNumber: null,
      documentDateValidFrom: '2013-02-27T00:00',
      documentDateValidTo: null,
      documentPublisher: 'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)',
      staffType : nomenclatures.get('staffTypes', 'F'),
      ratingType: nomenclatures.get('ratingTypes', 'B737'),
      ratingClass : null,
      authorization: nomenclatures.get('authorizations', 'FI(A)'),
      licenceType : null,
      engLangLevel: null,
      personOtherDocumentType: nomenclatures.get('personOtherDocumentTypes', 'CtrlTalon'),
      personOtherDocumentRole: nomenclatures.get('documentRoles', 'Training'),
      valid: nomenclatures.get('boolean', 'true'),
      notes: '',
      bookPageNumber: '87',
      pageCount: 1
    },

    person1Training2: {
      documentNumber: 'BG CPA 00185-11232',
      documentPersonNumber: null,
      documentDateValidFrom: '2012-03-12T00:00',
      documentDateValidTo: '2016-03-02T00:00',
      documentPublisher: 'ВА: ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ (BG)',
      staffType: nomenclatures.get('staffTypes', 'F'),
      ratingType : null,
      ratingClass : null,
      authorization : null,
      licenceType : nomenclatures.get('licenceTypes', 'CPA'),
      extraDataOVD : null,
      extraDataAML : null,
      extraDataSUVD : null,
      engLangLevel: null,
      personOtherDocumentType: nomenclatures.get('personOtherDocumentTypes', 'CtrlTalon'),
      personOtherDocumentRole: nomenclatures.get('documentRoles', 'Training'),
      valid: nomenclatures.get('boolean', 'true'),
      notes: '',
      bookPageNumber: '81',
      pageCount: 1
    },

    person1Training3: {
      documentNumber: '5-6448',
      documentPersonNumber: null,
      documentDateValidFrom: '2012-03-12T00:00',
      documentDateValidTo: null,
      documentPublisher: 'Инспектор:  Георги Мишев Христов Код:46',
      staffType: nomenclatures.get('staffTypes', 'F'),
      ratingType : null,
      ratingClass : null,
      authorization: nomenclatures.get('authorizations', 'FI(A)'),
      licenceType : nomenclatures.get('licenceTypes', 'CPA'),
      extraDataOVD : null,
      extraDataAML : null,
      extraDataSUVD : null,
      engLangLevel: null,
      personOtherDocumentType: nomenclatures.get('personOtherDocumentTypes', 'CtrlCard'),
      personOtherDocumentRole: nomenclatures.get('documentRoles', 'Training'),
      valid: nomenclatures.get('boolean', 'false'),
      notes: '',
      bookPageNumber: '79',
      pageCount: 1
    },

    person1Training4: {
      documentNumber: '80',
      documentPersonNumber: null,
      documentDateValidFrom: '2012-03-10T00:00',
      documentDateValidTo: null,
      documentPublisher: 'УЦ: Ратан',
      staffType: nomenclatures.get('staffTypes', 'F'),
      ratingType : null,
      ratingClass : null,
      authorization : null,
      licenceType : nomenclatures.get('licenceTypes', 'CPA'),
      engLangLevel: null,
      personOtherDocumentType: nomenclatures.get('personOtherDocumentTypes', 'Certificate'),
      personOtherDocumentRole: nomenclatures.get('documentRoles', 'TheoreticalTraining'),
      valid: nomenclatures.get('boolean', 'true'),
      notes: '',
      bookPageNumber: '79',
      pageCount: 1
    }

  };
})(typeof module === 'undefined' ? (this['person-document-training.sample'] = {}) : module);