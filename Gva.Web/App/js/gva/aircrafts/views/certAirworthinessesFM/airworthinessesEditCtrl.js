/*global angular,_*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessFM,
    aircraftCertAirworthiness
  ) {
    var originalAirworthiness = _.cloneDeep(aircraftCertAirworthiness);

    $scope.isEdit = true;
    $scope.aw = aircraftCertAirworthiness;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aw = _.cloneDeep(originalAirworthiness);
    };

    $scope.save = function () {
      return $scope.editCertAirworthinessForm.$validate()
      .then(function () {
        if ($scope.editCertAirworthinessForm.$valid) {
          return AircraftCertAirworthinessFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aw)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.airworthinessesFM.search');
            });
        }
      });
    };

    $scope.deleteAirworthiness = function () {
      return AircraftCertAirworthinessFM.remove({
        id: $stateParams.id,
        ind: aircraftCertAirworthiness.partIndex
      }).$promise.then(function () {
          return $state.go('root.aircrafts.view.airworthinessesFM.search');
        });
    };
  }

  CertAirworthinessesFMEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessFM',
    'aircraftCertAirworthiness'
  ];

  CertAirworthinessesFMEditCtrl.$resolve = {
    aircraftCertAirworthiness: [
      '$stateParams',
      'AircraftCertAirworthinessFM',
      function ($stateParams, AircraftCertAirworthinessFM) {
        return AircraftCertAirworthinessFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertAirworthinessesFMEditCtrl', CertAirworthinessesFMEditCtrl);
}(angular));