/*global angular*/
(function (angular) {
  'use strict';

  function CertPermitsToFlyEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertPermitToFly,
    aircraftCertPermitToFly
  ) {
    $scope.isEdit = true;

    $scope.permit = aircraftCertPermitToFly;

    $scope.save = function () {
      $scope.aircraftCertPermitForm.$validate()
      .then(function () {
        if ($scope.aircraftCertPermitForm.$valid) {
          return AircraftCertPermitToFly
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.permit)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.permits.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.permits.search');
    };
  }

  CertPermitsToFlyEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertPermitToFly',
    'aircraftCertPermitToFly'
  ];

  CertPermitsToFlyEditCtrl.$resolve = {
    aircraftCertPermitToFly: [
      '$stateParams',
      'AircraftCertPermitToFly',
      function ($stateParams, AircraftCertPermitToFly) {
        return AircraftCertPermitToFly.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertPermitsToFlyEditCtrl', CertPermitsToFlyEditCtrl);
}(angular));