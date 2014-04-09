/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentOther,
    equipmentDocumentOther
  ) {
    var originalDoc = _.cloneDeep(equipmentDocumentOther);

    $scope.equipmentDocumentOther = equipmentDocumentOther;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.editDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.editDocumentOtherForm.$valid) {
            return EquipmentDocumentOther
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.equipmentDocumentOther)
              .$promise
              .then(function () {
                return $state.go('root.equipments.view.others.search');
              });
          }
        });
    };
    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.equipmentDocumentOther.part = _.cloneDeep(originalDoc.part);
    };
    
    $scope.deleteOther = function () {
      return EquipmentDocumentOther.remove({
        id: $stateParams.id,
        ind: equipmentDocumentOther.partIndex
      }).$promise.then(function () {
        return $state.go('root.equipments.view.others.search');
      });
    };
  }

  EquipmentOthersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentDocumentOther',
    'equipmentDocumentOther'
  ];

  EquipmentOthersEditCtrl.$resolve = {
    equipmentDocumentOther: [
      '$stateParams',
      'EquipmentDocumentOther',
      function ($stateParams, EquipmentDocumentOther) {
        return EquipmentDocumentOther.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOthersEditCtrl', EquipmentOthersEditCtrl);
}(angular, _));