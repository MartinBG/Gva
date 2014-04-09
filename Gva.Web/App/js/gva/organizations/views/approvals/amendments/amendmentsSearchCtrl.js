/*global angular, _*/
(function (angular,_) {
  'use strict';

  function AmendmentsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAmendment,
    organizationAmendments
  ) {

    $scope.organizationAmendments = organizationAmendments;

    $scope.editAmendment = function (amendment) {
      return $state.go('root.organizations.view.amendments.edit', {
        id: $stateParams.id,
        ind: $stateParams.ind,
        childInd: amendment.partIndex
      });
    };

    $scope.newAmendment = function () {
      return $state.go('root.organizations.view.amendments.new');
    };

    $scope.goBack = function () {
      return $state.go('root.organizations.view.approvals.search');
    };
  }

  AmendmentsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationAmendment',
    'organizationAmendments'
  ];

  AmendmentsSearchCtrl.$resolve = {
    organizationAmendments: [
      '$stateParams',
      'OrganizationAmendment',
      function ($stateParams, OrganizationAmendment) {
        return OrganizationAmendment.query($stateParams).$promise.then(function(amendments){
          return _.sortBy(amendments, 'partIndex').reverse();
        });
      }
    ]
  };

  angular.module('gva')
    .controller('AmendmentsSearchCtrl', AmendmentsSearchCtrl);
}(angular, _));