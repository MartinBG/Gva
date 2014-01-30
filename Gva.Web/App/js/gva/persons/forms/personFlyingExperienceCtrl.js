/*global angular*/
(function (angular) {
  'use strict';

  function PersonFlyingExperienceCtrl($scope, $stateParams, PersonFlyingExperience) {
    $scope.checkUniqueness = function () {
      return PersonFlyingExperience.query({
        id: $stateParams.id,
        organization: $scope.model.organization.nomTypeValueId,
        aircraft: $scope.model.aircraft.nomTypeValueId,
        ratingType: $scope.model.ratingType.nomTypeValueId,
        ratingClass: $scope.model.ratingClass.nomTypeValueId,
        authorization: $scope.model.authorization.nomTypeValueId,
        licenceType: $scope.model.licenceType.nomTypeValueId,
        locationIndicator: $scope.model.locationIndicator.nomTypeValueId,
        sector: $scope.model.sector,
        month: $scope.model.period.month,
        year: $scope.model.period.year,
        experienceRole: $scope.model.experienceRole.nomTypeValueId,
        experienceMeasure: $scope.model.experienceMeasure.nomTypeValueId
      }).$promise
        .then(function (flyingExperiences) {
          return flyingExperiences.length === 0;
        });
    };
    $scope.saveIsClicked = false;
  }

  PersonFlyingExperienceCtrl.$inject = ['$scope', '$stateParams', 'PersonFlyingExperience'];

  angular.module('gva').controller('PersonFlyingExperienceCtrl', PersonFlyingExperienceCtrl);
}(angular));