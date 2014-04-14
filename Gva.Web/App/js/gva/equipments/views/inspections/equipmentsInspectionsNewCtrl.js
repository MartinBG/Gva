/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentsInspectionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentInspection,
    equipmentInspection) {
    $scope.equipmentInspection = equipmentInspection;

    $scope.save = function () {
      return $scope.newInspectionForm.$validate()
      .then(function () {
        if ($scope.newInspectionForm.$valid) {
          return EquipmentInspection
            .save({ id: $stateParams.id }, $scope.equipmentInspection)
            .$promise
            .then(function () {
              return $state.go('root.equipments.view.inspections.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.equipments.view.inspections.search');
    };
  }

  EquipmentsInspectionsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentInspection',
    'equipmentInspection'
  ];

  EquipmentsInspectionsNewCtrl.$resolve = {
    equipmentInspection: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {
              examiners: [{ sortOrder: 1 }],
              auditDetails: [],
              disparities: []
            },
            applications: [application]
          };
        }
        else {
          return {
            part: {
              examiners: [{ sortOrder: 1 }],
              auditDetails: [],
              disparities: []
            },
            files: []
          };
        }
      }
    ]
  };

  angular.module('gva').controller('EquipmentsInspectionsNewCtrl', EquipmentsInspectionsNewCtrl);
}(angular));
