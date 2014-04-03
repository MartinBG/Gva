/*global angular, _*/
(function (angular, _) {
  'use strict';

  function OrganizationsChooseDocumentsCtrl(
    $state,
    $stateParams,
    $scope,
    certificate,
    availableDocuments) {
    $scope.availableDocuments = availableDocuments;

    $scope.save = function () {
      _.each(_.filter($scope.availableDocuments, { 'checked': true }),
        function (document) {
          certificate.part.includedDocuments.push({ linkedDocumentId: document.partIndex });
        });
      return $state.go('^');
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
    'availableDocuments'
  ];

  OrganizationsChooseDocumentsCtrl.$resolve = {
    availableDocuments: [
      '$stateParams',
      'OrganizationDocumentOther',
      'certificate',
      function ($stateParams, OrganizationDocumentOther, certificate) {
        return OrganizationDocumentOther.query({ id: $stateParams.id })
            .$promise.then(function (availableDocuments) {
              return _.reject(availableDocuments, function (availableDocument) {
                var count = _.where(certificate.part.includedDocuments,
                  { linkedDocumentId: availableDocument.partIndex }).length;
                return count > 0;
              });
            });
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsChooseDocumentsCtrl', OrganizationsChooseDocumentsCtrl);
}(angular, _));
