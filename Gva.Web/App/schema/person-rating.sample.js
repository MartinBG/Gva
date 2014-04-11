/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Rating1: {
      staffType: nomenclatures.get('staffTypes', 'F'),
      personRatingLevel: nomenclatures.get('personRatingLevels', 'A'),
      ratingClass:  nomenclatures.get('ratingClasses', 'VLA'),
      ratingType: nomenclatures.get('ratingTypes', 'BAe 146'),
      authorization: nomenclatures.get('authorizations', 'FI(A)'),
      personRatingModel: nomenclatures.get('personRatingModels', 'temporary'),
      locationIndicator: nomenclatures.get('locationIndicators', 'LBBG'),
      sector: 'sektor',
      aircraftTypeGroup: nomenclatures.get('aircraftTypeGroups', 'Airbus A319/A320/A321 (PW JT8D)'),
      ratingCategory: nomenclatures.get('ratingCategories', 'A1'),
      editions: [
        {
          documentDateValidFrom: '1912-10-07T00:00',
          documentDateValidTo: '1912-12-24T00:00',
          notes: 'Бележки 1',
          notesAlt: 'some notes 1',
          inspector: nomenclatures.get('inspectors', 'Vladimir'),
          ratingSubClasses: [nomenclatures.get('ratingSubClasses', 'A1'), nomenclatures.get('ratingSubClasses', 'A2')],
          limitations: [nomenclatures.get('ratingLimitationTypes', 'MCL'), nomenclatures.get('ratingLimitationTypes', 'OCL')]
        },
        {
          documentDateValidFrom: '1812-04-04T00:00',
          documentDateValidTo: '1812-05-04T00:00',
          notes: 'Бележки 2',
          notesAlt: 'some notes 2',
          inspector: nomenclatures.get('inspectors', 'Vanq'),
          ratingSubClasses: [nomenclatures.get('ratingSubClasses', 'A2'), nomenclatures.get('ratingSubClasses', 'A3')],
          limitations: [nomenclatures.get('ratingLimitationTypes', 'MCL'), nomenclatures.get('ratingLimitationTypes', 'OCL')]
        }
      ]
    },
    person1Rating2: {
      staffType: nomenclatures.get('staffTypes', 'F'),
      personRatingLevel: nomenclatures.get('personRatingLevels', 'A'),
      ratingClass:  nomenclatures.get('ratingClasses', 'VLA'),
      ratingType: nomenclatures.get('ratingTypes', 'Тu 154'),
      authorization: nomenclatures.get('authorizations', 'FI(A)'),
      personRatingModel: nomenclatures.get('personRatingModels', 'temporary'),
      locationIndicator: nomenclatures.get('locationIndicators', 'LBBG'),
      sector: 'sektor',
      aircraftTypeGroup: nomenclatures.get('aircraftTypeGroups', 'Airbus A319/A320/A321 (PW JT8D)'),
      ratingCategory: nomenclatures.get('ratingCategories', 'A1'),
      editions: [
        {
          documentDateValidFrom: '1920-10-07T00:00',
          documentDateValidTo: '1920-12-24T00:00',
          notes: 'Бележки 1 на човек 2',
          notesAlt: 'some notes 1 of person 2',
          inspector: nomenclatures.get('inspectors', 'Vanq'),
          ratingSubClasses: [nomenclatures.get('ratingSubClasses', 'A1'), nomenclatures.get('ratingSubClasses', 'A4')],
          limitations: [nomenclatures.get('ratingLimitationTypes', 'MCL'), nomenclatures.get('ratingLimitationTypes', 'OCL')]
        }
      ]
    },
    person1Rating3: {
      staffType: nomenclatures.get('staffTypes', 'F'),
      personRatingLevel: nomenclatures.get('personRatingLevels', 'A'),
      ratingClass:  nomenclatures.get('ratingClasses', 'VLA'),
      ratingType: nomenclatures.get('ratingTypes', 'MD80'),
      authorization: nomenclatures.get('authorizations', 'FI(A)'),
      personRatingModel: nomenclatures.get('personRatingModels', 'temporary'),
      locationIndicator: nomenclatures.get('locationIndicators', 'LBBG'),
      sector: 'sektor',
      aircraftTypeGroup: nomenclatures.get('aircraftTypeGroups', 'Airbus A319/A320/A321 (PW JT8D)'),
      ratingCategory: nomenclatures.get('ratingCategories', 'A1'),
      editions: [
        {
          documentDateValidFrom: '2012-04-04T00:00',
          documentDateValidTo: '2012-05-04T00:00',
          notes: 'Бележки 2 на човек 2',
          notesAlt: 'some notes 2 of person 2',
          inspector: nomenclatures.get('inspectors', 'Georgi'),
          ratingSubClasses: [nomenclatures.get('ratingSubClasses', 'A3'), nomenclatures.get('ratingSubClasses', 'A1')],
          limitations: [nomenclatures.get('ratingLimitationTypes', 'MCL'), nomenclatures.get('ratingLimitationTypes', 'OCL')],
        }
      ]
    }
  };
})(typeof module === 'undefined' ? (this['person-rating.sample'] = {}) : module);
