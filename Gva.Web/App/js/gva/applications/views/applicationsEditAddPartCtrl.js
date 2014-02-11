/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditAddPartCtrl(
    $scope,
    $state,
    $stateParams,
    Application
    ) {
    $scope.cancel = function () {
      return $state.transitionTo('root.applications.edit.case', $stateParams, { reload: true });
    };

    $scope.addPart = function () {
      $scope.addFormWrapper.$validate()
        .then(function () {
          if ($scope.addFormWrapper.$valid) {
            return Application
              .partsNew({ id: $stateParams.id }, {
                docId: $scope.documentData.currentDocId,
                files: $scope.wrapper.applicationDocPart.file,
                setPartAlias: $scope.documentData.docPartType.alias,
                part: $scope.wrapper.applicationDocPart.part
              }).$promise.then(function () {
                return $state.transitionTo('root.applications.edit.case',
                  $stateParams, { reload: true });
              });
          }
        });
    };

    $scope.linkNew = function () {
      $scope.addFormWrapper.$validate()
        .then(function () {
          if ($scope.addFormWrapper.$valid) {
            return Application
              .partsLinkNew({ id: $stateParams.id }, {
                docFileKey: $scope.documentData.docFiles[0].key,
                setPartAlias: $scope.documentData.docPartType.alias,
                part: $scope.wrapper.applicationDocPart.part
              }).$promise.then(function () {
                return $state.transitionTo('root.applications.edit.case',
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
    'Application'
  ];

  angular.module('gva').controller('ApplicationsEditAddPartCtrl', ApplicationsEditAddPartCtrl);
}(angular
));
