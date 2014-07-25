﻿/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function NextDocStageModalCtrl(
    $scope,
    $modalInstance,
    DocStages,
    stageModel
  ) {
    $scope.form = {};
    $scope.stageModel = stageModel;

    $scope.save = function () {
      return $scope.form.stageForm.$validate().then(function () {
        if ($scope.form.stageForm.$valid) {
          return DocStages
            .save({
              id: $scope.stageModel.docId,
              docVersion: $scope.stageModel.docVersion
            }, $scope.stageModel)
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

    $scope.isEndingDateValid = function () {
      if (!$scope.stageModel.endingDate) {
        return true;
      }
      else {
        var momentED = moment($scope.stageModel.endingDate).startOf('day'),
          momentSD = moment($scope.stageModel.startingDate).startOf('day'),
          today = moment().startOf('day');

        return !momentSD.isAfter(momentED) && !today.isAfter(momentED);
      }
    };
  }

  NextDocStageModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'DocStages',
    'stageModel'
  ];

  NextDocStageModalCtrl.$resolve = {
    stageModel: function () {
      return {};
    }
  };

  angular.module('gva').controller('NextDocStageModalCtrl', NextDocStageModalCtrl);
}(angular, moment));
