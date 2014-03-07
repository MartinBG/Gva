/*global angular, require*/
(function (angular) {
  'use strict';

  var aircraftData = require('./aircraft-data.sample'),
      aircraftCertRegistrations = require('./aircraft-cert-registrations.sample'),
      aircraftCertSmods = require('./aircraft-cert-smods.sample'),
      aircraftCertMarks = require('./aircraft-cert-marks.sample'),
      aircraftCertAirworthinesses = require('./aircraft-cert-airworthinesses.sample'),
      aircraftCertNoises = require('./aircraft-cert-noises.sample'),
      aircraftCertPermitsToFly = require('./aircraft-cert-permitstofly.sample'),
      aircraftCertRadios = require('./aircraft-cert-radios.sample'),
      aircraftDocumentDebts = require('./aircraft-document-debts.sample'),
      aircraftDocumentDebtsFM = require('./aircraft-document-debtsfm.sample'),
      aircraftDocumentOthers = require('./aircraft-document-other.sample'),
      aircraftInspections = require('./aircraft-inspections.sample'),
      aircraftDocumentOccurrences = require('./aircraft-document-occurrences.sample'),
      aircraftMaintenances = require('./aircraft-maintenance.sample');

  angular.module('app').constant('aircraftLots', [
    {
      lotId: 1,
      nextIndex: 24,
      aircraftData: {
        partIndex: 1,
        part: aircraftData.aircraft1Data
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
      aircraftCertNoises: [
        {
          partIndex: 11,
          part: aircraftCertNoises.aircraft1Noise1
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
      ]
    },
    {
      lotId: 2,
      nextIndex: 2,
      aircraftData: {
        partIndex: 1,
        part: aircraftData.aircraft2Data
      }
    }
  ]);
}(angular));
