/*global angular,_*/
(function (angular) {
  'use strict';

  function AircraftOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOther,
    aircraftDocumentOther
  ) {
    var originalDocument = _.cloneDeep(aircraftDocumentOther);

    $scope.aircraftDocumentOther = aircraftDocumentOther;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.aircraftDocumentOther.part = _.cloneDeep(originalDocument.part);
      $scope.$broadcast('cancel', originalDocument);
    };

    $scope.save = function () {
      return $scope.editDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.editDocumentOtherForm.$valid) {
            return AircraftDocumentOther
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.aircraftDocumentOther)
              .$promise
              .then(function () {
                return $state.go('root.aircrafts.view.others.search');
              });
          }
        });
    };

    $scope.deleteDocumentOther = function () {
      return AircraftDocumentOther.remove({
        id: $stateParams.id,
        ind: aircraftDocumentOther.partIndex
      }).$promise.then(function () {
          return $state.go('root.aircrafts.view.others.search');
        });
    };
  }

  AircraftOthersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOther',
    'aircraftDocumentOther'
  ];

  AircraftOthersEditCtrl.$resolve = {
    aircraftDocumentOther: [
      '$stateParams',
      'AircraftDocumentOther',
      function ($stateParams, AircraftDocumentOther) {
        return AircraftDocumentOther.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftOthersEditCtrl', AircraftOthersEditCtrl);
}(angular));