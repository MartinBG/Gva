/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentsViewCtrl(
    $scope,
    $state,
    $stateParams,
    equipment,
    application
  ) {
    $scope.equipment = equipment;
    $scope.application = application;
    $scope.set = $stateParams.set;
    $scope.lotId = $stateParams.id;

    $scope.edit = function () {
      return $state.go('root.equipments.view.edit');
    };
  }

  EquipmentsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'equipment',
    'application'
  ];

  EquipmentsViewCtrl.$resolve = {
    equipment: [
      '$stateParams',
      'Equipments',
      function ($stateParams, Equipments) {
        return Equipments.get({ id: $stateParams.id }).$promise;
      }
    ],
    application: [
      '$stateParams',
      'EquipmentApplications',
      function ResolveApplication($stateParams, EquipmentApplications) {
        if (!!$stateParams.appId) {
          return EquipmentApplications.get($stateParams).$promise
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
