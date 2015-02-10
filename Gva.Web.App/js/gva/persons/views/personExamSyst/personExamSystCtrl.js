/*global angular*/
(function (angular) {
  'use strict';

  function PersonExamSystCtrl(
    $scope,
    examSystData) {
    $scope.examSystData = examSystData;
  }

  PersonExamSystCtrl.$inject = [
    '$scope',
    'examSystData'
  ];
  
  PersonExamSystCtrl.$resolve = {
    examSystData: [
      '$stateParams',
      'PersonsExamSystData',
      function ($stateParams, PersonsExamSystData) {
        return PersonsExamSystData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };


  angular.module('gva').controller('PersonExamSystCtrl', PersonExamSystCtrl);
}(angular));
