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

    $scope.selectRating = function (event, rating) {
      if ($(event.target).is(':checked')) {
        $scope.selectedRatings.push(rating);
      }
      else {
        $scope.selectedRatings = _.without($scope.selectedRatings, rating);
      }
    };
  }

  ChooseRatingsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'ratings',
    'includedRatings'
  ];

  angular.module('gva').controller('ChooseRatingsModalCtrl', ChooseRatingsModalCtrl);
}(angular, _, $));
