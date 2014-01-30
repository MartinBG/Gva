/*global angular*/
(function (angular) {
  'use strict';

  function PersonFlyingExperienceCtrl($scope) {
    //TODO: how to check uniqueness
    //$scope.checkUniqueness = function () {
    //  return PersonFlyingExperience.query({
    //    id: $stateParams.id,
    //    organization: $scope.model.organization ?
    //      $scope.model.organization.nomTypeValueId : undefined,
    //    aircraft: $scope.model.aircraft ?
    //      $scope.model.aircraft.nomTypeValueId : undefined,
    //    ratingType: $scope.model.ratingType ?
    //      $scope.model.ratingType.nomTypeValueId : undefined,
    //    ratingClass: $scope.model.ratingClass ?
    //      $scope.model.ratingClass.nomTypeValueId : undefined,
    //    authorization: $scope.model.authorization ?
    //      $scope.model.authorization.nomTypeValueId : undefined,
    //    licenceType: $scope.model.licenceType ?
    //      $scope.model.licenceType.nomTypeValueId : undefined,
    //    locationIndicator:$scope.model.locationIndicator ?
    //      $scope.model.locationIndicator.nomTypeValueId : undefined,
    //    sector: $scope.model.sector,
    //    month: $scope.model.period ?
    //      $scope.model.period.month : undefined,
    //    year: $scope.model.period ?
    //      $scope.model.period.year : undefined,
    //    experienceRole:$scope.model.experienceRole ?
    //      $scope.model.experienceRole.nomTypeValueId : undefined,
    //    experienceMeasure:$scope.model.experienceMeasure ?
    //      $scope.model.experienceMeasure.nomTypeValueId : undefined
    //  }).$promise
    //    .then(function (flyingExperiences) {
    //      return flyingExperiences.length === 0;
    //    });
    //};
    $scope.saveClicked = false;
  }

  PersonFlyingExperienceCtrl.$inject = ['$scope', '$stateParams', 'PersonFlyingExperience'];

  angular.module('gva').controller('PersonFlyingExperienceCtrl', PersonFlyingExperienceCtrl);
}(angular));