/*global angular*/
(function (angular) {
  'use strict';

  function FlyingExperiencesNewCtrl($scope, $state, $stateParams, PersonFlyingExperience) {
    $scope.isEdit = false;

    $scope.save = function () {
      $scope.personFlyingExperienceForm.saveIsClicked = true;
      $scope.personFlyingExperienceForm.$validate()
        .then(function () {
          if ($scope.personFlyingExperienceForm.$valid) {
            return PersonFlyingExperience
              .save({ id: $stateParams.id }, $scope.personFlyingExperience)
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

  FlyingExperiencesNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonFlyingExperience'];

  angular.module('gva').controller('FlyingExperiencesNewCtrl', FlyingExperiencesNewCtrl);
}(angular));
