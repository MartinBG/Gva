/*global angular*/
(function (angular) {
  'use strict';

  function PrintRatingEditionModalCtrl(
    $scope,
    $modalInstance,
    scModalParams
  ) {
    $scope.model = {
      lotId: scModalParams.lotId,
      licenceIndex: scModalParams.licenceIndex,
      licenceEditionIndex: scModalParams.licenceEditionIndex,
      ratingIndex: scModalParams.ratingIndex,
      ratingEditionIndex: scModalParams.ratingEditionIndex
    };

    $scope.close = function () {
      return $modalInstance.close();
    };
  }

  PrintRatingEditionModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams'
  ];

  angular.module('gva').controller('PrintRatingEditionModalCtrl', PrintRatingEditionModalCtrl);
}(angular));
