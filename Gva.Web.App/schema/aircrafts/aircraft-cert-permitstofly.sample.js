/*global module, require*/
(function (module) {
  'use strict';

  var nomenclatures = require('./nomenclatures.sample');

  module.exports = {
    aircraft1PermitToFly1: {
      certId: '5',
      issueDate: '2013-09-04T00:00',
      issuePlace: 'София',
      purpose: 'Полета трябва да бъде изпълнен в съответствие с условията определени' + 
        'в одобреното РЛЕ – ПВП, на или под 12 500 ft, освен ако не е осигурен' +
        'кислород за екипажа по време на полета.',
      purposeAlt: 'The flight should be performed in accordance to conditions defined' +
        'in the approved AFM - VFR, at or below 12 500 feet unless oxygen is available' +
        'to the crew for the entire flight.',
      pointFrom: 'София, България',
      pointFromAlt:  'Sofia, Bulgaria',
      pointTo: 'Берлин, Германия',
      pointToAlt:  'Berlin, Germany',
      planStops:  'Не',
      planStopsAlt: 'No',
      validToDate:  '2018-09-04T00:00',
      notes: 'Това разрешение е валидно за един полет по' +
        'най-късия маршрут от София (Р. България) до Берлин (Ф. Р. Германия).',
      notesAlt: 'This permit to fly is valid for one flight only on the shortest practicable' +
        'route from Sofia (Republic of Bulgaria) to Berlin (Federal Republic of Germany).',
      crew: 'Командир: Орлин Делчев. Втори пилот: Полина Паскалева',
      crewAlt: 'Commander: Orlin Delchev. Second pilot: Polina Paskaleva'
    }
  };
})(typeof module === 'undefined' ? (this['aircraft-cert-permitstofly.sample'] = {}) : module);
