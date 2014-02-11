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

      return $state.go('root.applications.edit.newfile');
    };

    $scope.linkPart = function (docId, docFile) {
      $scope.documentData.docFiles = [];
      $scope.documentData.docFiles.push(docFile);
      $scope.documentData.currentDocId = docId;
      $scope.documentData.docPartType = null;

      return $state.go('root.applications.edit.linkpart');
    };

    $scope.newfile = function (docCase) {
      $scope.documentData.docPartType = null;
      $scope.documentData.currentDocId = docCase.docInfo.docId;
      return $state.go('root.applications.edit.newfile');
    };

    $scope.viewPart = function (docCase) {
      var path = '';

      if (docCase.appFile.setPartAlias === 'DocumentId') {
        path = 'root.persons.view.documentIds.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentEducation') {
        path = 'root.persons.view.documentEducations.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentEmployment') {
        path = 'root.persons.view.employments.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentMed') {
        path = 'root.persons.view.medicals.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentCheck') {
        path = 'root.persons.view.checks.edit';
      }
      //else if (docCase.setPartId === 6) {
      //path = 'root.persons.view.documentIds.edit';
      //}
      //else if (docCase.setPartId === 7) {
      //path = 'root.persons.view.documentIds.edit';
      //}

      return $state.go(path, {
        id: $scope.application.lotId,
        ind: docCase.appFile.partIndex
      });
    };

    $scope.viewDoc = function (docId) {
      return $state.go('root.docs.edit.addressing', { docId: docId });
    };
  }

  ApplicationsEditCaseCtrl.$inject = [
    '$scope',
    '$state'
  ];

  angular.module('gva').controller('ApplicationsEditCaseCtrl', ApplicationsEditCaseCtrl);
}(angular));
