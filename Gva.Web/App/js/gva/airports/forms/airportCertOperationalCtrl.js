/*global angular*/
(function (angular) {
  'use strict';
  function AirportCertOperationalCtrl($scope, $state) {

    $scope.deleteDocument = function (document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.chooseDocuments = function () {
      $state.go('.chooseDocuments', {}, {}, {
        selectedDocuments: $scope.model.includedDocuments
      });
    };

    // coming from a child state and carrying payload
    if ($state.previous && $state.previous.includes[$state.current.name] && $state.payload) {
      if ($state.payload.selectedDocuments) {
        [].push.apply($scope.model.includedDocuments, $state.payload.selectedDocuments);
      }
    }

    $scope.viewDocument = function (document) {
      var state;

      if (document.setPartAlias === 'airportOther') {
        state = 'root.airports.view.others.edit';
      }
      else if (document.setPartAlias === 'airportOwner') {
        state = 'root.airports.view.owners.edit';
      }
      else if (document.setPartAlias === 'airportApplication') {
        state = 'root.airports.view.applications.edit';
      }
      else if (document.setPartAlias === 'inspection') {
        state = 'root.airports.view.inspections.edit';
      }

      return $state.go(state, { ind: document.partIndex });
    };
  }

  AirportCertOperationalCtrl.$inject = ['$scope','$state'];

  angular.module('gva').controller('AirportCertOperationalCtrl', AirportCertOperationalCtrl);
}(angular));
