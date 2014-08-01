/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentsInspectionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentInspections,
    equipmentInspection) {
    $scope.equipmentInspection = equipmentInspection;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newInspectionForm.$validate()
      .then(function () {
        if ($scope.newInspectionForm.$valid) {
          return EquipmentInspections
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
    'EquipmentInspections',
    'equipmentInspection'
  ];

  EquipmentsInspectionsNewCtrl.$resolve = {
    equipmentInspection: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {
              examiners: [],
              auditDetails: [],
              disparities: []
            },
            applications: [application]
          };
        }
        else {
          return {
            part: {
              examiners: [],
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
