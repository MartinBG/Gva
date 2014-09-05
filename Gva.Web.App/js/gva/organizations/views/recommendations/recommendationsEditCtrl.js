/*global angular,_*/
(function (angular, _) {
  'use strict';

  function RecommendationsEditCtrl(
    $scope,
    $state,
    $stateParams,
    OrganizationRecommendations,
    organizationRecommendation,
    scMessage
  ) {
    var originalRecommendation = _.cloneDeep(organizationRecommendation);
    $scope.lotId = $stateParams.id;

    $scope.organizationRecommendation = organizationRecommendation;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.organizationRecommendation = _.cloneDeep(originalRecommendation);
    };

    $scope.save = function () {
      return $scope.editRecommendation.$validate()
        .then(function () {
          if ($scope.editRecommendation.$valid) {
            return OrganizationRecommendations
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

    $scope.deleteRecommendation = function () {
      return scMessage('common.messages.confirmDelete')
      .then(function (result) {
        if (result === 'OK') {
          return OrganizationRecommendations
            .remove({ id: $stateParams.id, ind: organizationRecommendation.partIndex })
            .$promise.then(function () {
              return $state.go('root.organizations.view.recommendations.search');
            });
        }
      });
    };
  }

  RecommendationsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'OrganizationRecommendations',
    'organizationRecommendation',
    'scMessage'
  ];

  RecommendationsEditCtrl.$resolve = {
    organizationRecommendation: [
      '$stateParams',
      'OrganizationRecommendations',
      function ($stateParams, OrganizationRecommendations) {
        return OrganizationRecommendations.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('RecommendationsEditCtrl', RecommendationsEditCtrl);
}(angular, _));
