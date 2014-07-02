/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOthersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    documentOthers
  ) {
    $scope.documentOthers = documentOthers;

    $scope.editDocumentOther = function (documentOther) {
      return $state.go('root.equipments.view.others.edit',
        {
          id: $stateParams.id,
          ind: documentOther.partIndex
        });
    };

    $scope.newDocumentOther = function () {
      return $state.go('root.equipments.view.others.new');
    };
  }

  EquipmentOthersSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'documentOthers'
  ];

  EquipmentOthersSearchCtrl.$resolve = {
    documentOthers: [
      '$stateParams',
      'EquipmentDocumentOthers',
      function ($stateParams, EquipmentDocumentOthers) {
        return EquipmentDocumentOthers.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOthersSearchCtrl', EquipmentOthersSearchCtrl);
}(angular));
