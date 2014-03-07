/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1DocOther1: {
      documentNumber: '232d',
      documentDateValidFrom: '1981-04-04T00:00',
      documentDateValidTo: '1982-04-04T00:00',
      documentPublisher: 'МВР София',
      otherDocumentType: nomenclatures.get('otherDocumentTypes', 'CtrlTalon'),
      aircraftOtherDocumentRole: nomenclatures.get('documentRoles', 'Training'),
      valid: nomenclatures.get('boolean', 'false'),
      notes: 'Тест',
      bookPageNumber: '2',
      pageCount: '4'
    },
    aircraft1DocOther2: {
      documentNumber: '1',
      documentDateValidFrom: '2001-05-06T00:00',
      documentDateValidTo: '2015-07-09T00:00',
      documentPublisher: 'МВР София',
      otherDocumentType: nomenclatures.get('otherDocumentTypes', 'CtrlTalon'),
      aircraftOtherDocumentRole: nomenclatures.get('documentRoles', 'Training'),
      valid: nomenclatures.get('boolean', 'true'),
      notes: 'Тест2',
      bookPageNumber: '1',
      pageCount: '7'
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-document-other.sample'] = {}) : module);
