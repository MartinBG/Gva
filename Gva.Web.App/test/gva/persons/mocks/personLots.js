/*global angular, require*/
(function (angular) {
  'use strict';

  var personData = require('./person-data.sample'),
      personAddresses = require('./person-address.sample'),
      personStatuses = require('./person-status.sample'),
      personDocumentEmployments = require('./person-document-employment.sample'),
      personDocumentEducations = require('./person-document-education.sample'),
      personDocumentIds = require('./person-document-id.sample'),
      personDocumentOthers = require('./person-document-other.sample'),
      personDocumentTrainings = require('./person-document-training.sample'),
      personDocumentMedicals = require('./person-document-med.sample'),
      personDocumentChecks = require('./person-document-checks.sample'),
      personFlyingExperiences = require('./person-flyingExperience.sample'),
      personRatings = require('./person-rating.sample'),
      personDocumentApplications = require('./person-document-application.sample');

  angular.module('app').constant('personLots', [
    {
      lotId: 1,
      nextIndex: 31,
      personData: {
        partIndex: 1,
        part: personData.person1Data
      },
      personAddresses: [
        {
          partIndex: 2,
          part: personAddresses.person1Address1
        },
        {
          partIndex: 3,
          part: personAddresses.person1Address2
        }
      ],
      personStatuses: [
        {
          partIndex: 4,
          part: personStatuses.person1Status1
        },
        {
          partIndex: 5,
          part: personStatuses.person1Status2
        },
        {
          partIndex: 6,
          part: personStatuses.person1Status3
        },
        {
          partIndex: 7,
          part: personStatuses.person1Status4
        }
      ],
      personDocumentEmployments: [
        {
          partIndex: 8,
          part: personDocumentEmployments.person1Employment,
          files: []
        }
      ],
      personDocumentEducations: [
        {
          partIndex: 9,
          part: personDocumentEducations.person1Education,
          files: []
        }
      ],
      personDocumentIds: [
        {
          partIndex: 10,
          part: personDocumentIds.person1Id,
          files: []
        },
        {
          partIndex: 11,
          part: personDocumentIds.person1Id,
          files: []
        },
        {
          partIndex: 12,
          part: personDocumentIds.person1Id,
          files: []
        }
      ],
      personDocumentOthers: [
        {
          partIndex: 13,
          part: personDocumentOthers.person1Doc1,
          files: []
        }
      ],
      personDocumentTrainings: [
        {
          partIndex: 14,
          part: personDocumentTrainings.person1Training1,
          files: []
        },
        {
          partIndex: 15,
          part: personDocumentTrainings.person1Training2,
          files: []
        },
        {
          partIndex: 16,
          part: personDocumentTrainings.person1Training3,
          files: []
        },
        {
          partIndex: 17,
          part: personDocumentTrainings.person1Training4,
          files: []
        }
      ],
      personDocumentMedicals: [
        {
          partIndex: 18,
          part: personDocumentMedicals.person1Medical1,
          files: []
        },
        {
          partIndex: 19,
          part: personDocumentMedicals.person1Medical2,
          files: []
        }
      ],
      personDocumentChecks: [
        {
          partIndex: 20,
          part: personDocumentChecks.person1Check1,
          files: []
        },
        {
          partIndex: 21,
          part: personDocumentChecks.person1Check2,
          files: []
        }
      ],
      personFlyingExperiences: [
        {
          partIndex: 22,
          part: personFlyingExperiences.person1FlyingExperience1
        },
        {
          partIndex: 23,
          part: personFlyingExperiences.person1FlyingExperience2
        }
      ],
      personRatings: [
        {
          partIndex: 24,
          part: personRatings.person1Rating1
        },
        {
          partIndex: 25,
          part: personRatings.person1Rating2
        },
        {
          partIndex: 26,
          part: personRatings.person1Rating3
        }
      ],
      personLicences: [],
      personDocumentApplications: [
        {
          partIndex: 29,
          part: personDocumentApplications.application1
        },
        {
          partIndex: 30,
          part: personDocumentApplications.application2
        }
      ]
    },
    {
      lotId: 2,
      nextIndex: 3,
      personData: {
        partIndex: 1,
        part: personData.person2Data
      },
      personDocumentEmployments: [
        {
          partIndex: 2,
          part: personDocumentEmployments.person2Employment,
          files: []
        }
      ],
      personAddresses: [],
      personStatuses: [],
      personDocumentEducations: [],
      personDocumentIds: [],
      personDocumentOthers: [],
      personDocumentTrainings: [],
      personDocumentMedicals: [],
      personDocumentChecks: [],
      personFlyingExperiences: [],
      personRatings: [],
      personLicences: []
    },
    {
      lotId: 3,
      nextIndex: 3,
      personData: {
        partIndex: 1,
        part: personData.person3Data
      },
      personDocumentEmployments: [
        {
          partIndex: 2,
          part: personDocumentEmployments.person3Employment,
          files: []
        }
      ],
      personAddresses: [],
      personStatuses: [],
      personDocumentEducations: [],
      personDocumentIds: [],
      personDocumentOthers: [],
      personDocumentTrainings: [],
      personDocumentMedicals: [],
      personDocumentChecks: [],
      personFlyingExperiences: [],
      personRatings: [],
      personLicences: []
    }
  ]);
}(angular));
