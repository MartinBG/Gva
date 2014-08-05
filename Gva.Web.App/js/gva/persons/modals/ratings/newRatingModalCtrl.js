/*global angular*/
(function (angular) {
  'use strict';

  function NewRatingModalCtrl(
    $scope,
    $modalInstance,
    PersonRatings,
    scModalParams,
    rating
  ) {
    $scope.form = {};
    $scope.rating = rating;
    $scope.lotId = scModalParams.lotId;

    $scope.save = function () {
      return $scope.form.newRatingForm.$validate()
        .then(function () {
          if ($scope.form.newRatingForm.$valid) {
            return PersonRatings
              .save({ id: $scope.lotId }, $scope.rating)
              .$promise
              .then(function (savedRating) {
                return $modalInstance.close(savedRating);
              });
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewRatingModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonRatings',
    'scModalParams',
    'rating'
  ];

  NewRatingModalCtrl.$resolve = {
    rating: function () {
      return {
        part: {
          editions: [{}]
        }
      };
    }
  };

  angular.module('gva').controller('NewRatingModalCtrl', NewRatingModalCtrl);
}(angular));
