/*global angular, _*/
(function (angular, _) {
  'use strict';

  function FlyingExperiencesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    flyingExperiences
  ) {
    $scope.flyingExperiences = _.map(flyingExperiences, function (flyingExperience) {
      var minutes;
      if (flyingExperience.part.totalDoc) {
        minutes = flyingExperience.part.totalDoc / 60000;
        flyingExperience.totalDocHours = Math.floor(minutes / 60);
      } else {
        flyingExperience.totalDocHours = 0;
      }
      if (flyingExperience.part.total) {
        minutes = flyingExperience.part.total / 60000;
        flyingExperience.totalHours = Math.floor(minutes / 60);
      } else {
        flyingExperience.totalHours = 0;
      }
      return flyingExperience;
    });
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
}(angular, _));