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
      return $scope.newCertPermitForm.$validate()
         .then(function () {
            if ($scope.newCertPermitForm.$valid) {
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
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('CertPermitsToFlyNewCtrl', CertPermitsToFlyNewCtrl);
}(angular));
