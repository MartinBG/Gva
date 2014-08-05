/*global angular*/
(function (angular) {
  'use strict';

  function ChooseOrganizationModalCtrl(
    $scope,
    $modalInstance,
    Organizations,
    scModalParams,
    organizations
  ) {
    $scope.organizations = organizations;

    $scope.filters = {
      uin: scModalParams.uin,
      name: scModalParams.name
    };

    $scope.search = function () {
      return Organizations.query($scope.filters).$promise.then(function (organizations) {
        $scope.organizations = organizations;
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectOrganization = function (organization) {
      return $modalInstance.close(organization.id);
    };
  }

  ChooseOrganizationModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'Organizations',
    'scModalParams',
    'organizations'
  ];

  ChooseOrganizationModalCtrl.$resolve = {
    organizations: [
      'Organizations',
      'scModalParams',
      function (Organizations, scModalParams) {
        return Organizations.query(scModalParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseOrganizationModalCtrl', ChooseOrganizationModalCtrl);
}(angular));
