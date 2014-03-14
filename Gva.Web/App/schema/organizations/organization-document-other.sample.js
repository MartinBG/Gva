/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    organization1Doc1: {
      documentNumber: '1221',
      documentPersonNumber: '1221',
      documentDateValidFrom: '2010-03-06T00:00',
      documentDateValidTo: '2013-03-06T00:00',
      documentPublisher: 'УЦ: Български въздухоплавателен център',
      organizationOtherDocumentType: nomenclatures.get('organizationOtherDocumentTypes', 'Protocol'),
      organizationOtherDocumentRole: nomenclatures.get('organizationOtherDocumentRoles', 'FlightTest'),
      valid: true,
      notes: '',
      bookPageNumber: '62',
      pageCount: 1
    },
    organization1Doc2: {
      documentNumber: '24',
      documentPersonNumber: '3223',
      documentDateValidFrom: '2010-09-17T00:00',
      documentDateValidTo: null,
      documentPublisher: 'Проверяващ: Кръстю Георгиев Нешев Код:11225',
      organizationOtherDocumentType: nomenclatures.get('organizationOtherDocumentTypes', 'Protocol'),
      organizationOtherDocumentRole: nomenclatures.get('organizationOtherDocumentRoles', 'FlightTest'),
      valid: true,
      notes: '',
      bookPageNumber: '63',
      pageCount: 1
    },
    organization1Doc3: {
      documentNumber: '24',
      documentPersonNumber: '4567',
      documentDateValidFrom: '2010-09-17T00:00',
      documentDateValidTo: null,
      documentPublisher: 'Проверяващ: Кръстю Георгиев Нешев Код:11225',
      organizationOtherDocumentType: nomenclatures.get('organizationOtherDocumentTypes', 'CtrlTalon'),
      organizationOtherDocumentRole: nomenclatures.get('organizationOtherDocumentRoles', 'FlightTest'),
      valid: true,
      notes: '',
      bookPageNumber: '63',
      pageCount: 1
    }
  };
})(typeof module === 'undefined' ? (this['organization-document-other.sample'] = {}) : module);
