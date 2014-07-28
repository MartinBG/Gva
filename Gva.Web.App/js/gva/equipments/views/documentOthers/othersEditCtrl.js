/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EquipmentOthersEditCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentOthers,
    equipmentDocumentOther,
    scMessage
  ) {
    var originalDoc = _.cloneDeep(equipmentDocumentOther);

    $scope.equipmentDocumentOther = equipmentDocumentOther;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.equipmentDocumentOther = _.cloneDeep(originalDoc);
    };

    $scope.save = function () {
      return $scope.editDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.editDocumentOtherForm.$valid) {
            return EquipmentDocumentOthers
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.equipmentDocumentOther)
              .$promise
              .then(function () {
                return $state.go('root.equipments.view.others.search');
              });
          }
        });
    };

    $scope.deleteOther = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return EquipmentDocumentOthers.remove({
            id: $stateParams.id,
            ind: equipmentDocumentOther.partIndex
          }).$promise.then(function () {
            return $state.go('root.equipments.view.others.search');
          });
        }
      });
    };
  }

  EquipmentOthersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'EquipmentDocumentOthers',
    'equipmentDocumentOther',
    'scMessage'
  ];

  EquipmentOthersEditCtrl.$resolve = {
    equipmentDocumentOther: [
      '$stateParams',
      'EquipmentDocumentOthers',
      function ($stateParams, EquipmentDocumentOthers) {
        return EquipmentDocumentOthers.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOthersEditCtrl', EquipmentOthersEditCtrl);
}(angular, _));
