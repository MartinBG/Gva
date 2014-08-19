/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOthersNewCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentOthers,
    equipmentDocumentOther
  ) {
    $scope.equipmentDocumentOther = equipmentDocumentOther;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.newDocumentOtherForm.$valid) {
            return EquipmentDocumentOthers
              .save({ id: $stateParams.id }, $scope.equipmentDocumentOther).$promise
              .then(function () {
                return $state.go('root.equipments.view.others.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.equipments.view.others.search');
    };
  }

  EquipmentOthersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentDocumentOthers',
    'equipmentDocumentOther'
  ];

  EquipmentOthersNewCtrl.$resolve = {
    equipmentDocumentOther: [
      '$stateParams',
      'EquipmentDocumentOthers',
      function ($stateParams, EquipmentDocumentOthers) {
        return EquipmentDocumentOthers.newDocument({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOthersNewCtrl', EquipmentOthersNewCtrl);
}(angular));
