/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Класове инциденти с ВС.
  module.exports = [
    {
      nomValueId: 1, code: 'A', name: 'Accident', nameAlt: null, parentValueId: null, alias: 'Accident'
    },
    {
      nomValueId: 2, code: 'E', name: 'Събитие, което генерира доклад за авиационо събитие', nameAlt: null, parentValueId: null, alias: 'Event'
    },
    {
      nomValueId: 3, code: 'O', name: 'Инцидент', nameAlt: null, parentValueId: null, alias: 'Occurence'
    }
  ];
})(typeof module === 'undefined' ? (this['aircraftOccurrenceClass'] = {}) : module);
