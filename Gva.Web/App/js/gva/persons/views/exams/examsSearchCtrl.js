/*global angular*/
(function (angular) {
  'use strict';

  function ExamsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    exams
  ) {

    $scope.exams = exams;

    $scope.editExam = function (exam) {
      return $state.go('root.persons.view.examASs.edit',
        {
          id: $stateParams.id,
          ind: exam.partIndex
        });
    };

    $scope.newExam = function () {
      return $state.go('root.persons.view.examASs.new');
    };
  }

  ExamsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'exams'
  ];

  ExamsSearchCtrl.$resolve = {
    exams: [
      '$stateParams',
      'PersonExam',
      function ($stateParams, PersonExam) {
        return PersonExam.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('ExamsSearchCtrl', ExamsSearchCtrl);
}(angular));
