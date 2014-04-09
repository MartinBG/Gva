/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportOpersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportCertOperational,
    airportCertOper
  ) {
    var originalCert = _.cloneDeep(airportCertOper);

    $scope.airportCertOper = airportCertOper;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editDocumentOperForm.$validate()
        .then(function () {
          if ($scope.editDocumentOperForm.$valid) {
            return AirportCertOperational
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.airportCertOper)
              .$promise
              .then(function () {
                return $state.go('root.airports.view.opers.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.airportCertOper.part = _.cloneDeep(originalCert.part);
    };
    
    $scope.deleteOper = function () {
      return AirportCertOperational.remove({
        id: $stateParams.id,
        ind: airportCertOper.partIndex
      }).$promise.then(function () {
        return $state.go('root.airports.view.opers.search');
      });
    };
  }

  AirportOpersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportCertOperational',
    'airportCertOper'
  ];

  AirportOpersEditCtrl.$resolve = {
    airportCertOper: [
      '$stateParams',
      'AirportCertOperational',
      function ($stateParams, AirportCertOperational) {
        return AirportCertOperational.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOpersEditCtrl', AirportOpersEditCtrl);
}(angular, _));