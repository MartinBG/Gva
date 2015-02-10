/*global angular*/
(function (angular) {
  'use strict';

  function QualificationsCtrl(
    $scope,
    qualifications
  ) {
    $scope.qualifications = qualifications;
  }

  QualificationsCtrl.$inject = [
    '$scope',
    'qualifications'
  ];

  QualificationsCtrl.$resolve = {
    qualifications: [
      'ExaminationSystem',
      function (ExaminationSystem) {
        return ExaminationSystem.getQualifications().$promise;
      }
    ]
  };

  angular.module('gva').controller('QualificationsCtrl', QualificationsCtrl);
}(angular));