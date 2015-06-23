/*global angular,_*/
(function (angular) {
  'use strict';

  function AppEditPartCtrl(
    $scope,
    $state,
    $stateParams,
    Applications,
    application,
    applicationPart) {
    var originalApplicationPart = _.cloneDeep(applicationPart);

    $scope.applicationPart = applicationPart;
    $scope.editMode = null;
    $scope.lotId = application.lotId;
    $scope.set = application.lotSetAlias;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.applicationPart = _.cloneDeep(originalApplicationPart);
    };

    $scope.save = function () {
      return $scope.editDocumentApplicationForm.$validate().then(function () {
        if ($scope.editDocumentApplicationForm.$valid) {
          return Applications.editAppPart({
            lotId: application.lotId,
            ind: application.partIndex
          },
          $scope.applicationPart).$promise.then(function () {
            $scope.editMode = null;
          });
        }
      });
    };
  }

  AppEditPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Applications',
    'application',
    'applicationPart'
  ];

  AppEditPartCtrl.$resolve = {
    applicationPart: [
      'Applications',
      'application',
      function (Applications, application) {
        return Applications.getAppPart({
          lotId: application.lotId,
          ind: application.partIndex
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AppEditPartCtrl', AppEditPartCtrl);
}(angular));
