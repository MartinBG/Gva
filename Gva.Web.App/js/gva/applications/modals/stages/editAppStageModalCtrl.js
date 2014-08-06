/*global angular*/
(function (angular) {
  'use strict';

  function EditAppStageModalCtrl(
    $scope,
    $modalInstance,
    AppStages,
    scModalParams,
    stageModel
  ) {
    $scope.form = {};
    $scope.stageModel = stageModel;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.save = function () {
      return $scope.form.stageForm.$validate()
        .then(function () {
          if ($scope.form.stageForm.$valid) {
            return AppStages
              .save({ id: scModalParams.appId, ind: $scope.stageModel.id }, $scope.stageModel)
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

    $scope.deleteStage = function () {
      return AppStages.remove({ id: scModalParams.appId, ind: $scope.stageModel.id })
          .$promise.then(function () {
            return $modalInstance.close();
          });
    };

  }

  EditAppStageModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'AppStages',
    'scModalParams',
    'stageModel'
  ];

  EditAppStageModalCtrl.$resolve = {
    stageModel: [
      'AppStages',
      'scModalParams',
      function (AppStages, scModalParams) {
        return AppStages.get({
          id: scModalParams.appId,
          ind: scModalParams.stageId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EditAppStageModalCtrl', EditAppStageModalCtrl);
}(angular));
