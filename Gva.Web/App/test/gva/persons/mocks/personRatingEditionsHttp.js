/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/ratings/:ind/editions',
        function ($params, personLots) {
          var person = _(personLots)
           .filter({ lotId: parseInt($params.id, 10) }).first();

          var rating = _(person.personRatings)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          return [200, rating.personRatingEditions];
        })
      .when('GET', '/api/persons/:id/ratings/:ind/editions/:childInd',
        function ($params, personLots) {
          var person = _(personLots)
           .filter({ lotId: parseInt($params.id, 10) }).first();

          var rating = _(person.personRatings)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          var ratingEdition = _(rating.personRatingEditions)
            .filter({ partIndex: parseInt($params.childInd, 10) }).first();

          return [200, ratingEdition];
        })
      .when('POST', '/api/persons/:id/ratings/:ind/editions',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var rating = _(person.personRatings)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          var personRatingEdition = {
            partIndex: person.nextIndex++,
            part: $jsonData.part
          };

          rating.personRatingEditions.push(personRatingEdition);

          return [200];
        })
      .when('POST', '/api/persons/:id/ratings/:ind/editions/:childInd',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var rating = _(person.personRatings)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          var ratingEdition = _(rating.personRatingEditions)
            .filter({ partIndex: parseInt($params.childInd, 10) }).first();

          _.assign(ratingEdition.part, $jsonData.part);

          return [200];
        })
      .when('DELETE', '/api/persons/:id/ratings/:ind/editions/:childInd',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var rating = _(person.personRatings)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          var ratingEditionChildInd = _(rating.personRatingEditions)
            .findIndex({ partIndex: parseInt($params.childInd, 10) });

          rating.personRatingEditions.splice(ratingEditionChildInd, 1);

          if (rating.personRatingEditions === []) {
            var ratingInd = _(rating.personRatings)
           .findIndex({ partIndex: parseInt($params.ind, 10) });

            person.personRatings.splice(ratingInd, 1);
          }
          return [200];
        });
  });
}(angular, _));