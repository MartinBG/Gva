/*global angular, require*/
(function (angular) {
  'use strict';

  var airportData = require('./airport-data.sample'),
      airportDocumentOthers = require('./airport-document-other.sample'),
      airportDocumentOwners = require('./airport-document-owner.sample'),
      airportCertOperationals = require('./airport-cert-operational.sample'),
      airportDocumentApplications = require('./airport-document-application.sample');

  angular.module('app').constant('airportLots', [
    {
      lotId: 1,
      nextIndex: 10,
      airportData: {
        partIndex: 1,
        part: airportData.airport1Data
      },
      airportDocumentOthers: [
        {
          partIndex: 2,
          part: airportDocumentOthers.airport1DocOther1
        },
        {
          partIndex: 3,
          part: airportDocumentOthers.airport1DocOther2
        }
      ],
      airportDocumentOwners: [
        {
          partIndex: 4,
          part: airportDocumentOwners.airport1Owner1
        },
        {
          partIndex: 5,
          part: airportDocumentOwners.airport1Owner2
        }
      ],
      airportCertOperationals: [
        {
          partIndex: 6,
          part: airportCertOperationals.airport1Oper1
        },
        {
          partIndex: 7,
          part: airportCertOperationals.airport1Oper2
        }
      ],
      airportDocumentApplications: [
        {
          partIndex: 8,
          part: airportDocumentApplications.airport1Application1
        },
        {
          partIndex: 9,
          part: airportDocumentApplications.airport1Application2
        }
      ]
    },
    {
      lotId: 2,
      nextIndex: 2,
      airportData: {
        partIndex: 1,
        part: airportData.airport2Data
      }
    }
  ]);
}(angular));
