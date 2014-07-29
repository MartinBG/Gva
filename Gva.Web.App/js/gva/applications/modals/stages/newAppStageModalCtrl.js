/*global angular*/
(function (angular) {
  'use strict';

  function NewAppStageModalCtrl(
    $scope,
    $modalInstance,
    AppStages,
    stageModel,
    ordinal,
    appId
  ) {
    $scope.form = {};
    $scope.stageModel = stageModel;
    $scope.stageModel.ordinal = ordinal;

    $scope.save = function () {
      return $scope.form.stageForm.$validate()
        .then(function () {
          if ($scope.form.stageForm.$valid) {
            return AppStages
              .save({ id: appId }, $scope.stageModel)
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
    '$modalInstance',
    'AppStages',
    'stageModel',
    'ordinal',
    'appId'
  ];

  NewAppStageModalCtrl.$resolve = {
    stageModel: function () {
      return {};
    }
  };
  angular.module('gva').controller('NewAppStageModalCtrl', NewAppStageModalCtrl);
}(angular));
