/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOthersNewCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentOther,
    equipmentDocumentOther
  ) {
    $scope.save = function () {
      return $scope.newDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.newDocumentOtherForm.$valid) {
            return EquipmentDocumentOther
              .save({ id: $stateParams.id }, $scope.equipmentDocumentOther).$promise
              .then(function () {
                return $state.go('root.equipments.view.others.search');
              });
          }
        });
    };

    $scope.equipmentDocumentOther = equipmentDocumentOther;

    $scope.cancel = function () {
      return $state.go('root.equipments.view.others.search');
    };
  }

  EquipmentOthersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentDocumentOther',
    'equipmentDocumentOther'
  ];

  EquipmentOthersNewCtrl.$resolve = {
    equipmentDocumentOther: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('EquipmentOthersNewCtrl', EquipmentOthersNewCtrl);
}(angular));