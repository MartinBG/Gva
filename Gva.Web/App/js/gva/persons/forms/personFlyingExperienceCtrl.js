/*global angular*/
(function (angular) {
  'use strict';

  function PersonFlyingExperienceCtrl($scope, $stateParams, PersonFlyingExperience) {
    $scope.isPositive = function (value) {
      return (value >= 0 ? true : false);
    };

    var checkValue = function (param, value) {
      if (!value || !$stateParams.id) {
        return true;
      }

      return PersonFlyingExperience.query({
        id: $stateParams.id,
        organization: param === 'organization' ? value.nomTypeValueId : undefined,
        aircraft: param === 'aircraft' ? value.nomTypeValueId : undefined,
        ratingType: param === 'ratingType' ? value.nomTypeValueId : undefined,
        ratingClass: param === 'ratingClass' ? value.nomTypeValueId : undefined,
        authorization: param === 'authorization' ? value.nomTypeValueId : undefined,
        licenceType: param === 'licenceType' ? value.nomTypeValueId : undefined,
        locationIndicator: param === 'locationIndicator' ? value.nomTypeValueId : undefined,
        sector: param === 'sector' ? value : undefined,
        experienceRole: param === 'experienceRole' ? value.nomTypeValueId : undefined,
        experienceMeasure: param === 'experienceMeasure' ? value.nomTypeValueId : undefined
      }).$promise
        .then(function (flyingExperiences) {
          return flyingExperiences.length === 0 ||
            (flyingExperiences.length === 1 &&
            flyingExperiences[0].partIndex === parseInt($stateParams.ind, 10));
        });
    };

    $scope.isUniqueOrganization = function (value) {
      return checkValue('organization', value);
    };

    $scope.isUniqueAircraft = function (value) {
      return checkValue('aircraft', value);
    };

    $scope.isUniqueRatingType = function (value) {
      return checkValue('ratingType', value);
    };

    $scope.isUniqueRatingClass = function (value) {
      return checkValue('ratingClass', value);
    };

    $scope.isUniqueAuthorization = function (value) {
      return checkValue('authorization', value);
    };

    $scope.isUniqueLicenceType = function (value) {
      return checkValue('licenceType', value);
    };

    $scope.isUniqueLocationIndicator = function (value) {
      return checkValue('locationIndicator', value);
    };

    $scope.isUniqueSector = function (value) {
      return checkValue('sector', value);
    };

    $scope.isUniqueExperienceRole = function (value) {
      return checkValue('experienceRole', value);
    };

    $scope.isUniqueExperienceMeasure = function (value) {
      return checkValue('experienceMeasure', value);
    };

  }

  PersonFlyingExperienceCtrl.$inject = ['$scope', '$stateParams', 'PersonFlyingExperience'];

  angular.module('gva').controller('PersonFlyingExperienceCtrl', PersonFlyingExperienceCtrl);
}(angular));
