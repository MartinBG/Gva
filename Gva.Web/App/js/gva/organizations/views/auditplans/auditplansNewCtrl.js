/*global angular*/
(function (angular) {
  'use strict';

  function AuditplansNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAuditplan,
    organizationAuditplan
    ) {
    $scope.organizationAuditplan = organizationAuditplan;

    $scope.save = function () {
      return $scope.organizationAuditplanForm.$validate()
        .then(function () {
          if ($scope.organizationAuditplanForm.$valid) {
            return OrganizationAuditplan
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
    'OrganizationAuditplan',
    'organizationAuditplan'
  ];

  AuditplansNewCtrl.$resolve = {
    organizationAuditplan: function () {
      return {};
    }
  };

  angular.module('gva').controller('AuditplansNewCtrl', AuditplansNewCtrl);
}(angular));
