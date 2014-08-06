/*global angular*/
(function (angular) {
  'use strict';

  function NewAppStageModalCtrl(
    $scope,
    $modalInstance,
    AppStages,
    scModalParams,
    stageModel
  ) {
    $scope.form = {};
    $scope.stageModel = stageModel;

    $scope.save = function () {
      return $scope.form.stageForm.$validate()
        .then(function () {
          if ($scope.form.stageForm.$valid) {
            return AppStages
              .save({ id: scModalParams.appId }, $scope.stageModel)
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
    'scModalParams',
    'stageModel'
  ];

  NewAppStageModalCtrl.$resolve = {
    stageModel: [
      'scModalParams',
      function (scModalParams) {
        return {
          ordinal: scModalParams.ordinal
        };
      }
    ]
  };
  angular.module('gva').controller('NewAppStageModalCtrl', NewAppStageModalCtrl);
}(angular));
