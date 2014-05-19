/*global angular,_*/
(function (angular) {
  'use strict';

  function StaffExaminersEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationStaffExaminer,
    organizationStaffExaminer
  ) {
    var originalStaffExaminer = _.cloneDeep(organizationStaffExaminer);

    $scope.organizationStaffExaminer = organizationStaffExaminer;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationStaffExaminer = _.cloneDeep(originalStaffExaminer);
    };

    $scope.save = function () {
      return $scope.editStaffChecker.$validate()
        .then(function () {
          if ($scope.editStaffChecker.$valid) {
            return OrganizationStaffExaminer
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.organizationStaffExaminer)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.staffExaminers.search');
              });
          }
        });
    };

    $scope.deleteStaffChecker = function () {
      return OrganizationStaffExaminer.remove({
          id: $stateParams.id,
          ind: organizationStaffExaminer.partIndex
        }).$promise.then(function () {
          return $state.go('root.organizations.view.staffExaminers.search');
        });
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
