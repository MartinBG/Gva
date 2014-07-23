/*global angular*/
(function (angular) {
  'use strict';

  function EditAppStageModalCtrl(
    $scope,
    $stateParams,
    $modalInstance,
    AppStages,
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
              .save({ id: $stateParams.id, ind: $scope.stageModel.id }, $scope.stageModel)
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
      return AppStages.remove({ id: $stateParams.id, ind: $scope.stageModel.id })
          .$promise.then(function () {
            return $modalInstance.close();
          });
    };

  }

  EditAppStageModalCtrl.$inject = [
    '$scope',
    '$stateParams',
    '$modalInstance',
    'AppStages',
    'stageModel'
  ];

  angular.module('gva').controller('EditAppStageModalCtrl', EditAppStageModalCtrl);
}(angular));
