/*global angular*/
(function (angular) {
  'use strict';

  function RecommendationsNewCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRecommendations,
    organizationRecommendation
  ) {
    $scope.organizationRecommendation = organizationRecommendation;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newRecommendation.$validate()
        .then(function () {
          if ($scope.newRecommendation.$valid) {
            return OrganizationRecommendations
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
    'OrganizationRecommendations',
    'organizationRecommendation'
  ];

  RecommendationsNewCtrl.$resolve = {
    organizationRecommendation: [
      '$stateParams',
      'OrganizationRecommendations',
      function ($stateParams, OrganizationRecommendations) {
        return OrganizationRecommendations.newRecommendation({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
       }
    ]
  };

  angular.module('gva').controller('RecommendationsNewCtrl', RecommendationsNewCtrl);
}(angular));
