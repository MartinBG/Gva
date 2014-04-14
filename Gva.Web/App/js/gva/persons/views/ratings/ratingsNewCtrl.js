/*global angular*/
(function (angular) {
  'use strict';

  function RatingsNewCtrl(
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
              .then(function () {
                return $state.go('root.persons.view.ratings.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.ratings.search');
    };
  }

  RatingsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonRating',
    'rating'
  ];

  RatingsNewCtrl.$resolve = {
    rating: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {
              editions: [{ applications: [application] }]
            }
          };
        }
        else {
          return {
            part: {
              editions: [{}]
            }
          };
        }
      }
    ]
  };

  angular.module('gva').controller('RatingsNewCtrl', RatingsNewCtrl);
}(angular));
