/*global angular*/
(function (angular) {
  'use strict';

  function RatingsNewCtrl($scope, $state, $stateParams, PersonRating) {
    $scope.save = function () {
      $scope.newRatingForm.$validate()
       .then(function () {
        if ($scope.newRatingForm.$valid) {
          return PersonRating
            .save({ id: $stateParams.id }, $scope.model).$promise
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

  RatingsNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonRating'];

  angular.module('gva').controller('RatingsNewCtrl', RatingsNewCtrl);
}(angular));