/*global angular*/
(function (angular) {
  'use strict';

  function FlyingExperiencesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    PersonFlyingExperience,
    flyingExperiences
  ) {
    $scope.flyingExperiences = flyingExperiences;

    $scope.editFlyingExperience = function (flyingExperience) {
      return $state.go(
        'root.persons.view.flyingExperiences.edit',
        { id: $stateParams.id, ind: flyingExperience.partIndex }
      );
    };

    $scope.newFlyingExperience = function () {
      return $state.go('root.persons.view.flyingExperiences.new');
    };
  }

  FlyingExperiencesSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonFlyingExperience',
    'flyingExperiences'
  ];

  FlyingExperiencesSearchCtrl.$resolve = {
    flyingExperiences: [
      '$stateParams',
      'PersonFlyingExperience',
      function ($stateParams, PersonFlyingExperience) {
        return PersonFlyingExperience.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('FlyingExperiencesSearchCtrl', FlyingExperiencesSearchCtrl);
}(angular));