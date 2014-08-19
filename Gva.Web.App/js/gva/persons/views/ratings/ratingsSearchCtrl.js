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