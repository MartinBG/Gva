/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseExaminersModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    examiners
  ) {
      $scope.examiners = examiners;

      $scope.selectedExaminers = [];

      var includedExaminersNames = _.pluck(scModalParams.includedExaminers, 'name');
      $scope.examiners = _.filter(examiners, function (examiner) {
        return !_.contains(includedExaminersNames, examiner.name);
      });

      $scope.addExaminers = function () {
        return $modalInstance.close($scope.selectedExaminers);
      };

      $scope.cancel = function () {
        return $modalInstance.dismiss('cancel');
      };

      $scope.selectExaminer = function (event, examiner) {
        var index;
        if ($(event.target).is(':checked')) {
          $scope.selectedExaminers.push(examiner);
        } else {
          index = $scope.selectedExaminers.indexOf(examiner);
          $scope.selectedExaminers.splice.apply($scope.selectedExaminers, [index, 1]);
        }
      };
  }

  ChooseExaminersModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'examiners'
  ];

  ChooseExaminersModalCtrl.$resolve = {
    examiners: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.query({
          alias: 'examiners'
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseExaminersModalCtrl', ChooseExaminersModalCtrl);
}(angular, _, $));
