/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function NextStageCtrl(
    $scope,
    $state,
    DocStage,
    doc,
    stageModel
  ) {
    $scope.stageModel = stageModel;

    $scope.save = function () {
      $scope.stageForm.$validate().then(function () {
        if ($scope.stageForm.$valid) {

          var stageData = {
            electronicServiceStageId: $scope.stageModel.stageId,
            startingDate: $scope.stageModel.startingDate,
            expectedEndingDate: $scope.stageModel.expectedEndingDate,
            endingDate: $scope.stageModel.endingDate
          };

          return DocStage.next({ docId: stageModel.docId }, stageData).$promise
            .then(function (result) {
            doc.docElectronicServiceStages = result.stages;
            return $state.go('^');
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

  NextStageCtrl.$inject = [
    '$scope',
    '$state',
    'DocStage',
    'doc',
    'stageModel'
  ];

  NextStageCtrl.$resolve = {
    stageModel: ['Nomenclature', 'doc',
      function (Nomenclature, doc) {
        return {
          docId: doc.docId,
          docTypeId: doc.docTypeId,
          startingDate: moment().startOf('day').format('YYYY-MM-DDTHH:mm:ss'),
          executors: 'Служител ДКХ'
        };
      }
    ]
  };

  angular.module('ems').controller('NextStageCtrl', NextStageCtrl);
}(angular, moment));
