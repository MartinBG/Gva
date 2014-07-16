/*global angular*/
(function (angular) {
  'use strict';

  function ChooseEmploymentModalCtrl(
    $scope,
    $modalInstance,
    employments
  ) {
      $scope.employments = employments;

      $scope.selectEmployment = function (employment) {
        return $modalInstance.close(employment.name);
      };

      $scope.cancel = function () {
        return $modalInstance.dismiss('cancel');
      };
  }

  ChooseEmploymentModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'employments'
  ];

  ChooseEmploymentModalCtrl.$resolve = {
    employments: [
      'Nomenclatures',
      function (Nomenclatures) {
        return Nomenclatures.query({
          alias: 'employmentCategories'
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseEmploymentModalCtrl', ChooseEmploymentModalCtrl);
}(angular));
