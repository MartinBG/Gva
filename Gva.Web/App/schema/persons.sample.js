$$persons = [];

(function (module) {
  'use strict';

  var personData = require('./person-data.sample'),
      personAdresses = require('./person-address.sample'),
      personEmployees = require('./person-document-employee.sample');

  module.exports = [{
    personData: personData.personData1,
    personAdresses: [personAdresses.personAddress1, personAdresses.personAddress2],
    personEmployees: [personEmployees.personEmployee1]
  }];
})(typeof module === 'undefined' ? (this['persons.sample'] = {}) : module);