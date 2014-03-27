/*global angular, require*/
(function (angular) {
  'use strict';

  var aircraftData = require('./aircraft-data.sample'),
      aircraftDataApex = require('./aircraft-dataapex.sample'),
      aircraftCertRegistrations = require('./aircraft-cert-registrations.sample'),
      aircraftCertRegistrationsFM = require('./aircraft-cert-registrationsfm.sample'),
      aircraftCertSmods = require('./aircraft-cert-smods.sample'),
      aircraftCertMarks = require('./aircraft-cert-marks.sample'),
      aircraftCertAirworthinesses = require('./aircraft-cert-airworthinesses.sample'),
      aircraftCertAirworthinessesFM = require('./aircraft-cert-airworthinessesfm.sample'),
      aircraftCertNoises = require('./aircraft-cert-noises.sample'),
      aircraftCertNoisesFM = require('./aircraft-cert-noisesfm.sample'),
      aircraftCertPermitsToFly = require('./aircraft-cert-permitstofly.sample'),
      aircraftCertRadios = require('./aircraft-cert-radios.sample'),
      aircraftDocumentDebts = require('./aircraft-document-debts.sample'),
      aircraftDocumentDebtsFM = require('./aircraft-document-debtsfm.sample'),
      aircraftDocumentOthers = require('./aircraft-document-other.sample'),
      aircraftInspections = require('./aircraft-inspections.sample'),
      aircraftDocumentOccurrences = require('./aircraft-document-occurrences.sample'),
      aircraftMaintenances = require('./aircraft-maintenance.sample'),
      aircraftDocumentOwners = require('./aircraft-document-owner.sample'),
      aircraftParts = require('./aircraft-parts.sample'),
      aircraftDocumentApplications = require('./aircraft-document-application.sample');

  angular.module('app').constant('aircraftLots', [
    {
      lotId: 1,
      nextIndex: 38,
      aircraftData: {
        partIndex: 1,
        part: aircraftData.aircraft1Data
      },
      aircraftDataApex: {
        partIndex: 30,
        part: aircraftDataApex.aircraft1Data
      },
      aircraftCertRegistrations: [
        {
          partIndex: 2,
          part: aircraftCertRegistrations.aircraft1Reg1
        },
        {
          partIndex: 3,
          part: aircraftCertRegistrations.aircraft1Reg2
        },
        {
          partIndex: 4,
          part: aircraftCertRegistrations.aircraft1Reg3
        },
        {
          partIndex: 5,
          part: aircraftCertRegistrations.aircraft1Reg4
        }
      ],
      aircraftCertRegistrationsFM: [
        {
          partIndex: 32,
          part: aircraftCertRegistrationsFM.aircraft1Reg1
        },
        {
          partIndex: 33,
          part: aircraftCertRegistrationsFM.aircraft1Reg2
        },
        {
          partIndex: 34,
          part: aircraftCertRegistrationsFM.aircraft1Reg3
        },
        {
          partIndex: 35,
          part: aircraftCertRegistrationsFM.aircraft1Reg4
        }
      ],
      aircraftCertSmods: [
        {
          partIndex: 6,
          part: aircraftCertSmods.aircraft1Smod1
        },
        {
          partIndex: 7,
          part: aircraftCertSmods.aircraft1Smod2
        }
      ],
      aircraftCertMarks: [
        {
          partIndex: 8,
          part: aircraftCertMarks.aircraft1Mark1
        },
        {
          partIndex: 9,
          part: aircraftCertMarks.aircraft1Mark2
        }
      ],
      aircraftCertAirworthinesses: [
        {
          partIndex: 10,
          part: aircraftCertAirworthinesses.aircraft1Airworthiness1
        }
      ],
      aircraftCertAirworthinessesFM: [
        {
          partIndex: 31,
          part: aircraftCertAirworthinessesFM.aircraft1Airworthiness1
        }
      ],
      aircraftCertNoises: [
        {
          partIndex: 11,
          part: aircraftCertNoises.aircraft1Noise1
        }
      ],
      aircraftCertNoisesFM: [
        {
          partIndex: 25,
          part: aircraftCertNoisesFM.aircraft1Noise1
        }
      ],
      aircraftCertPermitsToFly: [
        {
          partIndex: 12,
          part: aircraftCertPermitsToFly.aircraft1PermitToFly1
        }
      ],
      aircraftCertRadios: [
        {
          partIndex: 13,
          part: aircraftCertRadios.aircraft1Radio1
        }
      ],
      aircraftDocumentDebts: [
        {
          partIndex: 14,
          part: aircraftDocumentDebts.aircraft1Debt1
        }
      ],
      aircraftDocumentDebtsFM: [
        {
          partIndex: 15,
          part: aircraftDocumentDebtsFM.aircraft1Debt1
        }
      ],
      aircraftDocumentOthers: [
        {
          partIndex: 16,
          part: aircraftDocumentOthers.aircraft1DocOther1
        },
        {
          partIndex: 17,
          part: aircraftDocumentOthers.aircraft1DocOther2
        }
      ],
      aircraftInspections: [
        {
          partIndex: 18,
          part: aircraftInspections.aircraft1Inspection1
        },
        {
          partIndex: 19,
          part: aircraftInspections.aircraft1Inspection2
        }
      ],
      aircraftDocumentOccurrences: [
        {
          partIndex: 20,
          part: aircraftDocumentOccurrences.aircraft1Occurrence1
        },
        {
          partIndex: 21,
          part: aircraftDocumentOccurrences.aircraft1Occurrence2
        }
      ],
      aircraftMaintenances: [
        {
          partIndex: 22,
          part: aircraftMaintenances.aircraftMaintenance1
        },
        {
          partIndex: 23,
          part: aircraftMaintenances.aircraftMaintenance2
        }
      ],
      aircraftDocumentOwners: [
        {
          partIndex: 26,
          part: aircraftDocumentOwners.aircraft1Owner1
        },
        {
          partIndex: 27,
          part: aircraftDocumentOwners.aircraft1Owner2
        }
      ],
      aircraftParts: [
        {
          partIndex: 28,
          part: aircraftParts.aircraft1Part1
        },
        {
          partIndex: 29,
          part: aircraftParts.aircraft1Part2
        }
      ],
      aircraftDocumentApplications: [
        {
          partIndex: 36,
          part: aircraftDocumentApplications.aircraft1Application1
        },
        {
          partIndex: 37,
          part: aircraftDocumentApplications.aircraft1Application2
        }
      ]
    },
    {
      lotId: 2,
      nextIndex: 3,
      aircraftData: {
        partIndex: 1,
        part: aircraftData.aircraft2Data
      },
      aircraftDataApex: {
        partIndex: 2,
        part: aircraftDataApex.aircraft2Data
      }
    }
  ]);
}(angular));
