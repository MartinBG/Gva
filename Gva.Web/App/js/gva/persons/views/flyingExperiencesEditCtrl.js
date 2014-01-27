/*global angular*/
(function (angular) {
  'use strict';

  function FlyingExperiencesEditCtrl($scope, $state, $stateParams, PersonFlyingExperience) {
    PersonFlyingExperience
      .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
      .then(function (flyingExperience) {
        $scope.personFlyingExperience = flyingExperience;
      });

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
        month: $scope.personFlyingExperience.part.period.month,
        year: $scope.personFlyingExperience.part.period.year,
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

  FlyingExperiencesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonFlyingExperience'
  ];

  angular.module('gva').controller('FlyingExperiencesEditCtrl', FlyingExperiencesEditCtrl);
}(angular));
