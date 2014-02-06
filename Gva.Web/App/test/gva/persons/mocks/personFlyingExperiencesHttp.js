/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when(
      'GET',
      '/api/persons/:id/personFlyingExperiences?organization&' +
      'period&aircraft&ratingType&ratingClass&authorization&' +
      'licenceType&locationIndicator&sector&experienceRole&experienceMeasure&' +
      'month&year',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          return [200, person.personFlyingExperiences];
        })
      .when('GET', '/api/persons/:id/personFlyingExperiences/:ind',
        function ($params, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personFlyingExperience = _(person.personFlyingExperiences)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          if (personFlyingExperience) {
            return [200, personFlyingExperience];
          }
          else {
            return [404];
          }
        })
      .when('POST', '/api/persons/:id/personFlyingExperiences',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personFlyingExperience = $jsonData;

          personFlyingExperience.partIndex = person.nextIndex++;
          person.personFlyingExperiences =
            person.personFlyingExperiences ? person.personFlyingExperiences : [];
          person.personFlyingExperiences.push(personFlyingExperience);

          return [200];
        })
      .when('POST', '/api/persons/:id/personFlyingExperiences/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personFlyingExperience = _(person.personFlyingExperiences)
            .filter({ partIndex: parseInt($params.ind, 10) }).first();

          _.assign(personFlyingExperience, $jsonData);

          return [200];
        })
      .when('DELETE', '/api/persons/:id/personFlyingExperiences/:ind',
        function ($params, $jsonData, personLots) {
          var person = _(personLots)
            .filter({ lotId: parseInt($params.id, 10) }).first();

          var personFlyingExperienceInd = _(person.personFlyingExperiences)
            .findIndex({ partIndex: parseInt($params.ind, 10) });

          person.personFlyingExperiences.splice(personFlyingExperienceInd, 1);

          return [200];
        });
  });
}(angular, _));
