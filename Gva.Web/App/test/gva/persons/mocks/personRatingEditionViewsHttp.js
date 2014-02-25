/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/ratings/:ind/editionViews',
        function ($params, personLots) {
          var person = _(personLots)
           .filter({ lotId: parseInt($params.id, 10) }).first();

          var rating = _(person.personRatings)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          var personRatingEditions = _(rating.personRatingEditions)
          .forEach(function (re) {
            re.rating = rating.part;
            re.firstEditionValidFrom = rating.personRatingEditions[0].part.documentDateValidFrom;

            re.rating.ratingTypeOrRatingLevel = re.rating.personRatingLevel ?
              re.rating.personRatingLevel : re.rating.ratingType;

            var subClasses = _(re.part.ratingSubClasses)
            .pluck('name')
            .reduce(function (l, r) { return l + ', ' + r; });

            re.rating.classOrSubclass = re.rating.aircraftTypeGroup ?
              re.rating.aircraftTypeGroup.name : re.rating.ratingClass.name + ' ' + subClasses;

            re.rating.authorizationAndLimitations = re.rating.authorization ?
              re.rating.authorization.name : '';

            var limitations = _(re.part.limitations)
            .pluck('name')
            .reduce(function (l, r) { return l + ', ' + r; });
            re.ratingEdition = {};
            re.ratingEdition.partIndex = re.partIndex;
            re.ratingEdition.part = re.part;
            re.rating.authorizationAndLimitations += ' ' + limitations;
          }).value();

          return [200, personRatingEditions];
        });
  });
}(angular, _));