/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseAirportDocsModalCtrl(
    $scope,
    $modalInstance,
    AirportsInventory,
    scModalParams,
    docs
  ) {
    $scope.selectedDocs = [];

    $scope.docs = _.filter(docs, function (doc) {
      return !_.contains(scModalParams.includedDocs, doc.partIndex);
    });

    $scope.searchParams = {
      id: scModalParams.lotId,
      documentParts: []
    };

    $scope.addDocs = function () {
      var documents = [];
      _.each($scope.selectedDocs, function (doc) {
        documents.push({
          partIndex: doc.partIndex,
          setPartAlias: doc.setPartAlias
        });
      });

      return $modalInstance.close(documents);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectDoc = function (event, doc) {
      if ($(event.target).is(':checked')) {
        $scope.selectedDocs.push(doc);
      }
      else {
        $scope.selectedDocs = _.without($scope.selectedDocs, doc);
      }
    };

    $scope.search = function () {
      return AirportsInventory.query({
        id: $scope.searchParams.id,
        documentTypes: $scope.searchParams.documentParts ?
          _.pluck($scope.searchParams.documentParts, 'alias') :
          null
      }).$promise.then(function (docs) {
        $scope.docs = _.filter(docs, function (doc) {
          return !_.contains(scModalParams.includedDocs, doc.partIndex);
        });
      });
    };
  }

  ChooseAirportDocsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'AirportsInventory',
    'scModalParams',
    'docs'
  ];

  ChooseAirportDocsModalCtrl.$resolve = {
    docs: [
      'AirportsInventory',
      'scModalParams',
      function (AirportsInventory, scModalParams) {
        return AirportsInventory.query({ id: scModalParams.lotId }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseAirportDocsModalCtrl', ChooseAirportDocsModalCtrl);
}(angular, _, $));
