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
      'PersonExams',
      function ($stateParams, PersonExams) {
        return PersonExams.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('ExamsSearchCtrl', ExamsSearchCtrl);
}(angular));
