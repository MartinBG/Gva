/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AmendmentsChooseDocumentsCtrl(
    $state,
    $stateParams,
    $scope,
    organizationAmendment,
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
      _.each(_.filter($scope.availableDocuments, { 'checked': true }), function (document) {
        organizationAmendment.part.includedDocuments.push(
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

  AmendmentsChooseDocumentsCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'organizationAmendment',
    'availableDocuments',
    'Nomenclature'
  ];

  AmendmentsChooseDocumentsCtrl.$resolve = {
    availableDocuments: [
      '$stateParams',
      'OrganizationInventory',
      'organizationAmendment',
      function ($stateParams, OrganizationInventory, organizationAmendment) {
        return OrganizationInventory
          .query({
            id: $stateParams.id,
            documentTypes: $stateParams.documentTypes ? $stateParams.documentTypes.split(',') : null
          })
          .$promise
          .then(function (availableDocuments) {
            return _.reject(availableDocuments, function (availableDocument) {
              var count = _.where(organizationAmendment.part.includedDocuments,
                { partIndex: availableDocument.partIndex }).length;
              return count > 0;
            });
          });
      }
    ]
  };

  angular.module('gva')
    .controller('AmendmentsChooseDocumentsCtrl', AmendmentsChooseDocumentsCtrl);
}(angular, _));
