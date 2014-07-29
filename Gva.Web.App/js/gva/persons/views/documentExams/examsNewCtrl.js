/*global angular*/
(function (angular) {
  'use strict';

  function DocumentExamsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentExams,
    exam
  ) {
    $scope.exam = exam;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newExamForm.$validate()
        .then(function () {
          if ($scope.newExamForm.$valid) {
            return PersonDocumentExams
              .save({ id: $stateParams.id }, $scope.exam).$promise
              .then(function () {
                return $state.go('root.persons.view.exams.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.exams.search');
    };
  }

  DocumentExamsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentExams',
    'exam'
  ];

  DocumentExamsNewCtrl.$resolve = {
    exam: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('DocumentExamsNewCtrl', DocumentExamsNewCtrl);
}(angular));
