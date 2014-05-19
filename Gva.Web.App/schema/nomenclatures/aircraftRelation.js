/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Групи ВС
  module.exports = [
  {
    nomValueId: 1, code: 'R1', name: 'Собственик', nameAlt: null, alias: 'Owner'
  },
  {
    nomValueId: 2, code: 'R2', name: 'Оператор', nameAlt: null, alias: 'Oper'
  },
  {
    nomValueId: 3, code: 'R3', name: 'Наемател', nameAlt: null, alias: 'Loanee'
  },
  {
    nomValueId: 4, code: 'R4', name: 'Лизингодател', nameAlt: null, alias: 'Loaner'
  },
  {
    nomValueId: 5, code: 'R5', name: 'Организация за УЛППГ', nameAlt: null, alias: 'ULPPG'
  },
  {
    nomValueId: 6, code: 'R6', name: 'Организация за ТО', nameAlt: null, alias: 'TO'
  }
  ];
})(typeof module === 'undefined' ? (this['aircraftRelation'] = {}) : module);
