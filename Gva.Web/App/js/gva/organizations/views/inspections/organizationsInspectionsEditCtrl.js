/*global angular*/
(function (angular) {
  'use strict';

  function OrganizationsInspectionsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationInspection,
    organizationInspection
    ) {
    $scope.organizationInspection = organizationInspection;

    $scope.save = function () {
      return $scope.organizationInspectionForm.$validate()
      .then(function () {
        if ($scope.organizationInspectionForm.$valid) {
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


    $scope.cancel = function () {
      return $stateParams.childInd ?
        $state.go('^') :
        $state.go('root.organizations.view.inspections.search');
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
