/*global angular,_*/
(function (angular) {
  'use strict';

  function AuditplansEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAuditplan,
    organizationAuditplan
  ) {
    var originalAuditplan = _.cloneDeep(organizationAuditplan);

    $scope.organizationAuditplan = organizationAuditplan;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationAuditplan = _.cloneDeep(originalAuditplan);
    };

    $scope.save = function () {
      return $scope.editAuditplanForm.$validate()
      .then(function () {
        if ($scope.editAuditplanForm.$valid) {
          return OrganizationAuditplan
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.organizationAuditplan)
            .$promise
            .then(function () {
              return $state.go('root.organizations.view.auditplans.search');
            });
        }
      });
    };

    $scope.deleteAuditplan = function () {
      return OrganizationAuditplan.remove({
        id: $stateParams.id,
        ind: organizationAuditplan.partIndex
      }).$promise.then(function () {
        return $state.go('root.organizations.view.auditplans.search');
      });
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
