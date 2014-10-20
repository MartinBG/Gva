/*global angular*/
(function (angular) {
  'use strict';

  function NewExamModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentTrainings,
    scModalParams,
    personDocumentExam
  ) {
    $scope.form = {};
    $scope.personDocumentExam = personDocumentExam;
    $scope.lotId = scModalParams.lotId;
    $scope.caseTypeId = scModalParams.caseTypeId;
    $scope.save = function () {
      return $scope.form.newDocumentExamForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentExamForm.$valid) {
            return PersonDocumentTrainings
              .save({ id: $scope.lotId }, $scope.personDocumentExam)
              .$promise
              .then(function (savedExam) {
                return $modalInstance.close(savedExam);
              });
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewExamModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonDocumentTrainings',
    'scModalParams',
    'personDocumentExam'
  ];

  NewExamModalCtrl.$resolve = {
    personDocumentExam: [
      'PersonDocumentTrainings',
      'scModalParams',
      function (PersonDocumentTrainings, scModalParams) {
        return PersonDocumentTrainings.newTraining({
              id: scModalParams.lotId,
              appId: scModalParams.appId
            }).$promise;
      }
    ]
  };

  angular.module('gva').controller('NewExamModalCtrl', NewExamModalCtrl);
}(angular));
