/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Видове летателен опит
  module.exports = [
    {
      nomValueId: 7841, code: 'W', name: 'Отработени часове', nameAlt: '', alias: 'whours', textContent: {
        codeCA: null
      }
    },
    {
      nomValueId: 7842, code: 'H', name: 'Летателни часове', nameAlt: '', alias: 'fhours', textContent: {
        codeCA: null
      }
    },
    {
      nomValueId: 7843, code: 'F', name: 'Брой полети', nameAlt: '', alias: 'flights', textContent: {
        codeCA: null
      }
    }
  ];
})(typeof module === 'undefined' ? (this['experienceMeasure'] = {}) : module);