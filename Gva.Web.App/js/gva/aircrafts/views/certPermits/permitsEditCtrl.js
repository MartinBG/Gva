/*global angular,_*/
(function (angular) {
  'use strict';

  function CertPermitsToFlyEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertPermitToFly,
    aircraftCertPermitToFly
  ) {
    var originalPermit = _.cloneDeep(aircraftCertPermitToFly);

    $scope.isEdit = true;
    $scope.permit = aircraftCertPermitToFly;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.permit = _.cloneDeep(originalPermit);
    };

    $scope.save = function () {
      return $scope.editCertPermitForm.$validate()
      .then(function () {
        if ($scope.editCertPermitForm.$valid) {
          return AircraftCertPermitToFly
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.permit)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.permits.search');
            });
        }
      });
    };

    $scope.deletePermit = function () {
      return AircraftCertPermitToFly.remove({
        id: $stateParams.id,
        ind: aircraftCertPermitToFly.partIndex
      }).$promise.then(function () {
          return $state.go('root.aircrafts.view.permits.search');
        });
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