/*global angular*/
(function (angular) {
  'use strict';

  function FlyingExperiencesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    flyingExperiences
  ) {
    $scope.flyingExperiences = flyingExperiences;
  }

  FlyingExperiencesSearchCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'flyingExperiences'
  ];

  FlyingExperiencesSearchCtrl.$resolve = {
    flyingExperiences: [
      '$stateParams',
      'PersonFlyingExperiences',
      function ($stateParams, PersonFlyingExperiences) {
        return PersonFlyingExperiences.query($stateParams).$promise;
      }
    ]
  };

  angular.module('gva').controller('FlyingExperiencesSearchCtrl', FlyingExperiencesSearchCtrl);
}(angular));