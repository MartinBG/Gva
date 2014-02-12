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
      personRatingEditions = require('./person-rating-edition.sample'),
      personRatings = require('./person-rating.sample');

  angular.module('app').constant('personLots', [
    {
      lotId: 1,
      nextIndex: 27,
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
          file: [{
            key: '3a-1-ab',
            name: 'test.pdf',
            relativePath: ''
          }],
          applications: []
        }
      ],
      personDocumentEducations: [
        {
          partIndex: 9,
          part: personDocumentEducations.person1Education,
          file: [{
            key: '342-42-ab',
            name: 'testName.pdf',
            relativePath: '../folder1/folder2/'
          }],
          applications: []
        }
      ],
      personDocumentIds: [
        {
          partIndex: 10,
          part: personDocumentIds.person1Id,
          file: [{
            key: '342-43-ab',
            name: 'testName.pdf',
            relativePath: '../folder1/folder2/'
          }],
          applications: [
            {
              applicationId: 1,
              applicationName: 'application1'
            },
            {
              applicationId: 2,
              applicationName: 'application2'
            }
          ]
        },
        {
          partIndex: 11,
          part: personDocumentIds.person1Id,
          file: [],
          applications: [
            {
              applicationId: 1,
              applicationName: 'Издаване на свидетелство за правоспособност на авиационен персонал'
            }
          ]
        },
        {
          partIndex: 12,
          part: personDocumentIds.person1Id,
          file: [],
          applications: [
            {
              applicationId: 1,
              applicationName: 'Издаване на свидетелство за правоспособност на авиационен персонал'
            }
          ]
        }
      ],
      personDocumentOthers: [
        {
          partIndex: 13,
          part: personDocumentOthers.person1Doc1,
          applications: []
        }
      ],
      personDocumentTrainings: [
        {
          partIndex: 14,
          part: personDocumentTrainings.person1Training1,
          applications: [],
          file: [{
            key: '311-93-wb',
            name: 'test1.pdf',
            relativePath: '../folder1/folder3/'
          }]
        },
        {
          partIndex: 15,
          part: personDocumentTrainings.person1Training2,
          applications: []
        },
        {
          partIndex: 16,
          part: personDocumentTrainings.person1Training3,
          applications: [],
          file: [{
            key: '311-93-wb',
            name: 'test3.pdf',
            relativePath: '../folder1/folder3/'
          }]
        },
        {
          partIndex: 17,
          part: personDocumentTrainings.person1Training4,
          applications: []
        }
      ],
      personDocumentMedicals: [
        {
          partIndex: 18,
          part: personDocumentMedicals.person1Medical1,
          applications: []
        },
        {
          partIndex: 19,
          part: personDocumentMedicals.person1Medical2,
          applications: []
        }
      ],
      personDocumentChecks: [
        {
          partIndex: 20,
          part: personDocumentChecks.person1Check1,
          applications: []
        },
        {
          partIndex: 21,
          part: personDocumentChecks.person1Check2,
          applications: []
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
          part: personRatings.person1Rating1,
          personRatingEditions: [
            {
              partIndex: 25,
              part: personRatingEditions.person1Edition1
            },
            {
              partIndex: 26,
              part: personRatingEditions.person1Edition2
            }
          ]
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
          applications: []
        }
      ]
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
          applications: []
        }
      ]
    }
  ]);
}(angular));
