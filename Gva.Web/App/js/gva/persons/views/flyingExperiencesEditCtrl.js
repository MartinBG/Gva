/*global angular*/
(function (angular) {
  'use strict';

  function FlyingExperiencesEditCtrl($scope, $state, $stateParams, PersonFlyingExperience) {
    PersonFlyingExperience
      .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
      .then(function (flyingExperience) {
        $scope.personFlyingExperience = flyingExperience;
      });

    $scope.save = function () {
      $scope.personFlyingExperienceForm.$validate()
        .then(function (){
          if ($scope.personFlyingExperienceForm.$valid) {
            return PersonFlyingExperience
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personFlyingExperience)
              .$promise
              .then(function () {
                return $state.go('persons.flyingExperiences.search');
              });
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
