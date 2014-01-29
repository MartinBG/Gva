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
      return $state.go('applications/edit/case');
    };

    $scope.addPart = function () {
      return ApplicationDocFile
        .save({ id: $stateParams.id }, {
          personId: $scope.application.person.id,
          currentDocId: $scope.$parent.currentDocId,
          file: $scope.wrapper.applicationDocFile.file,
          fileType: $scope.docFileType,
          part: $scope.wrapper.applicationDocFile.part
        }
        ).$promise.then(function () {
          $scope.wrapper = null;
          return $state.transitionTo('applications/edit/case', $stateParams, { reload: true });
        });
    };

    $scope.linkNew = function () {
      return ApplicationDocFile
        .linkNew({ id: $stateParams.id, docFileId: $scope.$parent.docFileId }, {
          personId: $scope.application.person.id,
          currentDocId: $scope.$parent.currentDocId,
          part: $scope.wrapper.applicationDocFile.part
        }
        ).$promise.then(function () {
          $scope.wrapper = null;
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
