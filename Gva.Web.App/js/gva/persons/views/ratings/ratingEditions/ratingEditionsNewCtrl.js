/*global angular*/
(function (angular) {
  'use strict';

  function RatingEditionsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonRatings,
    PersonRatingEditions,
    rating,
    newRatingEdition
  ) {
    $scope.rating = rating;
    $scope.newRatingEdition = newRatingEdition;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newRatingEditionForm.$validate().then(function () {
        if ($scope.newRatingEditionForm.$valid) {
          return PersonRatingEditions
            .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.newRatingEdition).$promise
            .then(function (edition) {
              return $state.go(
                'root.persons.view.ratings.edit.editions.edit',
                { index: edition.partIndex });
            });
        }
      });
    };

    $scope.cancel = function () {
      return PersonRatings.lastEditionIndex($stateParams).$promise.then(function (index) {
        return $state.go(
          'root.persons.view.ratings.edit.editions.edit',
          { index: index.lastIndex });
      });
    };
  }

  RatingEditionsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonRatings',
    'PersonRatingEditions',
    'rating',
    'newRatingEdition'
  ];

  RatingEditionsNewCtrl.$resolve = {
    newRatingEdition: [
      '$stateParams',
      'PersonRatingEditions',
      function ($stateParams, PersonRatingEditions) {
        return PersonRatingEditions.newRatingEdition({
          id: $stateParams.id,
          ind: $stateParams.ind,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('RatingEditionsNewCtrl', RatingEditionsNewCtrl);
}(angular));
