/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseRatingsModalCtrl(
    $scope,
    $modalInstance,
    ratings,
    includedRatings
  ) {
    $scope.selectedRatings = [];

    $scope.ratings = _.filter(ratings, function (rating) {
      return !_.contains(includedRatings, rating.partIndex);
    });

    $scope.addRatings = function () {
      return $modalInstance.close($scope.selectedRatings);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectRating = function (event, ratingId) {
      if ($(event.target).is(':checked')) {
        $scope.selectedRatings.push(ratingId);
      }
      else {
        $scope.selectedRatings = _.without($scope.selectedRatings, ratingId);
      }
    };
  }

  ChooseRatingsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'ratings',
    'includedRatings'
  ];

  ChooseRatingsModalCtrl.$resolve = {
    ratings: [
      '$stateParams',
      'PersonRatings',
      function ($stateParams, PersonRatings) {
        return PersonRatings.query({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseRatingsModalCtrl', ChooseRatingsModalCtrl);
}(angular, _, $));
