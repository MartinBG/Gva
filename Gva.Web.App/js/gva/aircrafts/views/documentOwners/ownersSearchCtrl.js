/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOwnersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    documentOwners
  ) {
    $scope.documentOwners = documentOwners;
  }

  DocumentOwnersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'documentOwners'
  ];

  DocumentOwnersSearchCtrl.$resolve = {
    documentOwners: [
      '$stateParams',
      'AircraftDocumentOwners',
      function ($stateParams, AircraftDocumentOwners) {
        return AircraftDocumentOwners.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentOwnersSearchCtrl', DocumentOwnersSearchCtrl);
}(angular));
