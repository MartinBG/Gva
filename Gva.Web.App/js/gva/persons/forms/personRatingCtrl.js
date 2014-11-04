/*global angular*/
(function (angular) {
  'use strict';

  function PersonRatingCtrl($scope, scFormParams, PersonRatings) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;
    $scope.lotId = scFormParams.lotId;
    $scope.ratingPartIndex = scFormParams.ratingPartIndex;

    $scope.isValid = function () {
      return PersonRatings.isValid({
        id: $scope.lotId,
        ratingPartIndex: $scope.model.partIndex
      }, $scope.model.part)
        .$promise
        .then(function (result) {
          return result.isValid;
        });
    };
  }

  PersonRatingCtrl.$inject = ['$scope', 'scFormParams', 'PersonRatings'];

  angular.module('gva').controller('PersonRatingCtrl', PersonRatingCtrl);
}(angular));
