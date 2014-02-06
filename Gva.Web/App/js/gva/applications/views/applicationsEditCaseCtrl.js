/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditCaseCtrl(
    $scope,
    $state
    ) {
    $scope.linkNew = function (docId, docFile) {
      $scope.documentData.isLinkNew = true;
      $scope.documentData.docFiles = [];
      $scope.documentData.docFiles.push(docFile);
      $scope.documentData.currentDocId = docId;
      $scope.documentData.docPartType = null;

      return $state.go('applications/edit/newfile');
    };

    $scope.linkPart = function (docId, docFile) {
      $scope.documentData.docFiles = [];
      $scope.documentData.docFiles.push(docFile);
      $scope.documentData.currentDocId = docId;
      $scope.documentData.docPartType = null;

      return $state.go('applications/edit/linkpart');
    };

    $scope.newfile = function (docCase) {
      $scope.documentData.docPartType = null;
      $scope.documentData.currentDocId = docCase.docInfo.docId;
      return $state.go('applications/edit/newfile');
    };

    $scope.viewPart = function (docCase) {
      var path = '';

      if (docCase.appFile.setPartAlias === 'DocumentId') {
        path = 'persons.documentIds.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentEducation') {
        path = 'persons.documentEducations.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentEmployment') {
        path = 'persons.employments.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentMed') {
        path = 'persons.medicals.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentCheck') {
        path = 'persons.checks.edit';
      }
      //else if (docCase.setPartId === 6) {
      //path = 'persons.documentIds.edit';
      //}
      //else if (docCase.setPartId === 7) {
      //path = 'persons.documentIds.edit';
      //}

      return $state.go(path, {
        id: $scope.application.lotId,
        ind: docCase.appFile.partIndex
      });
    };

    $scope.viewDoc = function (docId) {
      return $state.go('docs/edit/addressing', { docId: docId });
    };
  }

  ApplicationsEditCaseCtrl.$inject = [
    '$scope',
    '$state'
  ];

  angular.module('gva').controller('ApplicationsEditCaseCtrl', ApplicationsEditCaseCtrl);
}(angular));
