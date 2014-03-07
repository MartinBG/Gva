/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1Occurrence1: {
      localDate: '1981-04-04T00:00',
      localTime: {
        hours: '13',
        minutes: '00'
      },
      aircraftOccurrenceClass: nomenclatures.get('aircraftOccurrenceClasses', 'Occurence'),
      country: nomenclatures.get('countries', 'BG'),
      area: 'София',
      occurrenceNotes: 'Occurence notes',
      notes: 'Notes',
      bookPageNumber: '2',
      pageCount: '4'
    },
    aircraft1Occurrence2: {
      localDate: '2001-04-04T00:00',
      localTime: {
        hours: '14',
        minutes: '20'
      },
      aircraftOccurrenceClass: nomenclatures.get('aircraftOccurrenceClasses', 'Event'),
      country: nomenclatures.get('countries', 'BG'),
      area: 'Пловдив',
      occurrenceNotes: 'Occurence notes2',
      notes: 'Notes2',
      bookPageNumber: '4',
      pageCount: '5'
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-document-occurrences.sample'] = {}) : module);
