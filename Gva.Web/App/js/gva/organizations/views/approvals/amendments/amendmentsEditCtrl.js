/*global angular,_*/
(function (angular) {
  'use strict';

  function AmendmentsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationAmendment,
    organizationAmendment
  ) {
    var originalAmendment = _.cloneDeep(organizationAmendment);

    $scope.amendment = organizationAmendment;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.amendment = _.cloneDeep(originalAmendment);
    };

    $scope.save = function () {
      return $scope.editAmendmentForm.$validate()
      .then(function () {
        if ($scope.editAmendmentForm.$valid) {
          return OrganizationAmendment
            .save({
              id: $stateParams.id,
              ind: $stateParams.ind,
              childInd: $stateParams.childInd
            }, $scope.amendment)
            .$promise
            .then(function () {
              return $state.go('root.organizations.view.amendments.search');
            });
        }
      });
    };

    $scope.deleteAmendment = function () {
      return OrganizationAmendment.remove({
        id: $stateParams.id,
        ind: $stateParams.ind,
        childInd: organizationAmendment.partIndex
      }).$promise.then(function () {
          return $state.go('root.organizations.view.amendments.search');
        });
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
