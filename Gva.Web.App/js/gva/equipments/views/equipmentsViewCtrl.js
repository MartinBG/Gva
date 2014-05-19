/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentsViewCtrl(
    $scope,
    $state,
    $stateParams,
    Equipment,
    equipment,
    application
  ) {
    $scope.equipment = equipment;
    $scope.application = application;

    $scope.edit = function () {
      return $state.go('root.equipments.view.edit');
    };

    $scope.viewApplication = function (appId) {
      return $state.go('root.applications.edit.case', { id: appId });
    };
  }

  EquipmentsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Equipment',
    'equipment',
    'application'
  ];

  EquipmentsViewCtrl.$resolve = {
    equipment: [
      '$stateParams',
      'Equipment',
      function ($stateParams, Equipment) {
        return Equipment.get({ id: $stateParams.id }).$promise;
      }
    ],
    application: [
      '$stateParams',
      'EquipmentApplication',
      function ResolveApplication($stateParams, EquipmentApplication) {
        if (!!$stateParams.appId) {
          return EquipmentApplication.get($stateParams).$promise
            .then(function (result) {
              if (result.applicationId) {
                return result;
              }

              return null;
            });
        }

        return null;
      }
    ]
  };

  angular.module('gva').controller('EquipmentsViewCtrl', EquipmentsViewCtrl);
}(angular));
