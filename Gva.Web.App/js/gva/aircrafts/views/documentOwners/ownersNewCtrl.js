/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOwnersNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOwners,
    aircraftDocumentOwner
  ) {
    $scope.aircraftDocumentOwner = aircraftDocumentOwner;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.newDocumentOwnerForm.$valid) {
            return AircraftDocumentOwners
              .save({ id: $stateParams.id }, $scope.aircraftDocumentOwner).$promise
              .then(function () {
                return $state.go('root.aircrafts.view.owners.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.owners.search');
    };
  }

  DocumentOwnersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOwners',
    'aircraftDocumentOwner'
  ];

  DocumentOwnersNewCtrl.$resolve = {
    aircraftDocumentOwner: [
      '$stateParams',
      'AircraftDocumentOwners',
      function ($stateParams, AircraftDocumentOwners) {
        return AircraftDocumentOwners.newDocumentOwner({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentOwnersNewCtrl', DocumentOwnersNewCtrl);
}(angular));
