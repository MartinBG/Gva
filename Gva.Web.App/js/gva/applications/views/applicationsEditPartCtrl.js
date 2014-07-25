/*global angular,_*/
(function (angular) {
  'use strict';

  function AppEditPartCtrl(
    $scope,
    $state,
    $stateParams,
    Applications,
    application) {
    var originalApplication = _.cloneDeep(application);

    $scope.application = application;
    $scope.editMode = null;
    $scope.lotId = $stateParams.lotId;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.application = _.cloneDeep(originalApplication);
    };

    $scope.save = function () {
      return $scope.editDocumentApplicationForm.$validate().then(function () {
          if ($scope.editDocumentApplicationForm.$valid) {
            return Applications.editAppPart({
              path: $stateParams.setPartPath,
              lotId: $stateParams.lotId,
              ind: $stateParams.ind
            },
            $scope.application).$promise.then(function () {
              $state.go('root.applications.edit.case', { id: $stateParams.appId });
            });
          }
        });
    };

    $scope.done = function () {
      $state.go('root.applications.edit.case', { id: $stateParams.appId });
    };
  }

  AppEditPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Applications',
    'application'
  ];

  AppEditPartCtrl.$resolve = {
    application: [
      '$stateParams',
      'Applications',
      function ($stateParams, Applications) {
        return Applications.getAppPart({
          path: $stateParams.setPartPath,
          lotId: $stateParams.lotId,
          ind: $stateParams.ind
        })
          .$promise.then(function (application) {
          application.files = {
            hideApplications: true,
            files: application.files
          };

          return application;
        });
      }
    ]
  };

  angular.module('gva').controller('AppEditPartCtrl', AppEditPartCtrl);
}(angular));
