/*global angular*/
(function (angular) {
  'use strict';

  function AirportOwnersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    documentOwners
  ) {
    $scope.documentOwners = documentOwners;
  }

  AirportOwnersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'documentOwners'
  ];

  AirportOwnersSearchCtrl.$resolve = {
    documentOwners: [
      '$stateParams',
      'AirportDocumentOwners',
      function ($stateParams, AirportDocumentOwners) {
        return AirportDocumentOwners.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOwnersSearchCtrl', AirportOwnersSearchCtrl);
}(angular));
