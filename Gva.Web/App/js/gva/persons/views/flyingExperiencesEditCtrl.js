/*global angular*/
(function (angular) {
  'use strict';

  function FlyingExperiencesEditCtrl(
    $scope,
    $state,
    $stateParams,
    PersonFlyingExperience,
    personFlyingExperience
  ) {
    $scope.isEdit = true;
    $scope.personFlyingExperience = personFlyingExperience;

    $scope.save = function () {
      $scope.personFlyingExperienceForm.$validate()
        .then(function () {
          if ($scope.personFlyingExperienceForm.$valid) {
            return PersonFlyingExperience
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personFlyingExperience)
              .$promise
              .then(function () {
                return $state.go('root.persons.view.flyingExperiences.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.flyingExperiences.search');
    };
  }

  FlyingExperiencesEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonFlyingExperience',
    'personFlyingExperience'
  ];

  FlyingExperiencesEditCtrl.$resolve = {
    personFlyingExperience: [
      '$stateParams',
      'PersonFlyingExperience',
      function ($stateParams, PersonFlyingExperience) {
        return PersonFlyingExperience.get({
          id: $stateParams.id,
          ind: $stateParams.ind
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('FlyingExperiencesEditCtrl', FlyingExperiencesEditCtrl);
}(angular));
