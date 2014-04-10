/*global angular*/
(function (angular) {
  'use strict';

  function RecommendationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRecommendation,
    organizationRecommendation
  ) {
    $scope.organizationRecommendation = organizationRecommendation;

    $scope.save = function () {
      return $scope.newRecommendation.$validate()
        .then(function () {
          if ($scope.newRecommendation.$valid) {
            return OrganizationRecommendation
              .save({ id: $stateParams.id }, $scope.organizationRecommendation)
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

  RecommendationsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationRecommendation',
    'organizationRecommendation'
  ];

  RecommendationsNewCtrl.$resolve = {
    organizationRecommendation: function () {
      return {
        part: {
          includedAudits: [],
          auditorsReview: {
            auditDetails: [],
            disparities: []
          },
          descriptionReview: {
            auditDetails: [],
            disparities: []
          },
          part1: { examiners: [] },
          part2: { examiners: [] },
          part3: { examiners: [] },
          part4: { examiners: [] },
          part5: { examiners: [] }
        }
      };
    }
  };

  angular.module('gva').controller('RecommendationsNewCtrl', RecommendationsNewCtrl);
}(angular));
