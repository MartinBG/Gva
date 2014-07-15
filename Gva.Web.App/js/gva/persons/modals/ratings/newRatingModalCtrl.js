/*global angular*/
(function (angular) {
  'use strict';

  function NewRatingModalCtrl(
    $scope,
    $modalInstance,
    $stateParams,
    PersonRatings,
    rating
  ) {
    $scope.form = {};
    $scope.rating = rating;

    $scope.save = function () {
      return $scope.form.newRatingForm.$validate()
        .then(function () {
          if ($scope.form.newRatingForm.$valid) {
            return PersonRatings
              .save({ id: $stateParams.id }, $scope.rating)
              .$promise
              .then(function (savedRating) {
                return $modalInstance.close(savedRating.partIndex);
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
    '$stateParams',
    'PersonRatings',
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
