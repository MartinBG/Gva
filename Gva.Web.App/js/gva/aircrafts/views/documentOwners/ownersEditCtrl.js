/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentOwnersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOwners,
    aircraftDocumentOwner
  ) {
    var originalOwner = _.cloneDeep(aircraftDocumentOwner);

    $scope.aircraftDocumentOwner = aircraftDocumentOwner;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aircraftDocumentOwner = _.cloneDeep(originalOwner);
    };

    $scope.save = function () {
      return $scope.editDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.editDocumentOwnerForm.$valid) {
            return AircraftDocumentOwners
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftDocumentOwner)
              .$promise
              .then(function () {
                return $state.go('root.aircrafts.view.owners.search');
              });
          }
        });
    };

    $scope.deleteOwner = function () {
      return AircraftDocumentOwners.remove({
        id: $stateParams.id,
        ind: aircraftDocumentOwner.partIndex
      }).$promise.then(function () {
          return $state.go('root.aircrafts.view.owners.search');
        });
    };
  }

  DocumentOwnersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOwners',
    'aircraftDocumentOwner'
  ];

  DocumentOwnersEditCtrl.$resolve = {
    aircraftDocumentOwner: [
      '$stateParams',
      'AircraftDocumentOwners',
      function ($stateParams, AircraftDocumentOwners) {
        return AircraftDocumentOwners.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentOwnersEditCtrl', DocumentOwnersEditCtrl);
}(angular));