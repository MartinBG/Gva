/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Други типове документи 
  module.exports = [
    {
      nomValueId: 9191, code: '14', name: 'Контролна карта', nameAlt: 'Контролна карта', alias: 'CtrlCard'
    },
    {
      nomValueId: 9192, code: '15', name: 'Контролен талон', nameAlt: 'Контролен талон', alias: 'CtrlTalon'
    }
];
})(typeof module === 'undefined' ? (this['otherDocumentType'] = {}) : module);
