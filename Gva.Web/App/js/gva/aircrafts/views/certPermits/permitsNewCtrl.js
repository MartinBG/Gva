/*global angular*/
(function (angular) {
  'use strict';

  function CertPermitsToFlyNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertPermitToFly,
    aircraftCertPermitToFly
  ) {
    $scope.isEdit = false;

    $scope.permit = aircraftCertPermitToFly;

    $scope.save = function () {
      $scope.aircraftCertPermitForm.$validate()
         .then(function () {
            if ($scope.aircraftCertPermitForm.$valid) {
              return AircraftCertPermitToFly
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
    'AircraftCertPermitToFly',
    'aircraftCertPermitToFly'
  ];
  CertPermitsToFlyNewCtrl.$resolve = {
    aircraftCertPermitToFly: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('CertPermitsToFlyNewCtrl', CertPermitsToFlyNewCtrl);
}(angular));
