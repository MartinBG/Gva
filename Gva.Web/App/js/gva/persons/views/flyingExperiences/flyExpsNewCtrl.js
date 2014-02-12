/*global angular*/
(function (angular) {
  'use strict';

  function FlyingExperiencesNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonFlyingExperience,
    personFlyingExperience
  ) {
    $scope.personFlyingExperience = personFlyingExperience;
    $scope.isEdit = false;

    $scope.save = function () {
      $scope.personFlyingExperienceForm.$validate()
        .then(function () {
          if ($scope.personFlyingExperienceForm.$valid) {
            return PersonFlyingExperience
              .save({ id: $stateParams.id }, $scope.personFlyingExperience)
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

  FlyingExperiencesNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonFlyingExperience',
    'personFlyingExperience'
  ];

  FlyingExperiencesNewCtrl.$resolve = {
    personFlyingExperience: function () {
      return {};
    }
  };

  angular.module('gva').controller('FlyingExperiencesNewCtrl', FlyingExperiencesNewCtrl);
}(angular));
