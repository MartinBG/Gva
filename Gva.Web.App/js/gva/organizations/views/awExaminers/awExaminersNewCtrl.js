/*global angular*/
(function (angular) {
  'use strict';

  function AwExaminersNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAwExaminers,
    organizationAwExaminer
  ) {
    $scope.organizationAwExaminer = organizationAwExaminer;

    $scope.save = function () {
      return $scope.newAwExaminer.$validate()
        .then(function () {
          if ($scope.newAwExaminer.$valid) {
            return OrganizationAwExaminers
              .save({ id: $stateParams.id }, $scope.organizationAwExaminer)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.awExaminers.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.awExaminers.search');
    };
  }

  AwExaminersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationAwExaminers',
    'organizationAwExaminer'
  ];

  AwExaminersNewCtrl.$resolve = {
    organizationAwExaminer: [
      '$stateParams',
      'OrganizationAwExaminers',
      function ($stateParams, OrganizationAwExaminers) {
        return OrganizationAwExaminers.newAwExaminer({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AwExaminersNewCtrl', AwExaminersNewCtrl);
}(angular));
