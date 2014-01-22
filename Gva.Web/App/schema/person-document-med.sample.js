/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Medical1: {
      documentNumberPrefix: 'MED BG',
      documentNumber: '1',
      documentNumberSuffix: '99994',
      documentDateValidFrom: '2010-04-04T00:00',
      documentDateValidTo: '2010-08-04T00:00',
      medClassType: nomenclatures.get('medicalClassTypes', 'class1'),
      documentPublisher: 'AMC Latvia',
      limitationsTypes: [
        nomenclatures.get('medicalLimitationTypes', 'VNL'),
        nomenclatures.get('medicalLimitationTypes', 'OML')
      ],
      notes: 'Test notes',
      bookPageNumber: '1',
      pageCount: 3
    },
    person1Medical2: {
      documentNumberPrefix: 'MED BG2',
      documentNumber: '3244',
      documentNumberSuffix: '9934',
      documentDateValidFrom: '2005-04-04T00:00',
      documentDateValidTo: '2015-09-06T00:00',
      medClassType: nomenclatures.get('medicalClassTypes', 'class2'),
      documentPublisher: 'Caa France',
      limitationsTypes: [
        nomenclatures.get('medicalLimitationTypes', 'MCL'),
        nomenclatures.get('medicalLimitationTypes', 'VDL'),
        nomenclatures.get('medicalLimitationTypes', 'VML')
      ],
      notes: 'Test notes doc2',
      bookPageNumber: '3',
      pageCount: '5'
    }
  };
})(typeof module === 'undefined' ? (this['person-document-med.sample'] = {}) : module);
