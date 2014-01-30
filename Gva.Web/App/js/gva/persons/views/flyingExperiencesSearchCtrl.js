/*global angular*/
(function (angular) {
  'use strict';

  function FlyingExperiencesSearchCtrl($scope, $state, $stateParams, PersonFlyingExperience) {
    PersonFlyingExperience.query($stateParams).$promise.then(function (flyingExperiences) {
      $scope.flyingExperiences = flyingExperiences;
    });

    $scope.editFlyingExperience = function (flyingExperience) {
      return $state.go(
        'persons.flyingExperiences.edit',
        { id: $stateParams.id, ind: flyingExperience.partIndex }
      );
    };

    $scope.deleteFlyingExperience = function (flyingExperience) {
      return PersonFlyingExperience.remove({ id: $stateParams.id, ind: flyingExperience.partIndex })
        .$promise.then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.newFlyingExperience = function () {
      return $state.go('persons.flyingExperiences.new');
    };
  }

  FlyingExperiencesSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonFlyingExperience'
  ];

  angular.module('gva').controller('FlyingExperiencesSearchCtrl', FlyingExperiencesSearchCtrl);
}(angular));