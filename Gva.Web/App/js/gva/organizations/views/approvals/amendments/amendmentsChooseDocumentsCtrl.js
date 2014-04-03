/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AmendmentsChooseDocumentsCtrl(
    $state,
    $stateParams,
    $scope,
    organizationAmendment,
    availableDocuments) {
    $scope.availableDocuments = availableDocuments;

    $scope.save = function () {
      _.each(_.filter($scope.availableDocuments, { 'checked': true }), function (document) {
        organizationAmendment.part.includedDocuments.push({ linkedDocumentId: document.partIndex });
      });
      return $state.go('^');
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
      'OrganizationDocumentOther',
      'organizationAmendment',
      function ($stateParams, OrganizationDocumentOther, organizationAmendment) {
        return OrganizationDocumentOther.query({ id: $stateParams.id })
            .$promise.then(function (availableDocuments) {
              return _.reject(availableDocuments, function (availableDocument) {
                var count = _.where(organizationAmendment.part.includedDocuments,
                  { linkedDocumentId: availableDocument.partIndex }).length;
                return count > 0;
              });
            });
      }
    ]
  };

  angular.module('gva')
    .controller('AmendmentsChooseDocumentsCtrl', AmendmentsChooseDocumentsCtrl);
}(angular, _));
