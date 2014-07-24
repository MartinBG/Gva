/*global angular*/
(function (angular) {
  'use strict';

  function NewAirportModalCtrl(
    $scope,
    $modalInstance,
    Airports,
    airport
  ) {
    $scope.form = {};
    $scope.airport = airport;

    $scope.save = function () {
      return $scope.form.newAirportForm.$validate().then(function () {
        if ($scope.form.newAirportForm.$valid) {
          return Airports.save($scope.airport).$promise.then(function (savedAirport) {
            return $modalInstance.close(savedAirport.id);
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewAirportModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Airports',
    'airport'
  ];

  NewAirportModalCtrl.$resolve = {
    airport: function () {
      return {
        airportData: {
          caseTypes: [
            {
              nomValueId: 4 // TO DO Remove hardcoded caseTypes
            }
          ],
          frequencies: [],
          radioNavigationAids: []
        }
      };
    }
  };

  angular.module('gva').controller('NewAirportModalCtrl', NewAirportModalCtrl);
}(angular));
