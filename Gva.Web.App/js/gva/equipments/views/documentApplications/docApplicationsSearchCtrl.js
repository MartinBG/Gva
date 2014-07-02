/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentApplicationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    equipmentDocumentApplications
  ) {
    $scope.equipmentDocumentApplications = equipmentDocumentApplications;


    $scope.editApplication = function (application) {
      return $state.go('root.equipments.view.applications.edit', {
        id: $stateParams.id,
        ind: application.partIndex
      });
    };

    $scope.newApplication = function () {
      return $state.go('root.equipments.view.applications.new');
    };
  }

  EquipmentApplicationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'equipmentDocumentApplications'
  ];

  EquipmentApplicationsSearchCtrl.$resolve = {
    equipmentDocumentApplications: [
      '$stateParams',
      'EquipmentDocumentApplications',
      function ($stateParams, EquipmentDocumentApplications) {
        return EquipmentDocumentApplications.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller(
    'EquipmentApplicationsSearchCtrl',
    EquipmentApplicationsSearchCtrl
  );
}(angular));
