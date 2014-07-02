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

    $scope.editDocumentOwner = function (documentOwner) {
      return $state.go('root.aircrafts.view.owners.edit',
        {
          id: $stateParams.id,
          ind: documentOwner.partIndex
        });
    };

    $scope.newDocumentOwner = function () {
      return $state.go('root.aircrafts.view.owners.new');
    };
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
