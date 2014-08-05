/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseRatingsModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    ratings
  ) {
    $scope.selectedRatings = [];

    $scope.ratings = _.filter(ratings, function (rating) {
      return !_.contains(scModalParams.includedRatings, rating.partIndex);
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
    'scModalParams',
    'ratings'
  ];

  ChooseRatingsModalCtrl.$resolve = {
    ratings: [
      'PersonRatings',
      'scModalParams',
      function (PersonRatings, scModalParams) {
        return PersonRatings.query({ id: scModalParams.lotId }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseRatingsModalCtrl', ChooseRatingsModalCtrl);
}(angular, _, $));
