/*global angular*/
(function (angular) {
  'use strict';

  function AuditplansSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationAuditplans
  ) {

    $scope.organizationAuditplans = organizationAuditplans;

    $scope.editAuditplan = function (address) {
      return $state.go('root.organizations.view.auditplans.edit', {
        id: $stateParams.id,
        ind: address.partIndex
      });
    };

    $scope.newAuditplan = function () {
      return $state.go('root.organizations.view.auditplans.new');
    };
  }

  AuditplansSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organizationAuditplans'
  ];

  AuditplansSearchCtrl.$resolve = {
    organizationAuditplans: [
      '$stateParams',
      'OrganizationAuditplans',
      function ($stateParams, OrganizationAuditplans) {
        return OrganizationAuditplans.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('AuditplansSearchCtrl', AuditplansSearchCtrl);
}(angular));