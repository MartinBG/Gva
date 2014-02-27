﻿/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function EndStageCtrl(
    $scope,
    $state,
    DocStage,
    doc,
    stageModel
  ) {
    $scope.stageModel = stageModel;

    $scope.cancel = function () {
      return $state.go('^');
    };

    $scope.save = function () {
      $scope.stageForm.$validate().then(function () {
        if ($scope.stageForm.$valid) {

          var stageData = {
            docElectronicServiceStageId: $scope.stageModel.docElectronicServiceStageId,
            endingDate: $scope.stageModel.endingDate
          };

          return DocStage.end({ docId: stageModel.docId }, stageData).$promise
            .then(function (result) {
            doc.docElectronicServiceStages = result.stages;
            return $state.go('^');
          });
        }
      });
    };

    $scope.isEndingDateValid = function () {
      if (!$scope.stageModel.endingDate) {
        return true;
      }
      else {
        return moment($scope.stageModel.endingDate) > moment($scope.stageModel.startingDate);
      }
    };
  }

  EndStageCtrl.$inject = [
    '$scope',
    '$state',
    'DocStage',
    'doc',
    'stageModel'
  ];

  EndStageCtrl.$resolve = {
    stageModel: ['Nomenclature', 'doc', 'DocStage',
      function (Nomenclature, doc, DocStage) {
        return DocStage.current({ docId: doc.docId}).$promise.then(function (result) {
          return {
            docElectronicServiceStageId: result.docElectronicServiceStageId,
            stageId: result.electronicServiceStageId,
            docId: doc.docId,
            docTypeId: doc.docTypeId,
            startingDate: result.startingDate,
            executors: result.electronicServiceStageExecutors,
            expectedEndingDate: result.expectedEndingDate,
            endingDate: result.endingDate
          };
        });
      }
    ]
  };

  angular.module('ems').controller('EndStageCtrl', EndStageCtrl);
}(angular, moment));
