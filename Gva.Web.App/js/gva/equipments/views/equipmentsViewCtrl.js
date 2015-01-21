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

    $scope.edit = function () {
      return $state.go('root.equipments.view.edit');
    };

    $scope.viewApplication = function (applicationId, partIndex) {
      return $state.go('root.applications.edit.data', {
        id: applicationId,
        set: $stateParams.set,
        lotId: $stateParams.id,
        ind: partIndex
      });
    };

    $scope.exitApplication = function () {
      delete $state.params.appId;
      $state.transitionTo($state.current, $state.params, { reload: true });
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
