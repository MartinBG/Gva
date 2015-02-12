/*global angular*/
(function (angular) {
  'use strict';

  function PersonExamSystCtrl(
    $scope,
    $state, 
    $stateParams,
    scModal,
    exams,
    examSystData) {

    $scope.examSystData = examSystData;
    $scope.exams = exams;

    $scope.addNewState = function () {
      var modalInstance = scModal.open('newQualificationState', {
        lotId: $stateParams.id
      });

      modalInstance.result.then(function () {
        $state.transitionTo('root.persons.view.examinationSystem', $stateParams, {reload: true});
      });

      return modalInstance.opened;
    };
  }

  PersonExamSystCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'scModal',
    'exams',
    'examSystData'
  ];
  
  PersonExamSystCtrl.$resolve = {
    examSystData: [
      '$stateParams',
      'PersonsExamSystData',
      function ($stateParams, PersonsExamSystData) {
        return PersonsExamSystData.get({ id: $stateParams.id }).$promise;
      }
    ],
    exams: [
      '$stateParams',
      'ExaminationSystem',
      function ($stateParams, ExaminationSystem) {
        return ExaminationSystem.getPersonExams({ lotId: $stateParams.id }).$promise;
      }
    ]
  };
  

  angular.module('gva').controller('PersonExamSystCtrl', PersonExamSystCtrl);
}(angular));
