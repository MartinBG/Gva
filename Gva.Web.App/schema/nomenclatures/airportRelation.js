/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Групи ВС
  module.exports = [
  {
    nomValueId: 1, code: 'OW', name: 'Собственик', nameAlt: null, alias: 'Owner'
  },
  {
    nomValueId: 2, code: 'TN', name: 'Наемател', nameAlt: null, alias: 'Loanee'
  },
  {
    nomValueId: 3, code: 'OP', name: 'Летищен оператор', nameAlt: null, alias: 'Oper'
  }
  ];
})(typeof module === 'undefined' ? (this['airportRelation'] = {}) : module);
