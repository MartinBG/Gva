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

    $scope.editCertPermit = function (permit) {
      return $state.go('root.aircrafts.view.permits.edit', {
        id: $stateParams.id,
        ind: permit.partIndex
      });
    };

    $scope.newCertPermit = function () {
      return $state.go('root.aircrafts.view.permits.new');
    };
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
