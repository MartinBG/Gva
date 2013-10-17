(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    personEmployee1: {
      hiredate: '2013-09-20T00:00',
      valid: true,
      organizationId: nomenclatures.getId('Organizations', 'AAK Progres'),
      employmentCategoryId: nomenclatures.getId('EmploymentCategories', 'First officer'),
      countryId: nomenclatures.getId('countries', 'Bulgaria'),
      notes: "",
      
      bookPageNumber: "1",
      pageCount: 1
    }
  };
})(typeof module === 'undefined' ? (this['person-document-employee.sample'] = {}) : module);