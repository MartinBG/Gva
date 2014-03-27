/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    airport1Data: {
      airportType: nomenclatures.get('airportTypes', 'AT1'),
      name: 'Летище София',
      nameAlt: 'Sofia Airport',
      icao: '33-12-AS',
      place: 'София',
      coordinates: {
        latitude: '123',
        longitude: '124'
      },
      runway: '2111',
      course: 'Изток',
      excess: '318',
      concrete: '321',
      frequencies: [
        {
          frequency: '123',
          parameters: '124'
        },
        {
          frequency: '127',
          parameters: '128'
        }
      ],
      radioNavigationAids: [
        {
          aid: '123',
          parameters: '124'
        },
        {
          aid: '124',
          parameters: '125'
        }
      ]
    },
    airport2Data: {
      airportType: nomenclatures.get('airportTypes', 'AT1'),
      name: 'Летище Варна',
      nameAlt: 'Varna Airport',
      icao: '34-12-AV',
      place: 'Варна',
      coordinates: {
        latitude: '23',
        longitude: '24'
      },
      runway: '1111',
      course: 'Изток',
      excess: '218',
      concrete: '330',
      frequencies: [
        {
          frequency: '213',
          parameters: '214'
        }
      ],
      radioNavigationAids: [
        {
          aid: '231',
          parameters: '241'
        }
      ]
    }
  };
})(typeof module === 'undefined' ? (this['airport-data.sample'] = {}) : module);
