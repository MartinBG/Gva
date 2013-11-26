/*global module, require*/
(function (module) {
  'use strict';

  var personData = require('./person-data.sample'),
      personAdresses = require('./person-address.sample'),
      personStatuses = require('./person-status.sample'),
      personEmployments = require('./person-document-employment.sample'),
      personEducations = require('./person-document-education.sample'),
      personIds = require('./person-document-id.sample'),
      personOtherDocuments = require('./person-document-other.sample'),
      personTrainings = require('./person-document-training.sample');

  module.exports = [{
    personData: personData.person1Data,
    personAdresses: [personAdresses.person1Address1, personAdresses.person1Address2],
    personStatus: [personStatuses.person1Status],
    personEmployments: [personEmployments.person1Employment],
    personEducations: [personEducations.person1Education],
    personIds: [personIds.person1Id],
    personOtherDocuments: [personOtherDocuments.person1Doc1],
    personTrainings: [personTrainings.person1Training1, personTrainings.person1Training2, personTrainings.person1Training3, personTrainings.person1Training4]
  }];
})(typeof module === 'undefined' ? (this['person.sample'] = {}) : module);
