/*global angular, _*/
(function (angular, _) {
  'use strict';
  function OrganizationApprovalCtrl(
    $scope,
    scFormParams,
    OrganizationRecommendations) {
    $scope.isNew = scFormParams.isNew;
    $scope.recommendations = [];

    OrganizationRecommendations
      .queryViews({id: scFormParams.lotId})
      .$promise
      .then(function(recommendations){
        _.forEach(recommendations,
          function (recommendation) {
            $scope.recommendations.push({
              id: recommendation.partIndex,
              text: recommendation.auditPartName + ' ' + recommendation.formText
            });
          });

        if(!!$scope.model.recommendationReport) {
          $scope.recommendationReport =
            _.find($scope.recommendations, {id: $scope.model.recommendationReport });
        }

        $scope.$watch('recommendationReport.id', function (id) {
          $scope.model.recommendationReport = id;
        });
      });
  }

  OrganizationApprovalCtrl.$inject = [
    '$scope',
    'scFormParams',
    'OrganizationRecommendations'
  ];

  angular.module('gva').controller('OrganizationApprovalCtrl', OrganizationApprovalCtrl);
}(angular, _));
