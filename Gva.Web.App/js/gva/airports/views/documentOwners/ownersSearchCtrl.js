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

    $scope.editDocumentOwner = function (documentOwner) {
      return $state.go('root.airports.view.owners.edit',
        {
          id: $stateParams.id,
          ind: documentOwner.partIndex
        });
    };

    $scope.newDocumentOwner = function () {
      return $state.go('root.airports.view.owners.new');
    };
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
