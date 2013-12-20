/*global angular, require*/
(function (angular) {
  'use strict';

  var personData = require('./person-data.sample'),
      personAdresses = require('./person-address.sample'),
      personStatuses = require('./person-status.sample'),
      personDocumentEmployments = require('./person-document-employment.sample'),
      personDocumentEducations = require('./person-document-education.sample'),
      personDocumentIds = require('./person-document-id.sample'),
      personDocumentOthers = require('./person-document-other.sample'),
      personDocumentTrainings = require('./person-document-training.sample');

  angular.module('app').constant('personLots', [{
    lotId: 1,
    personData: {
      partIndex: 1,
      part: personData.person1Data
    },
    personAdresses: [
      {
        partIndex: 2,
        part: personAdresses.person1Address1
      },
      {
        partIndex: 3,
        part: personAdresses.person1Address2
      }
    ],
    personStatuses: [
      {
        partIndex: 4,
        part: personStatuses.person1Status
      }
    ],
    personDocumentEmployments: [
      {
        partIndex: 5,
        part: personDocumentEmployments.person1Employee
      }
    ],
    personDocumentEducations: [
      {
        partIndex: 6,
        part: personDocumentEducations.person1Education
      }
    ],
    personDocumentIds: [
      {
        partIndex: 7,
        part: personDocumentIds.person1Id
      }
    ],
    personDocumentOthers: [
      {
        partIndex: 8,
        part: personDocumentOthers.person1Doc1
      }
    ],
    personDocumentTrainings: [
      {
        partIndex: 9,
        part: personDocumentTrainings.person1Training1
      },
      {
        partIndex: 10,
        part: personDocumentTrainings.person1Training2
      },
      {
        partIndex: 11,
        part: personDocumentTrainings.person1Training3
      },
      {
        partIndex: 12,
        part: personDocumentTrainings.person1Training4
      }
    ]
  }]);
}(angular));
