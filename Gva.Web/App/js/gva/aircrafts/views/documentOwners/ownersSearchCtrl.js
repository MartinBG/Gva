/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOwnersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOwner,
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

    $scope.deleteDocumentOwner = function (documentOwner) {
      return AircraftDocumentOwner.remove({ id: $stateParams.id, ind: documentOwner.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'AircraftDocumentOwner',
    'documentOwners'
  ];

  DocumentOwnersSearchCtrl.$resolve = {
    documentOwners: [
      '$stateParams',
      'AircraftDocumentOwner',
      function ($stateParams, AircraftDocumentOwner) {
        return AircraftDocumentOwner.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentOwnersSearchCtrl', DocumentOwnersSearchCtrl);
}(angular));
