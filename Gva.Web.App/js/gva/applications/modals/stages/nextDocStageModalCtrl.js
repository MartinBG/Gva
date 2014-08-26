/*global angular, moment*/
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

    $scope.isEndingDateValid = function (endingDate) {
      if (!endingDate) {
        return true;
      }
      else {
        var momentED = moment(endingDate).startOf('day'),
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
    stageModel: [
      'scModalParams',
      function (scModalParams) {
        return {
          docId: scModalParams.doc.docId,
          docVersion: scModalParams.caseDoc ?
            scModalParams.caseDoc.docVersion :
            scModalParams.doc.version,
          docTypeId: scModalParams.caseDoc ?
            scModalParams.caseDoc.docDocTypeId :
            scModalParams.doc.docTypeId,
          startingDate: moment().startOf('minute').format('YYYY-MM-DDTHH:mm:ss')
        };
      }
    ]
  };

  angular.module('gva').controller('NextDocStageModalCtrl', NextDocStageModalCtrl);
}(angular, moment));
