/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseRatingCtrl(
    $state,
    $stateParams,
    $scope,
    ratings
  ) {
    if (!($state.payload && $state.payload.selectedRatings)) {
      $state.go('^');
      return;
    }

    $scope.selectedRatings = [];

    $scope.ratings = _.filter(ratings, function (rating) {
      return !_.contains($state.payload.selectedRatings, rating.partIndex);
    });

    $scope.addRatings = function () {
      return $state.go('^', {}, {}, {
        selectedRatings: _.pluck($scope.selectedRatings, 'partIndex')
      });
    };

    $scope.goBack = function () {
      return $state.go('^');
    };

    $scope.selectRating = function (event, rating) {
      if ($(event.target).is(':checked')) {
        $scope.selectedRatings.push(rating);
      }
      else {
        $scope.selectedRatings = _.without($scope.selectedRatings, rating);
      }
    };
  }

  ChooseRatingCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'ratings'
  ];

  ChooseRatingCtrl.$resolve = {
    ratings: [
      '$stateParams',
      'PersonRating',
      function ($stateParams, PersonRating) {
        return PersonRating.query({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseRatingCtrl', ChooseRatingCtrl);
}(angular, _, $));
