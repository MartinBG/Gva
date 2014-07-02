/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessFM,
    aircraftCertAirworthiness
  ) {
    $scope.isEdit = false;

    $scope.aw = aircraftCertAirworthiness;

    $scope.save = function () {
      return $scope.newCertAirworthinessForm.$validate()
        .then(function () {
          if ($scope.newCertAirworthinessForm.$valid) {
            return AircraftCertAirworthinessFM
                .save({ id: $stateParams.id }, $scope.aw).$promise
                .then(function () {
              return $state.go('root.aircrafts.view.airworthinessesFM.search');
            });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.airworthinessesFM.search');
    };
  }

  CertAirworthinessesFMNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessFM',
    'aircraftCertAirworthiness'
  ];
  CertAirworthinessesFMNewCtrl.$resolve = {
    inspectorType: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.get({
          alias: 'inspectorTypes',
          valueAlias: 'examiner'
        }).$promise;
      }
    ],
    airworthinessReviewType: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.get({
          alias: 'airworthinessReviewTypes',
          valueAlias: '15a'
        }).$promise;
      }
    ],
    aircraftCertAirworthiness: [
      '$stateParams',
      'inspectorType',
      'airworthinessReviewType',
      function ($stateParams, inspectorType, airworthinessReviewType) {
        return {
          part: {
            lotId: $stateParams.id,
            inspector: {
              inspectorType: inspectorType
            },
            reviews: [{
              inspector: {
                inspectorType: inspectorType
              },
              airworthinessReviewType: airworthinessReviewType,
              amendment1: null,
              amendment2: null
            }]
          },
          files: []
        };
      }
    ]
  };

  angular.module('gva').controller('CertAirworthinessesFMNewCtrl', CertAirworthinessesFMNewCtrl);
}(angular));
