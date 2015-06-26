/*global angular,_*/
(function (angular) {
  'use strict';

  function AppEditPartCtrl(
    $scope,
    $state,
    $stateParams,
    scMessage,
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

    $scope.deleteApp = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return Applications
            .remove({ id: $stateParams.id })
            .$promise.then(function () {
              if (application.lotSetAlias === 'person') {
                return $state.go(
                  'root.persons.view.documentApplications.search', { id: application.lotId });
              }
              else if (application.lotSetAlias === 'organization') {
                return $state.go(
                  'root.organizations.view.documentApplications.search', { id: application.lotId });
              }
              else if (application.lotSetAlias === 'aircraft') {
                return $state.go(
                  'root.aircrafts.view.documentApplications.search', { id: application.lotId });
              }
              else if (application.lotSetAlias === 'airport') {
                return $state.go(
                  'root.airports.view.documentApplications.search', { id: application.lotId });
              }
              else if (application.lotSetAlias === 'equipment') {
                return $state.go(
                  'root.equipments.view.documentApplications.search', { id: application.lotId });
              }
            });
        }
      });
    };
  }

  AppEditPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'scMessage',
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
