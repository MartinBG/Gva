/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditAddPartCtrl(
    $scope,
    $state,
    $stateParams,
    Application
    ) {
    $scope.isLinkNew = $stateParams.isLinkNew;
    $scope.currentDocId = $stateParams.currentDocId;
    $scope.docPartTypeAlias = $stateParams.docPartTypeAlias;
    $scope.docFile = {
      key: $stateParams.docFileKey,
      name: $stateParams.docFileName,
      relativePath: null
    };

    $scope.cancel = function () {
      return $state.transitionTo('root.applications.edit.case', $stateParams, { reload: true });
    };

    $scope.addPart = function () {
      $scope.addFormWrapper.$validate()
        .then(function () {
          if ($scope.addFormWrapper.$valid) {
            return Application
              .partsNew({ id: $stateParams.id }, {
                docId: $stateParams.currentDocId,
                file: $scope.wrapper.applicationDocPart.docFile,
                setPartAlias: $stateParams.docPartTypeAlias,
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
                docFileKey: $stateParams.docFileKey,
                setPartAlias: $stateParams.docPartTypeAlias,
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
