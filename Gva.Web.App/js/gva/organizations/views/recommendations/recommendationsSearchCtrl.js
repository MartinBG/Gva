/*global angular*/
(function (angular) {
  'use strict';

  function RecommendationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    organizationRecommendations
  ) {

    $scope.organizationRecommendations = organizationRecommendations;

    $scope.editRecommendation = function (recommendation) {
      return $state.go('root.organizations.view.recommendations.edit', {
        id: $stateParams.id,
        ind: recommendation.partIndex
      });
    };

    $scope.newRecommendation = function () {
      return $state.go('root.organizations.view.recommendations.new');
    };
  }

  RecommendationsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'organizationRecommendations'
  ];

  RecommendationsSearchCtrl.$resolve = {
    organizationRecommendations: [
      '$stateParams',
      'OrganizationRecommendations',
      function ($stateParams, OrganizationRecommendations) {
        return OrganizationRecommendations.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('RecommendationsSearchCtrl', RecommendationsSearchCtrl);
}(angular));