/*global angular*/
(function (angular) {
  'use strict';

  function EquipmentOthersSearchCtrl(
    $scope,
    $state,
    $stateParams,
    EquipmentDocumentOther,
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

    $scope.deleteDocumentOther = function (documentOther) {
      return EquipmentDocumentOther.remove({ id: $stateParams.id, ind: documentOther.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
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
    'EquipmentDocumentOther',
    'documentOthers'
  ];

  EquipmentOthersSearchCtrl.$resolve = {
    documentOthers: [
      '$stateParams',
      'EquipmentDocumentOther',
      function ($stateParams, EquipmentDocumentOther) {
        return EquipmentDocumentOther.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('EquipmentOthersSearchCtrl', EquipmentOthersSearchCtrl);
}(angular));
