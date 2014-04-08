/*global angular*/
(function (angular) {
  'use strict';

  function AuditplansSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAuditplan,
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
    'OrganizationAuditplan',
    'organizationAuditplans'
  ];

  AuditplansSearchCtrl.$resolve = {
    organizationAuditplans: [
      '$stateParams',
      'OrganizationAuditplan',
      function ($stateParams, OrganizationAuditplan) {
        return OrganizationAuditplan.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('AuditplansSearchCtrl', AuditplansSearchCtrl);
}(angular));