/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Employee: {
      hiredate: '2013-09-20T00:00',
      valid: nomenclatures.get('boolean', 'true'),
      organization: nomenclatures.get('organizations', 'AAK Progres'),
      employmentCategory: nomenclatures.get('employmentCategories', 'First officer'),
      country: nomenclatures.get('countries', 'Bulgaria'),
      notes: '',
      bookPageNumber: '1',
      pageCount: 1
    }
  };
})(typeof module === 'undefined' ? (this['person-document-employment.sample'] = {}) : module);
