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

    $scope.newfile = function (c) {
      $scope.documentData.docPartType = null;
      $scope.documentData.currentDocId = c.docId;
      return $state.go('applications/edit/newfile');
    };

    $scope.viewPart = function (docCase) {
      var path = '';

      if (docCase.setPartId === 1) {
        path = 'persons.documentIds.edit';
      }
      else if (docCase.setPartId === 2) {
        path = 'persons.documentEducations.edit';
      }
      else if (docCase.setPartId === 3) {
        path = 'persons.employments.edit';
      }
      else if (docCase.setPartId === 4) {
        path = 'persons.medicals.edit';
      }
      else if (docCase.setPartId === 5) {
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
        ind: docCase.partIndex
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
