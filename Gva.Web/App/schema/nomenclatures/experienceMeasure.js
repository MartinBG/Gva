/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Видове летателен опит
  module.exports = [
    {
      nomTypeValueId: 7841, code: 'W', name: 'Отработени часове', nameAlt: '', alias: 'whours', content: {
        codeCA: null
      }
    },
    {
      nomTypeValueId: 7842, code: 'H', name: 'Летателни часове', nameAlt: '', alias: 'fhours', content: {
        codeCA: null
      }
    },
    {
      nomTypeValueId: 7843, code: 'F', name: 'Брой полети', nameAlt: '', alias: 'flights', content: {
        codeCA: null
      }
    }
  ];
})(typeof module === 'undefined' ? (this['experienceMeasure'] = {}) : module);