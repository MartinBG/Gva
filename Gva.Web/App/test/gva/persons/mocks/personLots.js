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
      personMedicals = require('./person-document-med.sample');

  angular.module('app').constant('personLots', [
    {
      lotId: 1,
      nextIndex: 18,
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
          }]
        }
      ],
      personDocumentEducations: [
        {
          partIndex: 9,
          part: personDocumentEducations.person1Education
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
        }
      ],
      personDocumentOthers: [
        {
          partIndex: 11,
          part: personDocumentOthers.person1Doc1
        }
      ],
      personDocumentTrainings: [
        {
          partIndex: 12,
          part: personDocumentTrainings.person1Training1
        },
        {
          partIndex: 13,
          part: personDocumentTrainings.person1Training2
        },
        {
          partIndex: 14,
          part: personDocumentTrainings.person1Training3
        },
        {
          partIndex: 15,
          part: personDocumentTrainings.person1Training4
        }
      ],
      personMedicals: [
        {
          partIndex: 16,
          part: personMedicals.person1Medical1
        },
        {
          partIndex: 17,
          part: personMedicals.person1Medical2
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
          part: personDocumentEmployments.person2Employment
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
          part: personDocumentEmployments.person3Employment
        }
      ]
    }
  ]);
}(angular));
