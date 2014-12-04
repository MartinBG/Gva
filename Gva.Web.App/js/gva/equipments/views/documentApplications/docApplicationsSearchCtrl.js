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
    $scope.lotId = $stateParams.id;

    $scope.isDeclinedApp = function(item) {
      if (item.part.stage) {
        return item.part.stage.alias === 'declined';
      }

      return false;
    };

    $scope.isDoneApp = function(item) {
      if (item.part.stage) {
        return item.part.stage.alias === 'done';
      }

      return false;
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
