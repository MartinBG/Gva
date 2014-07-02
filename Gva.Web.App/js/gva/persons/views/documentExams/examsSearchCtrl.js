/*global angular*/
(function (angular) {
  'use strict';

  function DocumentExamsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    exams
  ) {

    $scope.exams = exams;

    $scope.editExam = function (exam) {
      return $state.go('root.persons.view.exams.edit',
        {
          id: $stateParams.id,
          ind: exam.partIndex
        });
    };

    $scope.newExam = function () {
      return $state.go('root.persons.view.exams.new');
    };
  }

  DocumentExamsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'exams'
  ];

  DocumentExamsSearchCtrl.$resolve = {
    exams: [
      '$stateParams',
      'PersonDocumentExams',
      function ($stateParams, PersonDocumentExams) {
        return PersonDocumentExams.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('DocumentExamsSearchCtrl', DocumentExamsSearchCtrl);
}(angular));
