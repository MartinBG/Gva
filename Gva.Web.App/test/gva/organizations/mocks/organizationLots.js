/*global angular, require*/
(function (angular) {
  'use strict';

  var organizationData = require('./organization-data.sample'),
    organizationAddresses = require('./organization-address.sample'),
    certAirportOperators = require('./organization-cert-airportoperator.sample'),
    staffManagement = require('./organization-staff-managment.sample'),
    organizationOtherDocuments = require('./organization-document-other.sample'),
    certGroundServiceOperators = require('./organization-cert-groundserviceoperator.sample'),
    certGroundServiceOperatorsSnoOperational =
    require('./organization-cert-groundserviceoperatorssnooperational.sample'),
    organizationInspections = require('./organization-inspections.sample'),
    organizationApprovals = require('./organization-approval.sample'),
    organizationAmendments = require('./organization-amendment.sample'),
    staffExaminers = require('./organization-staff-examiner.sample'),
    recommendations = require('./organization-recommendation.sample');

  angular.module('app').constant('organizationLots', [
    {
      lotId: 1,
      nextIndex: 28,
      organizationData: {
        partIndex: 1,
        part: organizationData.organization1Data
      },
      organizationAddresses: [
        {
          partIndex: 2,
          part: organizationAddresses.organization1Address1
        },
        {
          partIndex: 3,
          part: organizationAddresses.organization1Address2
        }
      ],
      certAirportOperators: [
        {
          partIndex: 4,
          part: certAirportOperators.organization1CertAirportOperator1
        },
        {
          partIndex: 5,
          part: certAirportOperators.organization1CertAirportOperator2
        }
      ],
      staffManagement: [
        {
          partIndex: 8,
          part: staffManagement.staffManagement1,
          files: []
        },
        {
          partIndex: 9,
          part: staffManagement.staffManagement2,
          files: []
        }
      ],
      organizationDocumentOthers: [
        {
          partIndex: 10,
          part: organizationOtherDocuments.organization1Doc1,
          files: []
        },
        {
          partIndex: 11,
          part: organizationOtherDocuments.organization1Doc2,
          files: []
        },
        {
          partIndex: 12,
          part: organizationOtherDocuments.organization1Doc3,
          files: []
        }
      ],
      certGroundServiceOperators: [
        {
          partIndex: 13,
          part: certGroundServiceOperators.organization1CertCertGroundServiceOperator1
        },
        {
          partIndex: 14,
          part: certGroundServiceOperators.organization1CertCertGroundServiceOperator2
        }
      ],
      certGroundServiceOperatorsSnoOperational: [
        {
          partIndex: 13,
          part: certGroundServiceOperatorsSnoOperational.certGroundDerviceOperatorSnoOperational1
        },
        {
          partIndex: 14,
          part: certGroundServiceOperatorsSnoOperational.certGroundDerviceOperatorSnoOperational2
        }
      ],
      organizationInspections: [
        {
          partIndex: 15,
          part: organizationInspections.organization1Inspection1,
          files: []
        },
        {
          partIndex: 16,
          part: organizationInspections.organization1Inspection2,
          files: []
        }
      ],
      organizationApprovals: [
        {
          partIndex: 17,
          part: organizationApprovals.organization1Approval1,
          amendments: [
            {
              partIndex: 18,
              part: organizationAmendments.organization1Amendment1,
              files: []
            },
            {
              partIndex: 19,
              part: organizationAmendments.organization1Amendment2,
              files: []
            }
          ]
        }
      ],
      staffExaminers: [
        {
          partIndex: 20,
          part: staffExaminers.staffExaminer1,
          files: []
        },
        {
          partIndex: 21,
          part: staffExaminers.staffExaminer2,
          files: []
        }
      ],
      recommendations: [
        {
          partIndex: 22,
          part: recommendations.recommendation1,
          files: []
        },
        {
          partIndex: 23,
          part: recommendations.recommendation2,
          files: []
        }
      ],
      organizationDocumentApplications: []
    }
  ]);
}(angular));