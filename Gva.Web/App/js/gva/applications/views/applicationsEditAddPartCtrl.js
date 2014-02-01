/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditAddPartCtrl(
    $scope,
    $state,
    $stateParams,
    ApplicationDocPart
    ) {
    $scope.cancel = function () {
      return $state.transitionTo('applications/edit/case', $stateParams, { reload: true });
    };

    $scope.addPart = function () {
      $scope.addFormWrapper.$validate()
        .then(function () {
          if ($scope.addFormWrapper.$valid) {
            return ApplicationDocPart
              .save({ id: $stateParams.id }, {
                personId: $scope.application.lotId,
                currentDocId: $scope.documentData.currentDocId,
                files: $scope.wrapper.applicationDocPart.file,
                setPartId: $scope.documentData.docPartType.content.setPartId,
                part: $scope.wrapper.applicationDocPart.part
              }).$promise.then(function () {
                return $state.transitionTo('applications/edit/case',
                  $stateParams, { reload: true });
              });
          }
        });
    };

    $scope.linkNew = function () {
      $scope.addFormWrapper.$validate()
        .then(function () {
          if ($scope.addFormWrapper.$valid) {
            return ApplicationDocPart
              .linkNew({
                id: $stateParams.id,
                setPartId: $scope.documentData.docPartType.content.setPartId
              }, {
                personId: $scope.application.lotId,
                currentDocId: $scope.documentData.currentDocId,
                part: $scope.wrapper.applicationDocPart.part,
                docFileId: $scope.documentData.docFiles[0].key
              }).$promise.then(function () {
                return $state.transitionTo('applications/edit/case',
                  $stateParams, { reload: true });
              });
          }
        });
    };
  }

  ApplicationsEditAddPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'ApplicationDocPart'
  ];

  angular.module('gva').controller('ApplicationsEditAddPartCtrl', ApplicationsEditAddPartCtrl);
}(angular
));
