/*global angular,_*/
(function (angular,_) {
  'use strict';

  function OrganizationCertificateCtrl(
    $scope,
    $state,
    scModal,
    scFormParams) {
    $scope.lotId = scFormParams.lotId;

    $scope.deleteDocument = function (document) {
      var index = $scope.model.includedDocuments.indexOf(document);
      $scope.model.includedDocuments.splice(index, 1);
    };

    $scope.chooseDocuments = function () {
      var modalInstance = scModal.open('chooseOrganizationDocs', {
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

      if (document.setPartAlias === 'organizationOther') {
        state = 'root.organizations.view.documentOthers.edit';
      }
      else if (document.setPartAlias === 'organizationApplication') {
        state = 'root.organizations.view.documentApplications.edit';
      }

      return $state.go(state, { ind: document.partIndex });
    };
  }

  OrganizationCertificateCtrl.$inject = [
    '$scope',
    '$state',
    'scModal',
    'scFormParams'
  ];

  angular.module('gva')
    .controller('OrganizationCertificateCtrl', OrganizationCertificateCtrl);
}(angular,_));
