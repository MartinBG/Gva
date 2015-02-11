/*global angular*/
(function (angular) {
  'use strict';

  function AddExamsModalCtrl(
    $scope,
    $modalInstance,
    scModalParams
  ) {
    $scope.form = {};
    $scope.qualificationCode = scModalParams.qualificationCode;
    $scope.certCampCode = scModalParams.certCampCode;
    $scope.exams = scModalParams.exams || [];

    $scope.deleteExam = function (exam) {
      var index = $scope.exams.indexOf(exam);
      $scope.exams.splice(index, 1);
    };

    $scope.addExam = function () {
      $scope.exams.push({});
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.save = function () {
      return $scope.form.addExamsForm.$validate()
        .then(function () {
          if ($scope.form.addExamsForm.$valid) {
            return $modalInstance.close($scope.exams);
          }
        });
    };
  }

  AddExamsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams'
  ];

  angular.module('gva').controller('AddExamsModalCtrl', AddExamsModalCtrl);
}(angular));
