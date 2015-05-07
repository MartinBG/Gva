/*global angular*/
(function (angular) {
  'use strict';

  function CertAirworthinessesFMSearchCtrl(
    $scope,
    $state,
    $stateParams,
    scModal,
    aws
  ) {
    $scope.aws = aws;

    $scope.print = function (doc) {
      var params = {
        lotId: $stateParams.id,
        partIndex: doc.partIndex
      };

      var modalInstance = scModal.open('printAirworthiness', params);

      modalInstance.result.then(function (savedAirworthiness) {
        doc.stampNumber = savedAirworthiness.part.stampNumber;
      });

      return modalInstance.opened;
    };
  }

  CertAirworthinessesFMSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'scModal',
    'aws'
  ];

  CertAirworthinessesFMSearchCtrl.$resolve = {
    aws: [
      '$stateParams',
      'AircraftCertAirworthinessesFM',
      function ($stateParams, AircraftCertAirworthinessesFM) {
        return AircraftCertAirworthinessesFM.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
  .controller('CertAirworthinessesFMSearchCtrl', CertAirworthinessesFMSearchCtrl);
}(angular));
