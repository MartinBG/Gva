/*global angular,_*/
(function (angular,_) {
  'use strict';
  function AirportCertOperationalCtrl($scope, $state, scModal, scFormParams) {
    $scope.lotId = scFormParams.lotId;

    $scope.deleteDocument = function (document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.chooseDocuments = function () {
      var modalInstance = scModal.open('chooseAirportsDocs', {
        includedDocs: _.pluck($scope.model.includedDocuments, 'partIndex'),
        lotId: $scope.lotId
      });

      modalInstance.result.then(function (selectedDocs) {
        $scope.model.includedDocuments = $scope.model.includedDocuments.concat(selectedDocs);
      });

      return modalInstance.opened;
    };

    $scope.viewDocument = function (document) {
      var state;
      var params = {};
      if (document.setPartAlias === 'airportOther') {
        state = 'root.airports.view.others.edit';
        params = { ind: document.partIndex };
      }
      else if (document.setPartAlias === 'airportOwner') {
        state = 'root.airports.view.owners.edit';
        params = { ind: document.partIndex };
      }
      else if (document.setPartAlias === 'airportApplication') {
        state = 'root.applications.edit.data';
        params = { 
          ind: document.partIndex,
          id: document.applicationId,
          set: 'airport',
          lotId: $scope.lotId
        };
      }

      return $state.go(state, params);
    };
  }

  AirportCertOperationalCtrl.$inject = ['$scope', '$state', 'scModal', 'scFormParams'];

  angular.module('gva').controller('AirportCertOperationalCtrl', AirportCertOperationalCtrl);
}(angular,_));
