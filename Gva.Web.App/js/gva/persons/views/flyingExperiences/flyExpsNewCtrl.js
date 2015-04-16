/*global angular*/
(function (angular) {
  'use strict';

  function FlyingExperiencesNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonFlyingExperiences,
    personFlyingExperience
  ) {
    $scope.personFlyingExperience = personFlyingExperience;
    $scope.caseTypeId = $stateParams.caseTypeId;
    $scope.lotId = $stateParams.id;

    $scope.save = function () {
      return $scope.newFlyingExperienceForm.$validate()
        .then(function () {
          if ($scope.newFlyingExperienceForm.$valid) {
            return PersonFlyingExperiences
              .save({ id: $scope.lotId }, $scope.personFlyingExperience)
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
    'PersonFlyingExperiences',
    'personFlyingExperience'
  ];

  FlyingExperiencesNewCtrl.$resolve = {
    personFlyingExperience: [
      '$stateParams',
      'PersonFlyingExperiences',
      function ($stateParams, PersonFlyingExperiences) {
        return PersonFlyingExperiences.newFlyingExperience({
          id: $stateParams.id
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('FlyingExperiencesNewCtrl', FlyingExperiencesNewCtrl);
}(angular));
