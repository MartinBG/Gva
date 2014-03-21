/*global angular*/
(function (angular) {
  'use strict';

  function AmendmentsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAmendment,
    organizationAmendment
  ) {
    $scope.organizationAmendment = organizationAmendment;

    $scope.save = function () {
      return $scope.organizationAmendmentForm.$validate()
      .then(function () {
        if ($scope.organizationAmendmentForm.$valid) {
          return OrganizationAmendment
            .save({
              id: $stateParams.id,
              ind: $stateParams.ind,
              childInd: $stateParams.childInd
            }, $scope.organizationAmendment)
            .$promise
            .then(function () {
              return $state.go('root.organizations.view.amendments.search');
            });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.amendments.search');
    };
  }

  AmendmentsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationAmendment',
    'organizationAmendment'
  ];

  AmendmentsEditCtrl.$resolve = {
    organizationAmendment: [
      '$stateParams',
      'OrganizationAmendment',
      function ($stateParams, OrganizationAmendment) {
        return OrganizationAmendment.get({
          id: $stateParams.id,
          ind: $stateParams.ind,
          childInd: $stateParams.childInd
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('AmendmentsEditCtrl', AmendmentsEditCtrl);
}(angular));
