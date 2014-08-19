/*global angular*/
(function (angular) {
  'use strict';

  function CertPermitsToFlySearchCtrl(
    $scope,
    $state,
    $stateParams,
    permits
  ) {
    $scope.permits = permits;
  }

  CertPermitsToFlySearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'permits'
  ];

  CertPermitsToFlySearchCtrl.$resolve = {
    permits: [
      '$stateParams',
      'AircraftCertPermitsToFly',
      function ($stateParams, AircraftCertPermitsToFly) {
        return AircraftCertPermitsToFly.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertPermitsToFlySearchCtrl', CertPermitsToFlySearchCtrl);
}(angular));
