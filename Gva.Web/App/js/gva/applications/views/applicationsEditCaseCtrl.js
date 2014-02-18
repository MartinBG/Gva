/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditCaseCtrl(
    $scope,
    $state
    ) {

    $scope.linkNew = function (docId, docFile) {
      return $state.go('root.applications.edit.newFile',
        {
          isLinkNew: true,
          currentDocId: docId,
          docFileKey: docFile.key,
          docFileName: docFile.name
        });
    };

    $scope.linkPart = function (docId, docFile) {
      return $state.go('root.applications.edit.linkPart',
        {
          currentDocId: docId,
          docFileKey: docFile.key,
          docFileName: docFile.name
        });
    };

    $scope.newFile = function (docId) {
      return $state.go('root.applications.edit.newFile',
        {
          isLinkNew: false,
          currentDocId: docId
        });
    };

    $scope.viewPart = function (docCase) {
      var state;

      if (docCase.appFile.setPartAlias === 'DocumentId') {
        state = 'root.persons.view.documentIds.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentEducation') {
        state = 'root.persons.view.documentEducations.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentEmployment') {
        state = 'root.persons.view.employments.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentMed') {
        state = 'root.persons.view.medicals.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentCheck') {
        state = 'root.persons.view.checks.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentTraining') {
        state = 'root.persons.view.documentTrainings.edit';
      }
      //else if (docCase.setPartId === 7) {
      //state = 'root.persons.view.documentIds.edit';
      //}

      return $state.go(state, {
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
