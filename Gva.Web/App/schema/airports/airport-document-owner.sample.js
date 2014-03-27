/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    airport1Owner1: {
      airportRelation: nomenclatures.get('airportRelations', 'Owner'),
      person: nomenclatures.get('persons', 'P1'),
      organization: nomenclatures.get('organizations', 'AAK Progres'),
      documentNumber: '123',
      documentDate: '2010-05-06T00:00',
      fromDate: '2001-05-06T00:00',
      toDate: '2010-05-06T00:00',
      reasonTerminate: '',
      notes: 'Тест',
      bookPageNumber: '2',
      pageCount: '4'
    },
    airport1Owner2: {
      airportRelation: nomenclatures.get('airportRelations', 'Oper'),
      person: nomenclatures.get('persons', 'P2'),
      organization: nomenclatures.get('organizations', 'Wizz Air'),
      documentNumber: '124',
      documentDate: '2011-03-16T00:00',
      fromDate: '2002-03-16T00:00',
      toDate: '2011-03-16T00:00',
      reasonTerminate: '',
      notes: 'Тест2',
      bookPageNumber: '1',
      pageCount: '7'
    }
  };
})(typeof module === 'undefined' ? (this['airport-document-owner.sample'] = {}) : module);
