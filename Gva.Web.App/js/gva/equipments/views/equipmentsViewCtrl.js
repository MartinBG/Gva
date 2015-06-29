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
      'ApplicationNoms',
      function ResolveApplication($stateParams, ApplicationNoms) {
        if ($stateParams.appId) {
          return ApplicationNoms
            .get({ lotId: $stateParams.id, appId: $stateParams.appId })
            .$promise;
        }

        return null;
      }
    ]
  };

  angular.module('gva').controller('EquipmentsViewCtrl', EquipmentsViewCtrl);
}(angular));
