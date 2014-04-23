/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function EditStageCtrl(
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
        return moment($scope.stageModel.endingDate) > moment($scope.stageModel.startingDate);
      }
    };
  }

  EditStageCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'DocStage',
    'doc',
    'stageModel'
  ];

  EditStageCtrl.$resolve = {
    stageModel: ['Nomenclature', 'doc', 'DocStage',
      function (Nomenclature, doc, DocStage) {
        return DocStage.get({
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
