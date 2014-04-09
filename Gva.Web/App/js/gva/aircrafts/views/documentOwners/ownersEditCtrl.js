/*global angular,_*/
(function (angular) {
  'use strict';

  function DocumentOwnersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOwner,
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
      $scope.aircraftDocumentOwner.part = _.cloneDeep(originalOwner.part);
      $scope.$broadcast('cancel', originalOwner);
    };

    $scope.save = function () {
      return $scope.editDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.editDocumentOwnerForm.$valid) {
            return AircraftDocumentOwner
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftDocumentOwner)
              .$promise
              .then(function () {
                return $state.go('root.aircrafts.view.owners.search');
              });
          }
        });
    };

    $scope.deleteOwner = function () {
      return AircraftDocumentOwner.remove({
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
    'AircraftDocumentOwner',
    'aircraftDocumentOwner'
  ];

  DocumentOwnersEditCtrl.$resolve = {
    aircraftDocumentOwner: [
      '$stateParams',
      'AircraftDocumentOwner',
      function ($stateParams, AircraftDocumentOwner) {
        return AircraftDocumentOwner.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentOwnersEditCtrl', DocumentOwnersEditCtrl);
}(angular));