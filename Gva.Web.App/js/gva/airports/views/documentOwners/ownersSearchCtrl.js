/*global angular*/
(function (angular) {
  'use strict';

  function AirportOwnersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentOwner,
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

    $scope.deleteDocumentOwner = function (documentOwner) {
      return AirportDocumentOwner.remove({ id: $stateParams.id, ind: documentOwner.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'AirportDocumentOwner',
    'documentOwners'
  ];

  AirportOwnersSearchCtrl.$resolve = {
    documentOwners: [
      '$stateParams',
      'AirportDocumentOwner',
      function ($stateParams, AirportDocumentOwner) {
        return AirportDocumentOwner.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOwnersSearchCtrl', AirportOwnersSearchCtrl);
}(angular));
