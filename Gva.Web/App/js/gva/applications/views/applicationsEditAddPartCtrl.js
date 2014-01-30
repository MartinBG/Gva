/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditAddPartCtrl(
    $scope,
    $state,
    $stateParams,
    ApplicationDocFile
    ) {
    $scope.cancel = function () {
      $scope.wrapper = null;
      $scope.documentData = null;
      return $state.go('applications/edit/case');
    };

    $scope.addPart = function () {
      return ApplicationDocFile
        .save({ id: $stateParams.id }, {
          personId: $scope.application.person.id,
          currentDocId: $scope.documentData.currentDocId,
          file: $scope.wrapper.applicationDocFile.file,
          setPartId: $scope.documentData.docPartType.content.setPartId,
          part: $scope.wrapper.applicationDocFile.part
        }).$promise.then(function () {
          $scope.wrapper = null;
          $scope.documentData = null;
          return $state.transitionTo('applications/edit/case', $stateParams, { reload: true });
        });
    };

    $scope.linkNew = function () {
      return ApplicationDocFile
        .linkNew({
          id: $stateParams.id,
          setPartId: $scope.documentData.docPartType.content.setPartId
        }, {
          personId: $scope.application.person.id,
          currentDocId: $scope.documentData.currentDocId,
          part: $scope.wrapper.applicationDocFile.part,
          docFileId: $scope.documentData.docFileId
        }).$promise.then(function () {
          $scope.wrapper = null;
          $scope.documentData = null;
          return $state.transitionTo('applications/edit/case', $stateParams, { reload: true });
        });
    };
  }

  ApplicationsEditAddPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'ApplicationDocFile'
  ];

  angular.module('gva').controller('ApplicationsEditAddPartCtrl', ApplicationsEditAddPartCtrl);
}(angular
));
