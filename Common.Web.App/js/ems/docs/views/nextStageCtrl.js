﻿/*global angular, moment, _*/
(function (angular, moment, _) {
  'use strict';

  function NextStageCtrl(
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
            .save({
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

  NextStageCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'DocStages',
    'doc',
    'stageModel'
  ];

  NextStageCtrl.$resolve = {
    stageModel: ['doc',
      function (doc) {
        var caseDoc = _.first(doc.docRelations, function (item) {
          return item.docId === item.rootDocId;
        });

        return {
          docId: doc.docId,
          docVersion: caseDoc.length > 0 ? caseDoc[0].docVersion : doc.version,
          docTypeId: caseDoc.length > 0 ? caseDoc[0].docDocTypeId : doc.docTypeId,
          startingDate: moment().startOf('minute').format('YYYY-MM-DDTHH:mm:ss')
        };
      }
    ]
  };

  angular.module('ems').controller('NextStageCtrl', NextStageCtrl);
}(angular, moment, _));
