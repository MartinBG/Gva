﻿/*global angular*/
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

    $scope.save = function () {
      return $scope.newFlyingExperienceForm.$validate()
        .then(function () {
          if ($scope.newFlyingExperienceForm.$valid) {
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