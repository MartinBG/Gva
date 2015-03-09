/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AircraftsRegistrationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    Nomenclatures,
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
        regMark: $scope.filters.regMark,
        registerId: $scope.filters.registerId
      });
    };
  }

  AircraftsRegistrationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Nomenclatures',
    'registrations'
  ];

  AircraftsRegistrationsSearchCtrl.$resolve = {
    registrations: [
      '$stateParams',
      'Nomenclatures',
      'Aircrafts',
      function ($stateParams, Nomenclatures, Aircrafts) {
        return Aircrafts.getRegistrations($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftsRegistrationsSearchCtrl',
    AircraftsRegistrationsSearchCtrl);
}(angular, _));
