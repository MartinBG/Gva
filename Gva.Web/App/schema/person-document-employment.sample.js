﻿/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1Employee: {
      hiredate: '2013-09-20T00:00',
      valid: true,
      organizationId: nomenclatures.getId('organizations', 'AAK Progres'),
      employmentCategoryId: nomenclatures.getId('employmentCategories', 'First officer'),
      countryId: nomenclatures.getId('countries', 'Bulgaria'),
      notes: '',
      
      bookPageNumber: '1',
      pageCount: 1
    }
  };
})(typeof module === 'undefined' ? (this['person-document-employment.sample'] = {}) : module);