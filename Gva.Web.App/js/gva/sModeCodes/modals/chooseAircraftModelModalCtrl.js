/*global angular*/
(function (angular) {
  'use strict';

  function ChooseAircraftModelModalCtrl(
    $scope,
    $modalInstance,
    models
  ) {
    $scope.models = models;

    $scope.selectModel = function (model) {
      return $modalInstance.close(model.name || model.nameAlt);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  ChooseAircraftModelModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'models'
  ];

  ChooseAircraftModelModalCtrl.$resolve = {
    models: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.query({
          alias: 'aircraftModels'
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseAircraftModelModalCtrl', ChooseAircraftModelModalCtrl);
}(angular));
