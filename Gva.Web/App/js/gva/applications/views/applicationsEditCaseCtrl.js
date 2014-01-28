/*global angular, require*/
(function (angular) {
  'use strict';

  function ApplicationsEditCaseCtrl(
    $scope,
    $state,
    $stateParams
    ) {
    var nomenclatures = require('./nomenclatures.sample');

    $scope.$parent.docFileType = null;
    $scope.$parent.isLinkNew = false;

    $scope.linkNew = function (docId, docFile) {
      $scope.$parent.docFileType = nomenclatures.get('documents', docFile.docFileTypeAlias);
      $scope.$parent.isLinkNew = true;
      $scope.$parent.docFileId = docFile.docFileId;
      $scope.$parent.currentDocId = docId;

      return $state.go('applications/edit/addpart');
    };

    $scope.linkPart = function (docId, docFile) {
      $scope.$parent.docFileType = nomenclatures.get('documents', docFile.docFileTypeAlias);
      $scope.$parent.docFileId = docFile.docFileId;
      $scope.$parent.currentDocId = docId;

      return $state.go('applications/edit/linkpart');
    };

    $scope.newfile = function (c) {
      $scope.$parent.currentDocId = c.docId;
      return $state.go('applications/edit/newfile');
    };

    $scope.viewPart = function (partIndex) {
      return $state.go('persons.documentIds.edit', {
        id: $stateParams.id,
        ind: partIndex
      });
    };

    $scope.viewDoc = function (docId) {
      return $state.go('docs/edit/addressing', { docId: docId });
    };
  }

  ApplicationsEditCaseCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams'
  ];

  angular.module('gva').controller('ApplicationsEditCaseCtrl', ApplicationsEditCaseCtrl);
}(angular));
