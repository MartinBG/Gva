/*global angular*/
(function (angular) {
  'use strict';

  function AuditplansEditCtrl(
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
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.organizationAuditplan)
            .$promise
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

  AuditplansEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationAuditplan',
    'organizationAuditplan'
  ];

  AuditplansEditCtrl.$resolve = {
    organizationAuditplan: [
      '$stateParams',
      'OrganizationAuditplan',
      function ($stateParams, OrganizationAuditplan) {
        return OrganizationAuditplan.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AuditplansEditCtrl', AuditplansEditCtrl);
}(angular));
