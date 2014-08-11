/*global angular*/
(function (angular) {
  'use strict';

  function RatingsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonRatings,
    rating
  ) {
    $scope.rating = rating;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newRatingForm.$validate()
        .then(function () {
          if ($scope.newRatingForm.$valid) {
            return PersonRatings
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
    'PersonRatings',
    'rating'
  ];

  RatingsNewCtrl.$resolve = {
    rating: [
      '$stateParams',
      'PersonRatings',
      function ($stateParams, PersonRatings) {
        return PersonRatings.newRating({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('RatingsNewCtrl', RatingsNewCtrl);
}(angular));
