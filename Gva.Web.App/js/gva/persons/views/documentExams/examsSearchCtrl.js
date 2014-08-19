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
