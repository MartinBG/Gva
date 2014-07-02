/*global angular*/
(function (angular) {
  'use strict';

  function RatingsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    ratings
  ) {
    $scope.ratings = ratings;

    $scope.viewEdition = function (rating) {
      return $state.go('root.persons.view.ratings.edit', {
        id: $stateParams.id,
        ind: rating.partIndex
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
    'ratings'
  ];

  RatingsSearchCtrl.$resolve = {
    ratings: [
      '$stateParams',
      'PersonRatings',
      function ($stateParams, PersonRatings) {
        return PersonRatings.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('RatingsSearchCtrl', RatingsSearchCtrl);
}(angular));