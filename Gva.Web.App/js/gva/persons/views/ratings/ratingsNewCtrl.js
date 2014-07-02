﻿/*global angular*/
(function (angular) {
  'use strict';

  function RatingsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonRating,
    rating
  ) {
    $scope.rating = rating;

    $scope.save = function () {
      return $scope.newRatingForm.$validate()
        .then(function () {
          if ($scope.newRatingForm.$valid) {
            return PersonRating
              .save({ id: $stateParams.id }, $scope.rating).$promise
              .then(function () {
                return $state.go('root.persons.view.ratings.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.ratings.search');
    };
  }

  RatingsNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonRating',
    'rating'
  ];

  RatingsNewCtrl.$resolve = {
    rating: [
      'application',
      'Nomenclatures',
      function (application, Nomenclatures) {
        return Nomenclatures
          .get({ alias: 'caa', valueAlias: 'BG' })
          .$promise
          .then(function (caa) {
            if (application) {
              return {
                part: {
                  caa: caa,
                  editions: [{ applications: [application] }]
                }
              };
            } else {
              return {
                part: {
                  caa: caa,
                  editions: [{}]
                }
              };
            }
          });
      }
    ]
  };

  angular.module('gva').controller('RatingsNewCtrl', RatingsNewCtrl);
}(angular));
