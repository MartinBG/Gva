/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditLinkPartCtrl(
    $scope,
    $state,
    $stateParams,
    ApplicationDocFile,
    PersonDocumentId
    ) {

    if ($scope.$parent.docFileType) {
      if ($scope.$parent.docFileType.alias === 'DocumentId') {
        PersonDocumentId.query($stateParams).$promise.then(function (documentIds) {
          $scope.documentPart = documentIds;
        });
      }

      //todo add more
    }

    $scope.linkPart = function (item) {
      return ApplicationDocFile
        .linkExisting({ id: $stateParams.id, docFileId: $scope.$parent.docFileId }, {
          personId: $scope.application.person.id,
          currentDocId: $scope.$parent.currentDocId,
          partIndex: item.partIndex
        }
        ).$promise.then(function () {
          return $state.transitionTo('applications/edit/case', $stateParams, { reload: true });
        });

    };

    $scope.goBack = function () {
      return $state.go('applications/edit/case');
    };
  }

  ApplicationsEditLinkPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'ApplicationDocFile',
    'PersonDocumentId'
  ];

  angular.module('gva').controller('ApplicationsEditLinkPartCtrl', ApplicationsEditLinkPartCtrl);
}(angular
));
