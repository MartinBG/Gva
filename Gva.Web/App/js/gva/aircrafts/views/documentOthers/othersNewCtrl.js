/*global angular*/
(function (angular) {
  'use strict';

  function AircraftOthersNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOther,
    aircraftDocumentOther
  ) {
    $scope.save = function () {
      return $scope.aircraftDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.aircraftDocumentOtherForm.$valid) {
            return AircraftDocumentOther
              .save({ id: $stateParams.id }, $scope.aircraftDocumentOther).$promise
              .then(function () {
                return $state.go('root.aircrafts.view.others.search');
              });
          }
        });
    };

    $scope.aircraftDocumentOther = aircraftDocumentOther;

    $scope.cancel = function () {
      return $state.go('root.aircrafts.view.others.search');
    };
  }

  AircraftOthersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'AircraftDocumentOther',
    'aircraftDocumentOther'
  ];

  AircraftOthersNewCtrl.$resolve = {
    aircraftDocumentOther: function () {
      return {
        part: {}
      };
    }
  };

  angular.module('gva').controller('AircraftOthersNewCtrl', AircraftOthersNewCtrl);
}(angular));