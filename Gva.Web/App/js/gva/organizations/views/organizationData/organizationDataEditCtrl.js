/*global angular,_*/
(function (angular) {
  'use strict';

  function OrganizationDataEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationData,
    organizationData
  ) {
    var originalOrganizationData = _.cloneDeep(organizationData);

    $scope.organizationData = organizationData;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationData = _.cloneDeep(originalOrganizationData);
    };

    $scope.save = function () {
      return $scope.editOrganizationForm.$validate()
      .then(function () {
        if ($scope.editOrganizationForm.$valid) {
          return OrganizationData
          .save({ id: $stateParams.id }, $scope.organizationData)
          .$promise
          .then(function () {
            return $state.transitionTo('root.organizations.view', $stateParams, { reload: true });
          });
        }
      });
    };
  }

  OrganizationDataEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationData',
    'organizationData'
  ];

  OrganizationDataEditCtrl.$resolve = {
    organizationData: [
      '$stateParams',
      'OrganizationData',
      function ($stateParams, OrganizationData) {
        return OrganizationData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('OrganizationDataEditCtrl', OrganizationDataEditCtrl);
}(angular));
