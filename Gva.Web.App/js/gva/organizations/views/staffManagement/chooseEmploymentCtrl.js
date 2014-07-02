/*global angular*/
(function (angular) {
  'use strict';

  function ChooseEmploymentCtrl(
    $scope,
    $state,
    employments,
    selectedEmployment
  ) {
    $scope.employments = employments;

    $scope.selectEmployment = function (employment) {
      selectedEmployment.push(employment.name);
      return $state.go('^');
    };

    $scope.goBack = function () {
      return $state.go('^');
    };

  }

  ChooseEmploymentCtrl.$inject = [
    '$scope',
    '$state',
    'employments',
    'selectedEmployment'
  ];

  ChooseEmploymentCtrl.$resolve = {
    employments: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.query({
          alias: 'employmentCategories'
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseEmploymentCtrl', ChooseEmploymentCtrl);
}(angular));
