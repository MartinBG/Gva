/*global angular, _*/
(function (angular, _) {
  'use strict';

  function RatingsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonRatings,
    rating
  ) {
    var originalRating = _.cloneDeep(rating);
    $scope.rating = rating;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.ratingPartIndex = $stateParams.ind;

    $scope.edit = function () {
      $scope.editRatingMode = 'edit';
    };

    $scope.newEdition = function () {
      return $state.go('root.persons.view.ratings.edit.editions.new');
    };

    $scope.save = function () {
      return $scope.editRatingForm.$validate().then(function () {
        if ($scope.editRatingForm.$valid) {
          return PersonRatings
            .save({
              id: $scope.lotId,
              ind: $scope.ratingPartIndex,
              index: $stateParams.index
            }, $scope.rating, $scope.caseTypeId)
            .$promise
            .then(function (savedRating) {
              $scope.editRatingMode = null;
              originalRating = _.cloneDeep(savedRating);
            });
        }
      });
    };

    $scope.cancel = function () {
      $scope.editRatingMode = null;
      $scope.rating = _.cloneDeep(originalRating);
    };
  }

  RatingsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonRatings',
    'rating'
  ];

  RatingsEditCtrl.$resolve = {
    rating: [
      '$stateParams',
      'PersonRatings',
      function ($stateParams, PersonRatings) {
        return PersonRatings.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('RatingsEditCtrl', RatingsEditCtrl);
}(angular, _));
