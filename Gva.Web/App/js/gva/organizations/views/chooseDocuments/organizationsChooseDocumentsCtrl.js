/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function OrganizationsChooseDocumentsCtrl(
    $state,
    $stateParams,
    $scope,
    documents,
    documentParts
  ) {

    $scope.selectedDocuments = [];

    $scope.availableDocuments =  _.reject(documents, function (document) {
        var count = _.where($state.payload.selectedDocuments,
          { partIndex: document.partIndex }).length;
        return count > 0;
      });

    $scope.documentParts = documentParts;

    $scope.save = function () {
      var documents = [];
      _.each($scope.selectedDocuments, function (document) {
        documents.push({
          partIndex: document.partIndex,
          setPartAlias: document.setPartAlias
        });
      });

      return $state.go('^', {}, {}, {
        selectedDocuments: documents
      });
    };

    $scope.selectDocument = function (event, document) {
      if ($(event.target).is(':checked')) {
        $scope.selectedDocuments.push(document);
      }
      else {
        $scope.selectedDocuments = _.without($scope.selectedDocuments, document);
      }
    };

    $scope.search = function () {
      return $state.go('.', {
          id: $stateParams.id,
          documentTypes: _.pluck($scope.documentParts, 'alias')
        }, {}, {
          selectedDocuments: $state.payload.selectedDocuments
        });
    };

    $scope.goBack = function () {
      return $state.go('^');
    };

  }

  OrganizationsChooseDocumentsCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'documents',
    'documentParts'
  ];

  OrganizationsChooseDocumentsCtrl.$resolve = {
    documents: [
      '$stateParams',
      'OrganizationInventory',
      function ($stateParams, OrganizationInventory) {
        return OrganizationInventory
          .query({
            id: $stateParams.id,
            documentTypes: $stateParams.documentTypes? $stateParams.documentTypes.split(',') : null
          }).$promise;
      }
    ],
    documentParts: [
      '$stateParams',
      'Nomenclature',
      function ($stateParams, Nomenclature) {
        if ($stateParams.documentTypes) {
          return Nomenclature.query({alias: 'documentParts', set: 'organization'})
          .$promise.then(function(documentTypes){
            return  _.filter(documentTypes, function (type) {
              return _.contains($stateParams.documentTypes, type.alias);
            });
          });
        } else {
          return [];
        }
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsChooseDocumentsCtrl', OrganizationsChooseDocumentsCtrl);
}(angular, _, $));
