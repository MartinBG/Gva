/*global angular,_*/
(function (angular, _) {
  'use strict';

  function CertAirworthinessesFMEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftCertAirworthinessesFM,
    originalAirworthiness,
    scMessage
  ) {
    var originalAw = _.cloneDeep(originalAirworthiness);
    $scope.airworthiness = originalAw;
    $scope.isEditAw = false;
    $scope.lotId = $stateParams.id;
    $scope.partIndex = $stateParams.ind;
    $scope.certTypeAlias = $scope.airworthiness.part.airworthinessCertificateType.alias;

    $scope.editAw = function () {
     $scope.isEditAw = true;
    };

    $scope.cancelAw = function () {
      $scope.isEditAw = false;
      $scope.airworthiness = _.cloneDeep(originalAw);
    };

    $scope.deleteAw = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return AircraftCertAirworthinessesFM
          .remove({ id: $stateParams.id, ind: $stateParams.ind })
          .$promise.then(function () {
            return $state.go('root.aircrafts.view.airworthinessesFM.search');
          });
        }
      });
    };

    $scope.saveAw = function () {
      return $scope.aircraftCertAirworthinessForm.$validate()
      .then(function () {
        if ($scope.aircraftCertAirworthinessForm.$valid) {
          return AircraftCertAirworthinessesFM
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.airworthiness)
            .$promise
            .then(function () {
              $scope.isEditAw = false;
            });
        }
      });
    };
  }

  CertAirworthinessesFMEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftCertAirworthinessesFM',
    'aircraftCertAirworthiness',
    'scMessage'
  ];

  CertAirworthinessesFMEditCtrl.$resolve = {
    aircraftCertAirworthiness: [
      '$stateParams',
      'AircraftCertAirworthinessesFM',
      function ($stateParams, AircraftCertAirworthinessesFM) {
        return AircraftCertAirworthinessesFM.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };
  
  angular.module('gva').controller('CertAirworthinessesFMEditCtrl', CertAirworthinessesFMEditCtrl);
}(angular, _));
