/*global angular, _*/
(function (angular, _) {
  'use strict';

  function FlyingExperiencesSearchCtrl(
    $scope,
    $state,
    $stateParams,
    flyingExperiences
  ) {
    var groupedFlyingExp = _.groupBy(_.map(flyingExperiences, function (flyingExperience) {
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

      var location = (flyingExperience.part.locationIndicator ?
        flyingExperience.part.locationIndicator.code : '') + ' ' +
        (flyingExperience.part.sector ? flyingExperience.part.sector : '');
      var ratingTypes = _.pluck(flyingExperience.part.ratingTypes, 'code').join(', ');

      flyingExperience.ratingTypesAndLocation = 
        flyingExperience.part.ratingTypes && flyingExperience.part.ratingTypes.length > 0 ?
        ratingTypes : location;
      return flyingExperience;
    }), 'ratingTypesAndLocation');
    $scope.flyingExperiences = [].concat.apply([], _.map(groupedFlyingExp, function (value, key) {
      return _.sortBy(groupedFlyingExp[key], 'part.documentDate').reverse();
    }));
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