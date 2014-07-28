/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseOrgDocsModalCtrl(
    $scope,
    $modalInstance,
    OrganizationsInventory,
    docs,
    includedDocs,
    lotId
  ) {
    $scope.selectedDocs = [];

    $scope.docs = _.filter(docs, function (doc) {
      return !_.contains(includedDocs, doc.partIndex);
    });

    $scope.searchParams = {
      id: lotId,
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
      return OrganizationsInventory.query({
        id: $scope.searchParams.id,
        documentTypes: $scope.searchParams.documentParts ?
          _.pluck($scope.searchParams.documentParts, 'alias') :
          null
      }).$promise.then(function (docs) {
        $scope.docs = _.filter(docs, function (doc) {
          return !_.contains(includedDocs, doc.partIndex);
        });
      });
    };
  }

  ChooseOrgDocsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'OrganizationsInventory',
    'docs',
    'includedDocs',
    'lotId'
  ];

  angular.module('gva').controller('ChooseOrgDocsModalCtrl', ChooseOrgDocsModalCtrl);
}(angular, _, $));
