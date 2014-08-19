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
