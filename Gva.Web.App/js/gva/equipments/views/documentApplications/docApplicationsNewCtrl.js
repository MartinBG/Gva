/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentApplicationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentApplications,
    equipmentDocumentApplication
  ) {
    $scope.equipmentDocumentApplication = equipmentDocumentApplication;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newDocumentApplicationForm.$validate()
        .then(function () {
          if ($scope.newDocumentApplicationForm.$valid) {
            return EquipmentDocumentApplications
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
    'EquipmentDocumentApplications',
    'equipmentDocumentApplication'
  ];
  EquipmentApplicationsNewCtrl.$resolve = {
    equipmentDocumentApplication: [
      '$stateParams',
      'EquipmentDocumentApplications',
      function ($stateParams, EquipmentDocumentApplications) {
        return EquipmentDocumentApplications.newApplication({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentApplicationsNewCtrl', EquipmentApplicationsNewCtrl);
}(angular));
