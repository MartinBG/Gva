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
    $scope.aircraftDocumentOther = aircraftDocumentOther;
    $scope.lotId = $stateParams.id;

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
      '$stateParams',
      'AircraftDocumentOthers',
      function ($stateParams, AircraftDocumentOthers) {
        return AircraftDocumentOthers.newDocumentOther({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AircraftOthersNewCtrl', AircraftOthersNewCtrl);
}(angular));
