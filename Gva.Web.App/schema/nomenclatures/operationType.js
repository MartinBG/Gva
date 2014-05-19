/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Типове ориентации
  module.exports = [
    {
      nomValueId: 8020, code: 'OT1', name: 'Транспортна категория - пътници', nameAlt: null, parentValueId: null, alias: 'OT1'
    },
    {
      nomValueId: 8021, code: 'OT2', name: 'Транспортна категория - карго', nameAlt: null, parentValueId: null, alias: 'OT2'
    }
  ];
})(typeof module === 'undefined' ? (this['operationType'] = {}) : module);
