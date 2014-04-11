/*global angular, _*/
(function (angular, _) {
  'use strict';

  function RatingsEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonRating,
    rating
  ) {
    var originalRating = _.cloneDeep(rating);
    $scope.rating = rating;
    $scope.editMode = null;

    $scope.$watch('rating.part.editions | last', function (lastEdition) {
      $scope.currentEdition = lastEdition;
      $scope.lastEdition = lastEdition;
    });

    $scope.selectEdition = function (item) {
      $scope.currentEdition = item;
    };

    $scope.newEdition = function () {
      $scope.rating.part.editions.push({});

      $scope.editMode = 'edit';
    };

    $scope.editLastEdition = function () {
      $scope.editMode = 'edit';
    };

    $scope.deleteLastEdition = function () {
      $scope.rating.part.editions.pop();

      if ($scope.rating.part.editions.length === 0) {
        return PersonRating
          .remove({ id: $stateParams.id, ind: $stateParams.ind })
          .$promise.then(function () {
            return $state.go('root.persons.view.ratings.search');
          });
      }
      else {
        return PersonRating.save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.rating)
          .$promise.then(function () {
            originalRating = _.cloneDeep($scope.rating);
          });
      }
    };

    $scope.save = function () {
      return $scope.editRatingForm.$validate()
        .then(function () {
          if ($scope.editRatingForm.$valid) {
            $scope.editMode = 'saving';

            return PersonRating
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.rating).$promise
              .then(function () {
                $scope.editMode = null;
                originalRating = _.cloneDeep($scope.rating);
              }, function () {
                $scope.editMode = 'edit';
              });
          }
        });
    };

    $scope.cancel = function () {
      $scope.rating = _.cloneDeep(originalRating);
      $scope.editMode = null;
    };
  }

  RatingsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonRating',
    'rating'
  ];

  RatingsEditCtrl.$resolve = {
    rating: [
      '$stateParams',
      'PersonRating',
      function ($stateParams, PersonRating) {
        return PersonRating.get($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('RatingsEditCtrl', RatingsEditCtrl);
}(angular, _));