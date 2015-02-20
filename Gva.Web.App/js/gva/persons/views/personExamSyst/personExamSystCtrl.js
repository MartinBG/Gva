/*global angular, _*/
(function (angular, _) {
  'use strict';

  function PersonExamSystCtrl(
    $scope,
    $state, 
    $stateParams,
    scModal,
    scMessage,
    PersonExamSystData,
    exams,
    examSystData) {
    var originalData = _.cloneDeep(examSystData);
    $scope.examSystData = examSystData;
    $scope.exams = exams;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.addNewState = function () {
      var modalInstance = scModal.open('newQualificationState', {
        lotId: $stateParams.id
      });

      modalInstance.result.then(function (newState) {
        $scope.examSystData.part.states.push(newState);
      });

      return modalInstance.opened;
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.examSystData = _.cloneDeep(originalData);
    };

    $scope.save = function () {
      return PersonExamSystData
        .updateInfo({ id: $stateParams.id }, $scope.examSystData.part)
        .$promise
        .then(function () {
          $state.transitionTo('root.persons.view.examinationSystem', $stateParams, {reload: true});
        });
    };

    $scope.deleteState = function (state) {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          $scope.examSystData.part.states = _.without($scope.examSystData.part.states, state);
        }
      });
    };
  }

  PersonExamSystCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'scModal',
    'scMessage',
    'PersonExamSystData',
    'exams',
    'examSystData'
  ];
  
  PersonExamSystCtrl.$resolve = {
    examSystData: [
      '$stateParams',
      'PersonExamSystData',
      function ($stateParams, PersonExamSystData) {
        return PersonExamSystData.get({ id: $stateParams.id }).$promise;
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
}(angular, _));
