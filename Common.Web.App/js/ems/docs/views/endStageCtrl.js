/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function EndStageCtrl(
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
            .end({
              id: $scope.stageModel.docId,
              docVersion: $scope.stageModel.docVersion
            }, $scope.stageModel).$promise
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
        return moment($scope.stageModel.endingDate) > moment($scope.stageModel.startingDate);
      }
    };
  }

  EndStageCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'DocStages',
    'doc',
    'stageModel'
  ];

  EndStageCtrl.$resolve = {
    stageModel: ['doc', 'DocStages',
      function (doc, DocStages) {
        return DocStages.get({
          id: doc.docId,
          docVersion: doc.version
        })
          .$promise
          .then(function (result) {
            result.docVersion = doc.version;
            return result;
          });
      }
    ]
  };

  angular.module('ems').controller('EndStageCtrl', EndStageCtrl);
}(angular, moment));
