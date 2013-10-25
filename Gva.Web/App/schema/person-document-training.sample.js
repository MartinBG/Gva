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
      staffTypeId : nomenclatures.getId('staffTypes', 'Crew'),
      ratingTypeId : null,
      ratingClassId : null,
      authorizationId : null,
      licenceTypeId : null,
      engLangLevelId: null,
      personOtherDocumentTypeId: nomenclatures.getId('personOtherDocumentTypes', 'CtrlTalon'),
      personOtherDocumentRoleId: nomenclatures.getId('personOtherDocumentRoles', 'Training'),
      valid: true,
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
      staffTypeId : nomenclatures.getId('staffTypes', 'Crew'),
      ratingTypeId : null,
      ratingClassId : null,
      authorizationId : null,
      licenceTypeId : nomenclatures.getId('licenceTypes', 'CPA'),
      extraDataOVD : null,
      extraDataAML : null,
      extraDataSUVD : null,
      engLangLevelId: null,
      personOtherDocumentTypeId: nomenclatures.getId('personOtherDocumentTypes', 'CtrlTalon'),
      personOtherDocumentRoleId: nomenclatures.getId('personOtherDocumentRoles', 'Training'),
      valid: true,
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
      staffTypeId : nomenclatures.getId('staffTypes', 'Crew'),
      ratingTypeId : null,
      ratingClassId : null,
      authorizationId : null,
      licenceTypeId : nomenclatures.getId('licenceTypes', 'CPA'),
      extraDataOVD : null,
      extraDataAML : null,
      extraDataSUVD : null,
      engLangLevelId: null,
      personOtherDocumentTypeId: nomenclatures.getId('personOtherDocumentTypes', 'CtrlCard'),
      personOtherDocumentRoleId: nomenclatures.getId('personOtherDocumentRoles', 'Training'),
      valid: true,
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
      staffTypeId : nomenclatures.getId('staffTypes', 'Crew'),
      ratingTypeId : null,
      ratingClassId : null,
      authorizationId : null,
      licenceTypeId : nomenclatures.getId('licenceTypes', 'CPA'),
      engLangLevelId: null,
      personOtherDocumentTypeId: nomenclatures.getId('personOtherDocumentTypes', 'Certificate'),
      personOtherDocumentRoleId: nomenclatures.getId('personOtherDocumentRoles', 'TheoreticalTraining'),
      valid: true,
      notes: '',
    
      bookPageNumber: '79',
      pageCount: 1
    }

  };
})(typeof module === 'undefined' ? (this['person-document-education.sample'] = {}) : module);