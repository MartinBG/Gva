/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentApplicationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentApplication,
    equipmentDocumentApplication) {
    var originalApplication = _.cloneDeep(equipmentDocumentApplication);

    $scope.equipmentDocumentApplication = equipmentDocumentApplication;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.equipmentDocumentApplication.part = _.cloneDeep(originalApplication.part);
      $scope.$broadcast('cancel', originalApplication);
    };

    $scope.save = function () {
      return $scope.editDocumentApplicationForm.$validate()
        .then(function () {
          if ($scope.editDocumentApplicationForm.$valid) {
            return EquipmentDocumentApplication
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.equipmentDocumentApplication)
              .$promise
              .then(function () {
                return $state.go('root.equipments.view.applications.search');
              });
          }
        });
    };

    $scope.deleteApplication = function () {
      return EquipmentDocumentApplication.remove({
        id: $stateParams.id,
        ind: equipmentDocumentApplication.partIndex
      }).$promise.then(function () {
        return $state.go('root.equipments.view.applications.search');
      });
    };
  }

  EquipmentApplicationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentDocumentApplication',
    'equipmentDocumentApplication'
  ];

  EquipmentApplicationsEditCtrl.$resolve = {
    equipmentDocumentApplication: [
      '$stateParams',
      'EquipmentDocumentApplication',
      function ($stateParams, EquipmentDocumentApplication) {
        return EquipmentDocumentApplication.get($stateParams).$promise
            .then(function (application) {
          application.files = {
            hideApplications: true,
            files: application.files
          };

          return application;
        });
      }
    ]
  };

  angular.module('gva').controller('EquipmentApplicationsEditCtrl', EquipmentApplicationsEditCtrl);
}(angular, _));