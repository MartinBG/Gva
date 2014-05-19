/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AirportOwnersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AirportDocumentOwner,
    airportDocumentOwner
  ) {
    var originalDoc = _.cloneDeep(airportDocumentOwner);

    $scope.airportDocumentOwner = airportDocumentOwner;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.airportDocumentOwner = _.cloneDeep(originalDoc);
    };

    $scope.save = function () {
      return $scope.editDocumentOwnerForm.$validate()
        .then(function () {
          if ($scope.editDocumentOwnerForm.$valid) {
            return AirportDocumentOwner
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.airportDocumentOwner)
              .$promise
              .then(function () {
                return $state.go('root.airports.view.owners.search');
              });
          }
        });
    };

    $scope.deleteInspection = function () {
      return AirportDocumentOwner.remove({
        id: $stateParams.id,
        ind: airportDocumentOwner.partIndex
      }).$promise.then(function () {
        return $state.go('root.airports.view.owners.search');
      });
    };
  }

  AirportOwnersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AirportDocumentOwner',
    'airportDocumentOwner'
  ];

  AirportOwnersEditCtrl.$resolve = {
    airportDocumentOwner: [
      '$stateParams',
      'AirportDocumentOwner',
      function ($stateParams, AirportDocumentOwner) {
        return AirportDocumentOwner.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AirportOwnersEditCtrl', AirportOwnersEditCtrl);
}(angular, _));