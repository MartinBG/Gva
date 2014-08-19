/*global angular*/
(function (angular) {
  'use strict';

  function ExamsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonExams,
    exam
  ) {
    $scope.exam = exam;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

    $scope.save = function () {
      return $scope.newExamForm.$validate().then(function () {
        if ($scope.newExamForm.$valid) {
          return PersonExams.save({ id: $stateParams.id }, $scope.exam).$promise.then(function () {
            return $state.go('root.persons.view.examASs.search');
          });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.examASs.search');
    };
  }

  ExamsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonExams',
    'exam'
  ];

  ExamsNewCtrl.$resolve = {
    exam: [
      '$stateParams',
      'PersonExams',
      function ($stateParams, PersonExams) {
        return PersonExams.newExam({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ExamsNewCtrl', ExamsNewCtrl);
}(angular));
