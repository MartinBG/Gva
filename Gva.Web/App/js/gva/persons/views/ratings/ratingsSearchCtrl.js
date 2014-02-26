/*global angular*/
(function (angular) {
  'use strict';

  function RatingsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonRating,
    PersonRatingView,
    ratings
  ) {
    $scope.ratings = ratings;

    $scope.viewEdition = function (item) {
      return $state.go('root.persons.view.editions.search', {
        id: $stateParams.id,
        ind: item.partIndex
      });
    };

    $scope.newRating = function () {
      return $state.go('root.persons.view.ratings.new');
    };
  }

  RatingsSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonRating',
    'PersonRatingView',
    'ratings'
  ];

  RatingsSearchCtrl.$resolve = {
    ratings: [
      '$stateParams',
      'PersonRatingView',
      function ($stateParams, PersonRatingView) {
        return PersonRatingView.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('RatingsSearchCtrl', RatingsSearchCtrl);
}(angular));