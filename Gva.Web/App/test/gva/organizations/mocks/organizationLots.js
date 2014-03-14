/*global angular, require*/
(function (angular) {
  'use strict';

  var organizationData = require('./organization-data.sample');

  angular.module('app').constant('organizationLots', [
    {
      lotId: 1,
      nextIndex: 2,
      organizationData: {
        partIndex: 1,
        part: organizationData.organization1Data
      }
    }
  ]);
}(angular));