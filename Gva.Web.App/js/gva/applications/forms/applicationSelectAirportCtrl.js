/*global angular*/
(function (angular) {
  'use strict';
  function AppSelectAirportCtrl($scope, namedModal) {
    $scope.chooseAirport = function () {
      var modalInstance = namedModal.open('chooseAirport', null, {
        airports: [
          'Airports',
          function (Airports) {
            return Airports.query().$promise;
          }
        ]
      });

      modalInstance.result.then(function (airportId) {
        $scope.model.lot.id = airportId;
      });

      return modalInstance.opened;
    };

    $scope.newAirport = function () {
      var airport = {
        airportData: {
          caseTypes: [
            {
              nomValueId: 4
            }
          ],
          frequencies: [],
          radioNavigationAids: []
        }
      };

      var modalInstance = namedModal.open('newAirport', { airport: airport });

      modalInstance.result.then(function (airportId) {
        $scope.model.lot.id = airportId;
      });

      return modalInstance.opened;
    };
  }

  AppSelectAirportCtrl.$inject = ['$scope', 'namedModal'];

  angular.module('gva').controller('AppSelectAirportCtrl', AppSelectAirportCtrl);
}(angular));
