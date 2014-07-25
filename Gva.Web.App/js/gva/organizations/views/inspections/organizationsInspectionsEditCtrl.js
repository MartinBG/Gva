/*global angular,_*/
(function (angular) {
  'use strict';

  function OrganizationsInspectionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationInspections,
    organizationInspection,
    scMessage
  ) {
    var originalInspection = _.cloneDeep(organizationInspection);

    $scope.organizationInspection = organizationInspection;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationInspection = _.cloneDeep(originalInspection);
    };

    $scope.save = function () {
      return $scope.editInspectionForm.$validate()
      .then(function () {
        if ($scope.editInspectionForm.$valid) {
          return OrganizationInspections
            .save({
              id: $stateParams.id,
              ind: $stateParams.childInd ? $stateParams.childInd : $stateParams.ind
            }, $scope.organizationInspection)
            .$promise
            .then(function () {
              return $stateParams.childInd ?
                $state.go('^') :
                $state.go('root.organizations.view.inspections.search');
            });
        }
      });
    };

    $scope.deleteInspection = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return OrganizationInspections.remove({
            id: $stateParams.id,
            ind: organizationInspection.partIndex
          }).$promise.then(function () {
            return $stateParams.childInd ?
              $state.go('^') :
              $state.go('root.organizations.view.inspections.search');
          });
        }
      });
    };
  }

  OrganizationsInspectionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationInspections',
    'organizationInspection',
    'scMessage'
  ];

  OrganizationsInspectionsEditCtrl.$resolve = {
    organizationInspection: [
      '$stateParams',
      'OrganizationInspections',
      function ($stateParams, OrganizationInspections) {
        return OrganizationInspections.get({
          id: $stateParams.id,
          ind: $stateParams.childInd? $stateParams.childInd: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsInspectionsEditCtrl', OrganizationsInspectionsEditCtrl);
}(angular));
