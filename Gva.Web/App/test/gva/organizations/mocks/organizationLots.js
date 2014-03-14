/*global angular, require*/
(function (angular) {
  'use strict';

  var organizationData = require('./organization-data.sample'),
    organizationAddresses = require('./organization-address.sample'),
    certAirportOperators = require('./organization-cert-airportoperator.sample'),
    organizationAuditplans = require('./organization-auditplan.sample'),
    staffManagement = require('./organization-staff-managment.sample');

  angular.module('app').constant('organizationLots', [
    {
      lotId: 1,
      nextIndex: 10,
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
      organizationAuditplans: [
        {
          partIndex: 6,
          part: organizationAuditplans.organization1Auditplan1
        },
        {
          partIndex: 7,
          part: organizationAuditplans.organization1Auditplan2
        }
      ],
      staffManagement: [
        {
          partIndex: 8,
          part: staffManagement.staffManagement1
        },
        {
          partIndex: 9,
          part: staffManagement.staffManagement2
        }
      ]
    }
  ]);
}(angular));