/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentApplicationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentApplication,
    equipmentDocumentApplication
  ) {

    $scope.equipmentDocumentApplication = equipmentDocumentApplication;

    $scope.save = function () {
      return $scope.newDocumentApplicationForm.$validate()
        .then(function () {
          if ($scope.newDocumentApplicationForm.$valid) {
            return EquipmentDocumentApplication
              .save({ id: $stateParams.id }, $scope.equipmentDocumentApplication).$promise
              .then(function () {
                return $state.go('root.equipments.view.applications.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.equipments.view.applications.search');
    };
  }

  EquipmentApplicationsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentDocumentApplication',
    'equipmentDocumentApplication'
  ];
  EquipmentApplicationsNewCtrl.$resolve = {
    equipmentDocumentApplication: function () {
      return {
        part: {},
        files: {
          hideApplications: true,
          files: []
        }
      };
    }
  };

  angular.module('gva').controller('EquipmentApplicationsNewCtrl', EquipmentApplicationsNewCtrl);
}(angular));
