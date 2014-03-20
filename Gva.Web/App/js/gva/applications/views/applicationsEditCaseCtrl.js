/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditCaseCtrl(
    $scope,
    $state,
    application
    ) {
    $scope.application = application;

    $scope.linkNew = function (docId, docFile) {
      return $state.go('root.applications.edit.case.newFile',
        {
          isLinkNew: true,
          currentDocId: docId,
          docFileKey: docFile.key,
          docFileName: docFile.name
        });
    };

    $scope.linkPart = function (docId, docFile) {
      return $state.go('root.applications.edit.case.linkPart',
        {
          currentDocId: docId,
          docFileKey: docFile.key,
          docFileName: docFile.name
        });
    };

    $scope.newFile = function (docId) {
      return $state.go('root.applications.edit.case.newFile',
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
      else if (docCase.appFile.setPartAlias === 'DocumentOther') {
        state = 'root.persons.view.documentOthers.edit';
      }
      else if (docCase.appFile.setPartAlias === 'DocumentApplication') {
        state = 'root.persons.view.documentApplications.edit';
      }

      return $state.go(state, {
        id: $scope.application.lotId,
        ind: docCase.appFile.partIndex
      });
    };

    $scope.viewDoc = function (docId) {
      return $state.go('root.docs.edit.view', { docId: docId });
    };
  }

  ApplicationsEditCaseCtrl.$inject = [
    '$scope',
    '$state',
    'application'
  ];

  angular.module('gva').controller('ApplicationsEditCaseCtrl', ApplicationsEditCaseCtrl);
}(angular));
