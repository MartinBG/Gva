/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationDataEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationData,
    organizationData
  ) {
    $scope.organizationData = organizationData;

    $scope.save = function () {
      return $scope.organizationDataForm.$validate()
      .then(function () {
        if ($scope.organizationDataForm.$valid) {
          return OrganizationData
          .save({ id: $stateParams.id }, $scope.organizationData)
          .$promise
          .then(function () {
            return $state.transitionTo('root.organizations.view', $stateParams, { reload: true });
          });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view');
    };
  }

  OrganizationDataEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationData',
    'organizationData'
  ];

  OrganizationDataEditCtrl.$resolve = {
    organizationData: [
      '$stateParams',
      'OrganizationData',
      function ($stateParams, OrganizationData) {
        return OrganizationData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('OrganizationDataEditCtrl', OrganizationDataEditCtrl);
}(angular));
