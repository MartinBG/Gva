/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditAddPartCtrl(
    $scope,
    $state,
    $stateParams,
    Application,
    applicationDocPart,
    selectedPublisher
    ) {
    $scope.applicationDocPart = applicationDocPart;
    $scope.applicationDocPart.part.documentPublisher = selectedPublisher.pop() ||
      applicationDocPart.part.documentPublisher;

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
                file: $scope.applicationDocPart.docFile,
                setPartAlias: $stateParams.docPartTypeAlias,
                part: $scope.applicationDocPart.part
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
                part: $scope.applicationDocPart.part
              }).$promise.then(function () {
                return $state.transitionTo('root.applications.edit.case',
                  $stateParams, { reload: true });
              });
          }
        });
    };

    $scope.choosePublisher = function () {
      return $state.go('root.applications.edit.case.addPart.choosePublisher');
    };
  }

  ApplicationsEditAddPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Application',
    'applicationDocPart',
    'selectedPublisher'
  ];

  ApplicationsEditAddPartCtrl.$resolve = {
    applicationDocPart: function () {
      return {
        part: {}
      };
    },
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('ApplicationsEditAddPartCtrl', ApplicationsEditAddPartCtrl);
}(angular
));
