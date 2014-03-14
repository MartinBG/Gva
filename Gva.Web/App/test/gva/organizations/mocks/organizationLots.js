/*global angular, require*/
(function (angular) {
  'use strict';

  var organizationData = require('./organization-data.sample'),
    organizationAddresses = require('./organization-address.sample');

  angular.module('app').constant('organizationLots', [
    {
      lotId: 1,
      nextIndex: 4,
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
      ]
    }
  ]);
}(angular));