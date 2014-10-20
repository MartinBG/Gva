/*global angular*/
(function (angular) {
  'use strict';

  function NewRatingModalCtrl(
    $scope,
    $modalInstance,
    PersonRatings,
    PersonRatingEditions,
    scModalParams,
    newRating
  ) {
    $scope.form = {};
    $scope.newRating = newRating;
    $scope.newEdition = null;
    $scope.lotId = scModalParams.lotId;

    $scope.$watch('newRating.case.caseType', function () {
      if ($scope.newRating['case'] && $scope.newRating['case'].caseType) {
        PersonRatingEditions.newRatingEdition({
          id: $scope.lotId,
          appId: scModalParams.appId,
          caseTypeId: newRating['case'].caseType.nomValueId
        }).$promise.then(function (edition) {
          $scope.newEdition = edition;
        });
      }
    });

    $scope.save = function () {
      return $scope.form.newRatingForm.$validate()
        .then(function () {
          if ($scope.form.newRatingForm.$valid) {
            return PersonRatings.save(
              { id: $scope.lotId },
              {
                rating: $scope.newRating,
                edition: $scope.newEdition
              }).$promise.then(function (savedRating) {
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
    'PersonRatingEditions',
    'scModalParams',
    'newRating'
  ];

  NewRatingModalCtrl.$resolve = {
    newRating: [
      'PersonRatings',
      'scModalParams',
      function (PersonRatings, scModalParams) {
        return PersonRatings.newRating({
          id: scModalParams.lotId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('NewRatingModalCtrl', NewRatingModalCtrl);
}(angular));
