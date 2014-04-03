/*global angular*/
(function (angular) {
  'use strict';

  function RecommendationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRecommendation,
    organizationRecommendation
    ) {

    $scope.organizationRecommendation = organizationRecommendation;

    $scope.save = function () {
      return $scope.organizationRecommendationsForm.$validate()
        .then(function () {
          if ($scope.organizationRecommendationsForm.$valid) {
            return OrganizationRecommendation
              .save({
                id: $stateParams.id,
                ind: $stateParams.ind
              }, $scope.organizationRecommendation)
              .$promise
              .then(function () {
                return $state.go('root.organizations.view.recommendations.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.organizations.view.recommendations.search');
    };
  }

  RecommendationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationRecommendation',
    'organizationRecommendation'
  ];

  RecommendationsEditCtrl.$resolve = {
    organizationRecommendation: [
      '$stateParams',
      'OrganizationRecommendation',
      function ($stateParams, OrganizationRecommendation) {
        return OrganizationRecommendation.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('RecommendationsEditCtrl', RecommendationsEditCtrl);
}(angular));
