﻿/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function EditStageCtrl(
    $scope,
    $state,
    $stateParams,
    DocStages,
    doc,
    stageModel
  ) {
    $scope.stageModel = stageModel;

    $scope.save = function () {
      return $scope.stageForm.$validate().then(function () {
        if ($scope.stageForm.$valid) {
          return DocStages
            .edit({
              id: $scope.stageModel.docId,
              docVersion: $scope.stageModel.docVersion
            }, $scope.stageModel)
            .$promise
            .then(function () {
              return $state.transitionTo($state.$current.parent, $stateParams, { reload: true });
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
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

  EditStageCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'DocStages',
    'doc',
    'stageModel'
  ];

  EditStageCtrl.$resolve = {
    stageModel: ['doc', 'DocStages',
      function (doc, DocStages) {
        return DocStages.get({
          id: doc.docId,
          docVersion: doc.version
        })
          .$promise
          .then(function (result) {
            result.docVersion = doc.version;
            result.docTypeId = doc.docTypeId;
            return result;
          });
      }
    ]
  };

  angular.module('ems').controller('EditStageCtrl', EditStageCtrl);
}(angular, moment));
