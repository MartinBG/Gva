﻿/*global angular, _*/
(function (angular, _) {
  'use strict';

  function OrganizationsChooseDocumentsCtrl(
    $state,
    $stateParams,
    $scope,
    certificate,
    availableDocuments,
    Nomenclature
  ) {
    $scope.availableDocuments = availableDocuments;

    if ($stateParams.documentTypes) {
      Nomenclature.query({alias: 'documentParts', set: 'organization'})
      .$promise.then(function(documentTypes){
        $scope.documentParts = _.filter(documentTypes, function (type) {
          return _.contains($stateParams.documentTypes, type.alias);
        });
      });
    }

    $scope.save = function () {
      _.each(_.filter($scope.availableDocuments, { 'checked': true }),
        function (document) {
          certificate.part.includedDocuments.push({
            partIndex: document.partIndex,
            documentType: document.documentType
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

  OrganizationsChooseDocumentsCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'certificate',
    'availableDocuments',
    'Nomenclature'
  ];

  OrganizationsChooseDocumentsCtrl.$resolve = {
    availableDocuments: [
      '$stateParams',
      'OrganizationInventory',
      'certificate',
      function ($stateParams, OrganizationInventory, certificate) {
        return OrganizationInventory
          .query({
            id: $stateParams.id,
            documentTypes: $stateParams.documentTypes? $stateParams.documentTypes.split(',') : null
          })
          .$promise
          .then(function (availableDocuments) {
            return _.reject(availableDocuments, function (availableDocument) {
              var count = _.where(certificate.part.includedDocuments,
                { partIndex: availableDocument.partIndex }).length;
              return count > 0;
            });
          });
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsChooseDocumentsCtrl', OrganizationsChooseDocumentsCtrl);
}(angular, _));
