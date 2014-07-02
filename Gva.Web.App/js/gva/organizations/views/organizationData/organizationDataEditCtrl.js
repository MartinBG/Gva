/*global angular,_*/
(function (angular) {
  'use strict';

  function OrganizationDataEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationsData,
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
          return OrganizationsData
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
    'OrganizationsData',
    'organizationData'
  ];

  OrganizationDataEditCtrl.$resolve = {
    organizationData: [
      '$stateParams',
      'OrganizationsData',
      function ($stateParams, OrganizationsData) {
        return OrganizationsData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('OrganizationDataEditCtrl', OrganizationDataEditCtrl);
}(angular));
