/*global angular*/
(function (angular) {
  'use strict';

  function PersonFlyingExperienceCtrl($scope, scFormParams, PersonFlyingExperiences) {
    $scope.isNew = scFormParams.isNew;
    $scope.caseTypeId = scFormParams.caseTypeId;

    $scope.sum = function () {
      if ($scope.model.part.sector && $scope.model.part.locationIndicator) {
        PersonFlyingExperiences.sumAllFlightHours({
          id: scFormParams.lotId,
          partIndex: scFormParams.partIndex,
          sector: $scope.model.part.sector,
          locationIndicatorId: $scope.model.part.locationIndicator.nomValueId
        })
          .$promise
          .then(function(result) {
            $scope.model.part.total = result.total + ($scope.model.part.totalDoc || 0);
          });
      } else {
        $scope.model.part.total = $scope.model.part.totalDoc || 0;
      }
      
    };
  }

  PersonFlyingExperienceCtrl.$inject = ['$scope', 'scFormParams', 'PersonFlyingExperiences'];

  angular.module('gva').controller('PersonFlyingExperienceCtrl', PersonFlyingExperienceCtrl);
}(angular));
