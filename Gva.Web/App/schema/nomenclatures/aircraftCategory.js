/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Групи ВС
  module.exports = [
    {
      nomValueId: 8001, code: 'A1', name: 'A1 Самолет с тегло 5,700 кг. или повече', nameAlt: null, parentValueId: null, alias: 'A1'
    },
    {
      nomValueId: 8002, code: 'A2', name: 'A2 Самолет с тегло под 5,700', nameAlt: null, parentValueId: null, alias: 'A2'
    },
  ];
})(typeof module === 'undefined' ? (this['aircraftCategory'] = {}) : module);
