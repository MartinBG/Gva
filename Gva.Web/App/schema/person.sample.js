/*global module, require*/
(function (module) {
  'use strict';

  var personData = require('./person-data.sample'),
      personAdresses = require('./person-address.sample'),
      personStatuses = require('./person-status.sample'),
      personEmployments = require('./person-document-employment.sample'),
      personEducations = require('./person-document-education.sample'),
      personIds = require('./person-document-id.sample');

  module.exports = [{
    personData: personData.person1Data,
    personAdresses: [personAdresses.person1Address1, personAdresses.person1Address2],
    personStatus: [personStatuses.person1Status],
    personEmployments: [personEmployments.person1Employment],
    personEducations: [personEducations.person1Education],
    personIds: [personIds.person1Id]
  }];
})(typeof module === 'undefined' ? (this['persons.sample'] = {}) : module);
