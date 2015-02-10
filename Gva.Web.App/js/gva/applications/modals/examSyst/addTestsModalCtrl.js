/*global angular*/
(function (angular) {
  'use strict';

  function AddTestsModalCtrl(
    $scope,
    $modalInstance,
    scModalParams
  ) {
    $scope.form = {};
    $scope.qualificationCode = scModalParams.qualificationCode;
    $scope.certCampCode = scModalParams.certCampCode;
    $scope.tests = scModalParams.tests;

    $scope.deleteTest = function (test) {
      var index = $scope.tests.indexOf(test);
      $scope.tests.splice(index, 1);
    };

    $scope.addTest = function () {
      $scope.tests.push({});
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.save = function () {
      return $scope.form.addTestsForm.$validate()
        .then(function () {
          if ($scope.form.addTestsForm.$valid) {
            return $modalInstance.close($scope.tests);
          }
        });
    };
  }

  AddTestsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams'
  ];

  angular.module('gva').controller('AddTestsModalCtrl', AddTestsModalCtrl);
}(angular));
