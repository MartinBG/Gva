/*global angular*/
(function (angular) {
  'use strict';

  function AircraftOthersNewCtrl(
    $scope,
    $state,
    $stateParams,
    AircraftDocumentOthers,
    aircraftDocumentOther
  ) {
    $scope.save = function () {
      return $scope.newDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.newDocumentOtherForm.$valid) {
            return AircraftDocumentOthers
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
    'AircraftDocumentOthers',
    'aircraftDocumentOther'
  ];

  AircraftOthersNewCtrl.$resolve = {
    aircraftDocumentOther: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {},
            files: [{ isAdded: true, applications: [application] }]
          };
        }
        else {
          return {
            part: {},
            files: []
          };
        }
      }
    ]
  };

  angular.module('gva').controller('AircraftOthersNewCtrl', AircraftOthersNewCtrl);
}(angular));
