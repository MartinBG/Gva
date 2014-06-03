﻿/*global angular*/
(function (angular) {
  'use strict';

  function RecommendationsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRecommendation,
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
    'OrganizationRecommendation',
    'organizationRecommendations'
  ];

  RecommendationsSearchCtrl.$resolve = {
    organizationRecommendations: [
      '$stateParams',
      'OrganizationRecommendation',
      function ($stateParams, OrganizationRecommendation) {
        return OrganizationRecommendation.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva')
    .controller('RecommendationsSearchCtrl', RecommendationsSearchCtrl);
}(angular));