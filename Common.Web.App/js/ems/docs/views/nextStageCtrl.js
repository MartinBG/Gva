/*global angular, moment, _*/
(function (angular, moment, _) {
  'use strict';

  function NextStageCtrl(
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
        return moment($scope.stageModel.endingDate) >= moment($scope.stageModel.startingDate);
      }
    };
  }

  NextStageCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'DocStage',
    'doc',
    'stageModel'
  ];

  NextStageCtrl.$resolve = {
    stageModel: ['Nomenclature', 'doc',
      function (Nomenclature, doc) {
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
