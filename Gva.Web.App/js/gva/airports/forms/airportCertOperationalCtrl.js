/*global angular,_*/
(function (angular,_) {
  'use strict';
  function AirportCertOperationalCtrl($scope, $state, namedModal) {

    $scope.deleteDocument = function (document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.chooseDocuments = function () {
      var modalInstance = namedModal.open('chooseAirportsDocs', {
        includedDocs: _.pluck($scope.model.includedDocuments, 'partIndex')
      });

      modalInstance.result.then(function (selectedDocs) {
        $scope.model.includedDocuments = $scope.model.includedDocuments.concat(selectedDocs);
      });

      return modalInstance.opened;
    };

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

      return $state.go(state, { ind: document.partIndex });
    };
  }

  AirportCertOperationalCtrl.$inject = ['$scope', '$state', 'namedModal'];

  angular.module('gva').controller('AirportCertOperationalCtrl', AirportCertOperationalCtrl);
}(angular,_));
