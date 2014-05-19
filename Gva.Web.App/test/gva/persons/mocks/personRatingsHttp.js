/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', '/api/persons/:id/ratings',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, person.personRatings];
        })
      .when('GET', '/api/persons/:id/ratings/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var rating = _(person.personRatings)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          return [200, rating];
        })
      .when('POST', '/api/persons/:id/ratings',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var rating = $jsonData;

          rating.partIndex = person.nextIndex++;

          person.personRatings.push(rating);

          return [200, {partIndex: rating.partIndex}];
        })
      .when('POST', '/api/persons/:id/ratings/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var rating = _(person.personRatings)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(rating, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/persons/:id/ratings/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personRatingInd = _(person.personRatings)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          person.personRatings.splice(personRatingInd, 1);

          return [200];
        });
  });
}(angular, _));