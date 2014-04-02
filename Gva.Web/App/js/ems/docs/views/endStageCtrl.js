/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function EndStageCtrl(
    $scope,
    $state,
    $stateParams,
    DocStage,
    doc,
    stageModel
  ) {
    $scope.stageModel = stageModel;

    $scope.save = function () {
      return $scope.stageForm.$validate().then(function () {
        if ($scope.stageForm.$valid) {
          return DocStage
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
    'DocStage',
    'doc',
    'stageModel'
  ];

  EndStageCtrl.$resolve = {
    stageModel: ['Nomenclature', 'doc', 'DocStage',
      function (Nomenclature, doc, DocStage) {
        return DocStage.get({
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
