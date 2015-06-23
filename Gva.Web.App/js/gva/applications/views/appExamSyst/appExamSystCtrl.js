/*global angular, _*/
(function (angular, _) {
  'use strict';

  function AppExamSystCtrl(
    $scope,
    $state,
    $stateParams,
    Applications,
    application,
    applicationPart,
    qualifications) {
    var originalApplicationPart = _.cloneDeep(applicationPart);
    $scope.applicationPart = applicationPart;
    $scope.qualifications = qualifications;
    $scope.lotId = application.lotId;
    $scope.ind = application.partIndex;
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
          $scope.applicationPart).$promise
            .then(function () {
              $scope.editMode = null;
              $state.transitionTo('root.applications.edit.data', $stateParams);
            });
        }
      });
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.applicationPart = _.cloneDeep(originalApplicationPart);
    };
  }

  AppExamSystCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Applications',
    'application',
    'applicationPart',
    'qualifications'
  ];

  AppExamSystCtrl.$resolve = {
    applicationPart: [
      '$stateParams',
      'Applications',
      'application',
      function ($stateParams, Applications, application) {
        return Applications.getAppPart({
          lotId: application.lotId,
          ind: application.partIndex,
          id: $stateParams.id
        }).$promise;
      }
    ],
    qualifications: [
      '$stateParams',
      'Applications',
      'application',
      function ($stateParams, Applications, application) {
        return Applications.getAppQualifications({
          lotId: application.lotId,
          ind: application.partIndex,
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AppExamSystCtrl', AppExamSystCtrl);
}(angular, _));
