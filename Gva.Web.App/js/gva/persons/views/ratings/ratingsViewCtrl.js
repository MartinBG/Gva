/*global angular*/
(function (angular) {
  'use strict';

  function RatingsViewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonRatings,
    rating
  ) {
    $scope.rating = rating;
    $scope.lotId = $stateParams.id;

    $scope.newEdition = function () {
      return $state.go('root.persons.view.ratings.view.editions.new');
    };

  }

  RatingsViewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonRatings',
    'rating'
  ];

  RatingsViewCtrl.$resolve = {
    rating: [
      '$stateParams',
      'PersonRatings',
      function ($stateParams, PersonRatings) {
        return PersonRatings.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('RatingsViewCtrl', RatingsViewCtrl);
}(angular));
