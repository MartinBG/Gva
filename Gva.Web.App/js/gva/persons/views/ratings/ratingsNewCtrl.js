/*global angular*/
(function (angular) {
  'use strict';

  function RatingsNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonRatings,
    PersonRatingEditions,
    newRating
  ) {
    $scope.newRating = newRating;
    $scope.newEdition = null;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.appId = $stateParams.appId;

     $scope.$watch('newRating.case.caseType', function () {
       if ($scope.newRating['case'] && $scope.newRating['case'].caseType) {
         PersonRatingEditions.newRatingEdition({
           id: $scope.lotId,
           appId: $scope.appId,
           caseTypeId: newRating['case'].caseType.nomValueId
         }).$promise.then(function (edition) {
           $scope.newEdition = edition;
         });
       }
     });

    $scope.save = function () {
      return $scope.newRatingForm.$validate().then(function () {
          if ($scope.newRatingForm.$valid) {
            return PersonRatings.save({ id: $stateParams.id }, {
              rating: $scope.newRating,
              edition: $scope.newEdition
            }).$promise
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
    'PersonRatings',
    'PersonRatingEditions',
    'newRating'
  ];

  RatingsNewCtrl.$resolve = {
    newRating: [
      '$stateParams',
      'PersonRatings',
      function ($stateParams, PersonRatings) {
        return PersonRatings.newRating({
          id: $stateParams.id,
          appId: $stateParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('RatingsNewCtrl', RatingsNewCtrl);
}(angular));
