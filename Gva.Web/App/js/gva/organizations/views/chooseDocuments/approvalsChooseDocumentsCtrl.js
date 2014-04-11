/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ApprovalsChooseDocumentsCtrl(
    $state,
    $stateParams,
    $scope,
    organizationApproval,
    availableDocuments,
    documentParts
  ) {
    $scope.availableDocuments = availableDocuments;
    $scope.documentParts = documentParts;

    $scope.save = function () {
      _.each(_.filter($scope.availableDocuments, { 'checked': true }), function (document) {
        _.last(organizationApproval.part.amendments).includedDocuments.push(
          {
            partIndex: document.partIndex,
            setPartAlias: document.setPartAlias
          });
      });
      return $state.go('^');
    };

    $scope.search = function () {
      return $state.go($state.current, {
        id: $stateParams.id,
        documentTypes: _.pluck($scope.documentParts, 'alias')
      });
    };

    $scope.goBack = function () {
      return $state.go('^');
    };
  }

  ApprovalsChooseDocumentsCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'organizationApproval',
    'availableDocuments',
    'documentParts'
  ];

  ApprovalsChooseDocumentsCtrl.$resolve = {
    availableDocuments: [
      '$stateParams',
      'OrganizationInventory',
      'organizationApproval',
      function ($stateParams, OrganizationInventory, organizationApproval) {
        return OrganizationInventory
          .query({
            id: $stateParams.id,
            documentTypes: $stateParams.documentTypes ? $stateParams.documentTypes.split(',') : null
          })
          .$promise
          .then(function (availableDocuments) {
            return _.reject(availableDocuments, function (availableDocument) {
              var lastAmendment = _.last(organizationApproval.part.amendments),
                count = _.where(lastAmendment.includedDocuments,
                { partIndex: availableDocument.partIndex }).length;
              return count > 0;
            });
          });
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
    .controller('ApprovalsChooseDocumentsCtrl', ApprovalsChooseDocumentsCtrl);
}(angular, _));
