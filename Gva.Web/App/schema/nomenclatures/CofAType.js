/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Тип летателна годност на ВС
  module.exports = [
    {
      nomValueId: 8201, code: 'E25', name: 'EASA 25', nameAlt: null, parentValueId: null, alias: 'E25'
    },
    {
      nomValueId: 8202, code: 'E24', name: 'EASA 24', nameAlt: null, parentValueId: null, alias: 'E24'
    },
    {
      nomValueId: 8203, code: 'BGF', name: 'BG Form', nameAlt: null, parentValueId: null, alias: 'BGF'
    },
    {
      nomValueId: 8204, code: 'TC', name: 'Tech Cert', nameAlt: null, parentValueId: null, alias: 'TC'
    },
    {
      nomValueId: 8205, code: 'EXP', name: 'EXP', nameAlt: null, parentValueId: null, alias: 'EXP'
    }
  ];
})(typeof module === 'undefined' ? (this['CofAType'] = {}) : module);
