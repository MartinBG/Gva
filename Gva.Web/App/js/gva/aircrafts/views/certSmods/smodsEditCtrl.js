/*global angular,_*/
(function (angular) {
  'use strict';

  function CertSmodsEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertSmod,
    aircraftCertSmod
  ) {
    var originalSmod = _.cloneDeep(aircraftCertSmod);

    $scope.isEdit = true;
    $scope.smod = aircraftCertSmod;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.smod = _.cloneDeep(originalSmod);
    };

    $scope.save = function () {
      return $scope.editCertSmodForm.$validate()
      .then(function () {
        if ($scope.editCertSmodForm.$valid) {
          return AircraftCertSmod
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.smod)
            .$promise
            .then(function () {
              return $state.go('root.aircrafts.view.smods.search');
            });
        }
      });
    };

    $scope.deleteSmod = function () {
      return AircraftCertSmod.remove({ id: $stateParams.id, ind: aircraftCertSmod.partIndex })
        .$promise.then(function () {
          return $state.go('root.aircrafts.view.smods.search');
        });
    };
  }

  CertSmodsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertSmod',
    'aircraftCertSmod'
  ];

  CertSmodsEditCtrl.$resolve = {
    aircraftCertSmod: [
      '$stateParams',
      'AircraftCertSmod',
      function ($stateParams, AircraftCertSmod) {
        return AircraftCertSmod.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('CertSmodsEditCtrl', CertSmodsEditCtrl);
}(angular));