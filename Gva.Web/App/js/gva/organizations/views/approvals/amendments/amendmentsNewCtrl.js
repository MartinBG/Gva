/*global angular*/
(function (angular) {
  'use strict';

  function AmendmentsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAmendment,
    organizationAmendment) {
    $scope.organizationAmendment = organizationAmendment;

    $scope.save = function () {
      return $scope.organizationAmendmentForm.$validate()
        .then(function () {
          if ($scope.organizationAmendmentForm.$valid) {
            return OrganizationAmendment
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.organizationAmendment)
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

  AmendmentsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationAmendment',
    'organizationAmendment'
  ];

  AmendmentsNewCtrl.$resolve = {
    organizationAmendment: function () {
      return {
        amendment: {
          includedDocuments: [],
          lims145: [],
          lims147: [],
          limsMG: []
        }
      };
    }
  };

  angular.module('gva').controller('AmendmentsNewCtrl', AmendmentsNewCtrl);
}(angular));
