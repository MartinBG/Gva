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