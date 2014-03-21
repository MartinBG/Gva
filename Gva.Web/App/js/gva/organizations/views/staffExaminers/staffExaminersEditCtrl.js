/*global angular*/
(function (angular) {
  'use strict';

  function StaffExaminersEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffExaminer,
    organizationStaffExaminer
  ) {
    $scope.organizationStaffExaminer = organizationStaffExaminer;

    $scope.save = function () {
      $scope.organizationStaffExaminersForm.$validate()
      .then(function () {
        if ($scope.organizationStaffExaminersForm.$valid) {
          return OrganizationStaffExaminer
            .save({ id: $stateParams.id, ind: $stateParams.ind },
            $scope.organizationStaffExaminer)
            .$promise
            .then(function () {
              return $state.go('root.organizations.view.staffExaminers.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.staffExaminers.search');
    };
  }

  StaffExaminersEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationStaffExaminer',
    'organizationStaffExaminer'
  ];

  StaffExaminersEditCtrl.$resolve = {
    organizationStaffExaminer: [
      '$stateParams',
      'OrganizationStaffExaminer',
      function ($stateParams, OrganizationStaffExaminer) {
        return OrganizationStaffExaminer.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('StaffExaminersEditCtrl', StaffExaminersEditCtrl);
}(angular));
