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
          //if ($params.aircraft ) {
          //  var flyingExperiences = [],
          //      exists;
          //  angular.forEach(person.personFlyingExperiences, function (flyingExperience) {
          //    exists = flyingExperience.part.organization.nomTypeValueId ===
          //      parseInt($params.organization, 10) &&
          //      flyingExperience.part.aircraft.nomTypeValueId ===
          //      parseInt($params.aircraft, 10) &&
          //      flyingExperience.part.ratingType.nomTypeValueId ===
          //      parseInt($params.ratingType, 10) &&
          //      flyingExperience.part.ratingClass.nomTypeValueId ===
          //      parseInt($params.ratingClass, 10) &&
          //      flyingExperience.part.licenceType.nomTypeValueId ===
          //      parseInt($params.licenceType, 10) &&
          //      flyingExperience.part.sector ===
          //      $params.sector &&
          //      flyingExperience.part.period.month ===
          //      $params.month &&
          //      flyingExperience.part.period.year ===
          //      parseInt($params.year, 10) &&
          //      flyingExperience.part.experienceRole.nomTypeValueId ===
          //      parseInt($params.experienceRole, 10) &&
          //      flyingExperience.part.locationIndicator.nomTypeValueId ===
          //      parseInt($params.locationIndicator, 10) &&
          //      flyingExperience.part.authorization.nomTypeValueId ===
          //      parseInt($params.authorization, 10) &&
          //      flyingExperience.part.experienceMeasure.nomTypeValueId ===
          //      parseInt($params.experienceMeasure, 10);
          //    if (exists) {
          //      flyingExperiences.push(flyingExperience);
          //    }
          //  });
          //  return [200, flyingExperiences];
          //} else {
          return [200, person.personFlyingExperiences];
          //}
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