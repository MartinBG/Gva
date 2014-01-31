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
      aircraftTypeGroup:'',
      ratingCategory: nomenclatures.get('ratingCategories', 'A1')
    }
  };
})(typeof module === 'undefined' ? (this['person-rating.sample'] = {}) : module);
