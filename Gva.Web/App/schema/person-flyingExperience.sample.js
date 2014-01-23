/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    person1FlyingExperience1: {
      staffType: nomenclatures.get('staffTypes', 'Crew'),
      documentDate: '1981-04-04T00:00',
      period: {
        month: 'Януари',
        year: 2014
      },
      organization: nomenclatures.get('organizations', 'AAK Progres'),
      aircraft: nomenclatures.get('aircrafts', 'aircraft1'),
      ratingType: nomenclatures.get('ratingTypes', 'BAe 146'),
      ratingClass: nomenclatures.get('ratingClasses', 'VLA'),
      sector: 'sektor',
      licenceType: nomenclatures.get('licenceTypes', 'CPA'),
      authorization: nomenclatures.get('authorizations', 'FI(A)'),
      locationIndicator: nomenclatures.get('locationIndicators', 'LBBG'),
      experienceRole: nomenclatures.get('experienceRoles', 'type1'),
      experienceMeasure: nomenclatures.get('experienceMeasures', 'measure1'),
      dayIFR: {
        hours: 23,
        minutes: 33
      },
      dayVFR: {
        hours: 24,
        minutes: 34
      },
      nightIFR: {
        hours: 25,
        minutes: 35
      },
      nightVFR: {
        hours: 26,
        minutes: 36
      },
      dayLandings: 5,
      nightLandings: 15,
      total: {
        hours: 26,
        minutes: 36
      },
      totalDoc: {
        hours: 26,
        minutes: 36
      },
      totalLastMonths: {
        hours: 26,
        minutes: 36
      }
    }
  };
})(typeof module === 'undefined' ? (this['person-flyingExperience.sample'] = {}) : module);
