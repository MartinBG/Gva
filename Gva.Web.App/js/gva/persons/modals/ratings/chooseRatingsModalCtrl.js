/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseRatingsModalCtrl(
    $scope,
    $modalInstance,
    PersonRatings,
    scModalParams,
    ratings
  ) {
    $scope.selectedRatings = [];

    $scope.filterRatings = function (ratings) {
      return _.filter(ratings, function (rating) {
        return !_.contains(scModalParams.includedRatings, rating.partIndex);
      });
    };

    $scope.ratings = $scope.filterRatings(ratings);

    $scope.addRatings = function () {
      return $modalInstance.close($scope.selectedRatings);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.showAllRatings = function (event) {
      PersonRatings.getRatingsByValidity({ 
        id: scModalParams.lotId,
        valid: !$(event.target).is(':checked')
      }).$promise.then(function (allRatings) {
        $scope.ratings =  $scope.filterRatings(allRatings);
      });
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
    'PersonRatings',
    'scModalParams',
    'ratings'
  ];

  ChooseRatingsModalCtrl.$resolve = {
    ratings: [
      'PersonRatings',
      'scModalParams',
      function (PersonRatings, scModalParams) {
        return PersonRatings.getRatingsByValidity({ id: scModalParams.lotId }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseRatingsModalCtrl', ChooseRatingsModalCtrl);
}(angular, _, $));
