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
