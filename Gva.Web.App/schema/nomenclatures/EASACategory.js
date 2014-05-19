/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Категория EASA на ВС
  module.exports = [
    {
      nomValueId: 8219, code: 'AW', name: 'Aerial Work', nameAlt: null, parentValueId: null, alias: 'AW'
    },
    {
      nomValueId: 8220, code: 'COM', name: 'Commercial', nameAlt: null, parentValueId: null, alias: 'COM'
    },
    {
      nomValueId: 8221, code: 'COR', name: 'Corporate', nameAlt: null, parentValueId: null, alias: 'COR'
    },
    {
      nomValueId: 8222, code: 'PR', name: 'Private', nameAlt: null, parentValueId: null, alias: 'PR'
    },
    {
      nomValueId: 8223, code: 'VLA', name: 'VLA', nameAlt: null, parentValueId: null, alias: 'VLA'
    }
  ];
})(typeof module === 'undefined' ? (this['EASACategory'] = {}) : module);
