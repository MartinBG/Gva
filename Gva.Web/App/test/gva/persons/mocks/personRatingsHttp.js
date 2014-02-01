/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/ratings',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personRatings = _.map(person.personRatings, function (rating) {
            return {
              partIndex: rating.partIndex,
              rating: rating.part,
              ratingEdition: rating.personRatingEditions[rating.personRatingEditions.length - 1],
              firstEditionValidFrom: rating.personRatingEditions[0].part.documentDateValidFrom
            };
          });

          return [200, personRatings];
        })
      .when('POST', '/api/persons/:id/ratings',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personRating = {
            partIndex: person.nextIndex++,
            part: $jsonData.rating.part,
            personRatingEditions: [{
              partIndex: person.nextIndex++,
              part: $jsonData.ratingEdition.part
            }]
          };

          person.personRatings.push(personRating);

          return [200];
        });
  });
}(angular, _));