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
      regMark: null,
      registerId: null
    };

    if (!$stateParams.registerId) {
      $scope.filters.registerId = Nomenclatures.get({
        alias: 'registers',
        valueAlias: 'register1'
      }).nomValueId;
    } 

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
        if(!$stateParams.registerId) {
          return Nomenclatures.get({
            alias: 'registers',
            valueAlias: 'register1'
          })
            .$promise
            .then(function (nom) {
              var params = _.assign($stateParams, {registerId: nom.nomValueId});
              return Aircrafts.getRegistrations(params).$promise;
            });
        }
        else {
          return Aircrafts.getRegistrations($stateParams).$promise;
        }
      }
    ]
  };

  angular.module('gva').controller('AircraftsRegistrationsSearchCtrl',
    AircraftsRegistrationsSearchCtrl);
}(angular, _));
