/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Employment: {
      hiredate: '2013-09-20T00:00',
      valid: nomenclatures.get('boolean', 'true'),
      organization: nomenclatures.get('organizations', 'AAK Progres'),
      employmentCategory: nomenclatures.get('employmentCategories', '35'),
      country: nomenclatures.get('countries', 'KWI'),
      notes: '',
      bookPageNumber: '1',
      pageCount: 1
    },
    person2Employment: {
      hiredate: '2010-04-21T00:00',
      valid: nomenclatures.get('boolean', 'true'),
      organization: nomenclatures.get('organizations', 'Wizz Air'),
      employmentCategory: nomenclatures.get('employmentCategories', '35'),
      country: nomenclatures.get('countries', 'KWI'),
      notes: '',
      bookPageNumber: '1',
      pageCount: 1
    },
    person3Employment: {
      hiredate: '2011-05-15T00:00',
      valid: nomenclatures.get('boolean', 'true'),
      organization: nomenclatures.get('organizations', 'Fly Emirates'),
      employmentCategory: nomenclatures.get('employmentCategories', '35'),
      country: nomenclatures.get('countries', 'KWI'),
      notes: '',
      bookPageNumber: '1',
      pageCount: 1
    }
  };
})(typeof module === 'undefined' ? (this['person-document-employment.sample'] = {}) : module);
