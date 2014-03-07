/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Категория EASA на ВС
  module.exports = [
    {
      nomValueId: 8224, code: 'EU', name: 'EU', nameAlt: null, parentValueId: null, alias: 'EU'
    },
    {
      nomValueId: 8225, code: 'EUR', name: 'EU - Restricted', nameAlt: null, parentValueId: null, alias: 'EUR'
    },
    {
      nomValueId: 8226, code: 'OD', name: 'Old Doc', nameAlt: null, parentValueId: null, alias: 'OD'
    },
    {
      nomValueId: 8227, code: 'AII', name: 'Annex II', nameAlt: null, parentValueId: null, alias: 'AII'
    },
    {
      nomValueId: 8228, code: 'A2', name: 'Article-2', nameAlt: null, parentValueId: null, alias: 'A2'
    },
    {
      nomValueId: 8229, code: 'A1', name: 'Article-1', nameAlt: null, parentValueId: null, alias: 'A1'
    },
    {
      nomValueId: 8230, code: 'VLA', name: 'VLA', nameAlt: null, parentValueId: null, alias: 'VLA'
    },
    {
      nomValueId: 8231, code: 'AM', name: 'Amateur', nameAlt: null, parentValueId: null, alias: 'AM'
    },
    {
      nomValueId: 8232, code: 'XP', name: 'EXP', nameAlt: null, parentValueId: null, alias: 'XP'
    }
  ];
})(typeof module === 'undefined' ? (this['EURegType'] = {}) : module);
