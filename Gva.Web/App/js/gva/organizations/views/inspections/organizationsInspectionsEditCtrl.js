/*global angular,_*/
(function (angular) {
  'use strict';

  function OrganizationsInspectionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationInspection,
    organizationInspection
  ) {
    var originalInspection = _.cloneDeep(organizationInspection);

    $scope.organizationInspection = organizationInspection;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationInspection.part = _.cloneDeep(originalInspection.part);
      $scope.$broadcast('cancel', originalInspection);
    };

    $scope.save = function () {
      return $scope.editInspectionForm.$validate()
      .then(function () {
        if ($scope.editInspectionForm.$valid) {
          return OrganizationInspection
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
      return OrganizationInspection.remove({
        id: $stateParams.id,
        ind: organizationInspection.partIndex
      }).$promise.then(function () {
        return $stateParams.childInd ?
          $state.go('^') :
          $state.go('root.organizations.view.inspections.search');
      });
    };
  }

  OrganizationsInspectionsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationInspection',
    'organizationInspection'
  ];

  OrganizationsInspectionsEditCtrl.$resolve = {
    organizationInspection: [
      '$stateParams',
      'OrganizationInspection',
      function ($stateParams, OrganizationInspection) {
        return OrganizationInspection.get({
          id: $stateParams.id,
          ind: $stateParams.childInd? $stateParams.childInd: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('OrganizationsInspectionsEditCtrl', OrganizationsInspectionsEditCtrl);
}(angular));
