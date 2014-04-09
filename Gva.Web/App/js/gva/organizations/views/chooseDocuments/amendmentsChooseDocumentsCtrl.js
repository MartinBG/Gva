/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AmendmentsChooseDocumentsCtrl(
    $state,
    $stateParams,
    $scope,
    organizationAmendment,
    availableDocuments
    ) {
    $scope.availableDocuments = availableDocuments;

    $scope.documentsOptions = [
      { id: 'other', text: 'Други документи' },
      { id: 'application', text: 'Заявления' }
    ];

    if ($stateParams.documentTypes) {
      $scope.documentTypes = _.filter($scope.documentsOptions, function (tag) {
        return _.contains($stateParams.documentTypes, tag.id);
      });
    }

    $scope.save = function () {
      _.each(_.filter($scope.availableDocuments, { 'checked': true }), function (document) {
        organizationAmendment.part.includedDocuments.push(
          {
            partIndex: document.partIndex,
            documentType: document.documentType
          });
      });
      return $state.go('^');
    };

    $scope.search = function () {
      return $state.go($state.current, {
        id: $stateParams.id,
        documentTypes: _.pluck($scope.documentTypes, 'id')
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
    'availableDocuments'
  ];

  AmendmentsChooseDocumentsCtrl.$resolve = {
    availableDocuments: [
      '$stateParams',
      'OrganizationInventory',
      'organizationAmendment',
      function ($stateParams, OrganizationInventory, organizationAmendment) {
        return OrganizationInventory.query({
          id: $stateParams.id,
          documentTypes: $stateParams.documentTypes ? $stateParams.documentTypes.split(',') : null
        })
          .$promise.then(function (availableDocuments) {
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
