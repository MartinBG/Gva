/*global angular, moment*/
(function (angular, moment) {
  'use strict';

  function RatingsSearchCtrl(
    $scope,
    $state,
    $stateParams,
    ratings
  ) {
    $scope.ratings = ratings;

    $scope.isExpiringRating = function(item) {
      var today = moment(new Date()),
          difference = moment(item.lastDocDateValidTo).diff(today, 'days');

      return 0 <= difference && difference <= 30;
    };

    $scope.isExpiredRating = function(item) {
      return moment(new Date()).isAfter(item.lastDocDateValidTo);
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
}(angular, moment));