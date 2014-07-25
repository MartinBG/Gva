/*global angular,_*/
(function (angular) {
  'use strict';

  function CertPermitsToFlyEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertPermitsToFly,
    aircraftCertPermitToFly,
    scMessage
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
          return AircraftCertPermitsToFly
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.permit)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.permits.search');
            });
        }
      });
    };

    $scope.deletePermit = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftCertPermitsToFly.remove({
            id: $stateParams.id,
            ind: aircraftCertPermitToFly.partIndex
          }).$promise.then(function () {
              return $state.go('root.aircrafts.view.permits.search');
          });
        }
      });
    };
  }

  CertPermitsToFlyEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertPermitsToFly',
    'aircraftCertPermitToFly',
    'scMessage'
  ];

  CertPermitsToFlyEditCtrl.$resolve = {
    aircraftCertPermitToFly: [
      '$stateParams',
      'AircraftCertPermitsToFly',
      function ($stateParams, AircraftCertPermitsToFly) {
        return AircraftCertPermitsToFly.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertPermitsToFlyEditCtrl', CertPermitsToFlyEditCtrl);
}(angular));
