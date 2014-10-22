/*global angular*/
(function (angular) {
  'use strict';

  function CertPermitsToFlyNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertPermitsToFly,
    aircraftCertPermitToFly
  ) {

    $scope.lotId = $stateParams.id;
    $scope.permit = aircraftCertPermitToFly;

    $scope.save = function () {
      return $scope.newCertPermitForm.$validate()
         .then(function () {
            if ($scope.newCertPermitForm.$valid) {
              return AircraftCertPermitsToFly
              .save({ id: $stateParams.id }, $scope.permit).$promise
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

  CertPermitsToFlyNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertPermitsToFly',
    'aircraftCertPermitToFly'
  ];
  CertPermitsToFlyNewCtrl.$resolve = {
    aircraftCertPermitToFly: [
      '$stateParams',
      'AircraftCertPermitsToFly',
      function ($stateParams, AircraftCertPermitsToFly) {
        return AircraftCertPermitsToFly.newCertPermitToFly({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertPermitsToFlyNewCtrl', CertPermitsToFlyNewCtrl);
}(angular));
