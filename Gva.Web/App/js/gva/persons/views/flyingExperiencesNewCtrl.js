/*global angular*/
(function (angular) {
  'use strict';

  function FlyingExperiencesNewCtrl($scope, $state, $stateParams, PersonFlyingExperience) {


    var checkUniqueness = function () {

      return PersonFlyingExperience.query({
        id: $stateParams.id,
        organization: $scope.personFlyingExperience.part.organization.nomTypeValueId,
        aircraft: $scope.personFlyingExperience.part.aircraft.nomTypeValueId,
        ratingType: $scope.personFlyingExperience.part.ratingType.nomTypeValueId,
        ratingClass: $scope.personFlyingExperience.part.ratingClass.nomTypeValueId,
        authorization: $scope.personFlyingExperience.part.authorization.nomTypeValueId,
        licenceType: $scope.personFlyingExperience.part.licenceType.nomTypeValueId,
        locationIndicator: $scope.personFlyingExperience.part.locationIndicator.nomTypeValueId,
        sector: $scope.personFlyingExperience.part.sector,
        experienceRole: $scope.personFlyingExperience.part.experienceRole.nomTypeValueId,
        experienceMeasure: $scope.personFlyingExperience.part.experienceMeasure.nomTypeValueId
      }).$promise
        .then(function (flyingExperiences) {
          return flyingExperiences.length === 0;
        });
    };

    $scope.isUnique = true;

    $scope.save = function () {
      checkUniqueness().then(function (value) {
        if (value) {
          return PersonFlyingExperience
            .save({ id: $stateParams.id }, $scope.personFlyingExperience).$promise
            .then(function () {
              return $state.go('persons.flyingExperiences.search');
            });
        } else {
          $scope.isUnique = false;
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('persons.flyingExperiences.search');
    };
  }

  FlyingExperiencesNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonFlyingExperience'];

  angular.module('gva').controller('FlyingExperiencesNewCtrl', FlyingExperiencesNewCtrl);
}(angular));
