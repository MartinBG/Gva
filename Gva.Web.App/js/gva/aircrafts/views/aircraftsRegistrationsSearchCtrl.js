/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AircraftsRegistrationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    registrations) {

    $scope.filters = {
      certNumber: null,
      actNumber: null,
      regMark: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.registrations = registrations;

    $scope.search = function () {
      $state.go('root.aircrafts.registrations', {
        certNumber: $scope.filters.certNumber,
        actNumber: $scope.filters.actNumber,
        regMark: $scope.filters.regMark
      });
    };
  }

  AircraftsRegistrationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'registrations'
  ];

  AircraftsRegistrationsSearchCtrl.$resolve = {
    registrations: [
      '$stateParams',
      'Aircrafts',
      function ($stateParams, Aircrafts) {
        return Aircrafts.getRegistrations($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftsRegistrationsSearchCtrl',
    AircraftsRegistrationsSearchCtrl);
}(angular, _));
