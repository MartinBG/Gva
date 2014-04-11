/*global angular*/
(function (angular) {
  'use strict';

  function AddRatingCtrl(
    $scope,
    $state,
    $stateParams,
    PersonRating,
    rating
  ) {
    $scope.rating = rating;

    $scope.save = function () {
      return $scope.newRatingForm.$validate()
        .then(function () {
          if ($scope.newRatingForm.$valid) {
            return PersonRating
              .save({ id: $stateParams.id }, $scope.rating).$promise
              .then(function (rating) {
                return $state.go('^', {}, {}, {
                  selectedRatings: [rating.partIndex]
                });
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  AddRatingCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonRating',
    'rating'
  ];

  AddRatingCtrl.$resolve = {
    rating: function () {
      return {
        part: {
          editions: [{}]
        }
      };
    }
  };

  angular.module('gva').controller('AddRatingCtrl', AddRatingCtrl);
}(angular));