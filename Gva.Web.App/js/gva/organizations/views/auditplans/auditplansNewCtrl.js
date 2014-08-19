/*global angular*/
(function (angular) {
  'use strict';

  function AuditplansNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAuditplans,
    organizationAuditplan
  ) {
    $scope.organizationAuditplan = organizationAuditplan;

    $scope.save = function () {
      return $scope.newAuditplanForm.$validate()
        .then(function () {
          if ($scope.newAuditplanForm.$valid) {
            return OrganizationAuditplans
              .save({ id: $stateParams.id }, $scope.organizationAuditplan).$promise
              .then(function () {
                return $state.go('root.organizations.view.auditplans.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.auditplans.search');
    };
  }

  AuditplansNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationAuditplans',
    'organizationAuditplan'
  ];

  AuditplansNewCtrl.$resolve = {
    organizationAuditplan: [
      '$stateParams',
      'OrganizationAuditplans',
      function ($stateParams, OrganizationAuditplans) {
        return OrganizationAuditplans.newAuditplan({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AuditplansNewCtrl', AuditplansNewCtrl);
}(angular));
