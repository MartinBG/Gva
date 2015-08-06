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
      if (flyingExperience.totalDoc) {
        minutes = flyingExperience.totalDoc / 60000;
        flyingExperience.totalDocHours = Math.floor(minutes / 60);
      } else {
        flyingExperience.totalDocHours = 0;
      }
      if (flyingExperience.total) {
        minutes = flyingExperience.total / 60000;
        flyingExperience.totalHours = Math.floor(minutes / 60);
      } else {
        flyingExperience.totalHours = 0;
      }

      var location = (flyingExperience.locationIndicator ?
        flyingExperience.locationIndicator.code : '') + ' ' +
        (flyingExperience.sector ? flyingExperience.sector : '');
      var ratingTypes = _.pluck(flyingExperience.ratingTypes, 'code').join(', ');

      flyingExperience.ratingTypesAndLocation = 
        flyingExperience.ratingTypes && flyingExperience.ratingTypes.length > 0 ?
        ratingTypes : location;
      return flyingExperience;
    }), function (flyExp) {
      var classCode = flyExp.ratingClass ? flyExp.ratingClass.code : '';
      var authorizationCode = flyExp.authorization ? flyExp.authorization.code : '';
      var licenceCode = flyExp.licenceType ? flyExp.licenceType.code : '';
      return flyExp.ratingTypesAndLocation + ' ' +
        classCode + ' ' +
        authorizationCode + ' ' +
        licenceCode;
    });

    $scope.flyingExperiences = [].concat.apply([],
      _.map(groupedFlyingExp, function (value, key) {
        return _.sortBy(groupedFlyingExp[key], function(value) {
          return new Date(value.documentDate);
        }).reverse();
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