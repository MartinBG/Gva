/*global angular*/
(function (angular) {
  'use strict';

  function AirportOpersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    certOpers
  ) {
    $scope.certOpers = certOpers;
  }

  AirportOpersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'certOpers'
  ];

  AirportOpersSearchCtrl.$resolve = {
    certOpers: [
      '$stateParams',
      'AirportCertOperationals',
      function ($stateParams, AirportCertOperationals) {
        return AirportCertOperationals.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOpersSearchCtrl', AirportOpersSearchCtrl);
}(angular));
