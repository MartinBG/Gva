/*global angular*/
(function (angular) {
  'use strict';

  function ChooseOrganizationModalCtrl(
    $scope,
    $modalInstance,
    Organizations,
    organizations,
    uin,
    name
  ) {
    $scope.organizations = organizations;

    $scope.filters = {
      uin: uin,
      name: name
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
    'organizations',
    'uin',
    'name'
  ];

  angular.module('gva').controller('ChooseOrganizationModalCtrl', ChooseOrganizationModalCtrl);
}(angular));
