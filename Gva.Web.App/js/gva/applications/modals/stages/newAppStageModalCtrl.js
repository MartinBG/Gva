/*global angular*/
(function (angular) {
  'use strict';

  function NewAppStageModalCtrl(
    $scope,
    $stateParams,
    $modalInstance,
    AppStages,
    stageModel,
    ordinal
  ) {
    $scope.form = {};
    $scope.stageModel = stageModel;
    $scope.stageModel.ordinal = ordinal;

    $scope.save = function () {
      return $scope.form.stageForm.$validate()
        .then(function () {
          if ($scope.form.stageForm.$valid) {
            return AppStages
              .save({ id: $stateParams.id }, $scope.stageModel)
              .$promise
              .then(function () {
                return $modalInstance.close();
              });
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

  }

  NewAppStageModalCtrl.$inject = [
    '$scope',
    '$stateParams',
    '$modalInstance',
    'AppStages',
    'stageModel',
    'ordinal'
  ];

  NewAppStageModalCtrl.$resolve = {
    stageModel: function () {
      return {};
    }
  };
  angular.module('gva').controller('NewAppStageModalCtrl', NewAppStageModalCtrl);
}(angular));
