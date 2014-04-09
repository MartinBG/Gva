/*global angular*/
(function (angular) {
  'use strict';

  function AmendmentsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAmendment,
    organizationAmendment
    ) {
    $scope.amendment = organizationAmendment;

    $scope.save = function () {
      return $scope.newAmendmentForm.$validate()
        .then(function () {
          if ($scope.newAmendmentForm.$valid) {
            return OrganizationAmendment
              .save({ id: $stateParams.id, ind: $stateParams.ind },
              $scope.amendment)
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
        part: {
          includedDocuments: [],
          lims145: [],
          lims147: [],
          limsMG: []
        },
        files: {
          hideApplications: false,
          files: []
        }
      };
    }
  };

  angular.module('gva').controller('AmendmentsNewCtrl', AmendmentsNewCtrl);
}(angular));
