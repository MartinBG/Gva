/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AppExamSystCtrl(
    $scope,
    $stateParams,
    Applications,
    application,
    qualifications) {
    var originalApplication = _.cloneDeep(application);
    $scope.application = application;
    $scope.qualifications = qualifications;
    $scope.lotId = $stateParams.lotId;
    $scope.ind = $stateParams.ind;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editAppExSystDataForm.$validate().then(function () {
        if ($scope.editAppExSystDataForm.$valid) {
          return Applications.editAppPart({
            lotId: $scope.lotId,
            ind: $scope.ind
          },
          $scope.application).$promise.then(function () {
            $scope.editMode = null;
          });
        }
      });
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.application = _.cloneDeep(originalApplication);
    };
  }

  AppExamSystCtrl.$inject = [
    '$scope',
    '$stateParams',
    'Applications',
    'application',
    'qualifications'
  ];

  AppExamSystCtrl.$resolve = {
    application: [
      '$stateParams',
      'Applications',
      function ($stateParams, Applications) {
        return Applications.getAppPart({
          lotId: $stateParams.lotId,
          ind: $stateParams.ind,
          id: $stateParams.id
        }).$promise;
      }
    ],
    qualifications: [
      '$stateParams',
      'Applications',
      function ($stateParams, Applications) {
        return Applications.getAppQualifications({
          lotId: $stateParams.lotId,
          ind: $stateParams.ind,
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AppExamSystCtrl', AppExamSystCtrl);
}(angular, _));
