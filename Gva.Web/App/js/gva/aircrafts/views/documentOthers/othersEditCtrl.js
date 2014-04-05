/*global angular*/
(function (angular) {
  'use strict';

  function AircraftOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOther,
    aircraftDocumentOther
  ) {

    $scope.aircraftDocumentOther = aircraftDocumentOther;

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

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.others.search');
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