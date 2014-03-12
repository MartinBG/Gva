/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Видове за одит
  module.exports = [
    {
      nomValueId: 1, code: 'P', name: 'Планов', nameAlt: 'Планов', parentValueId: null, alias: 'Planned'
    },
    {
      nomValueId: 2, code: 'E', name: 'Извънреден', nameAlt: 'Извънреден', parentValueId: null, alias: 'Emergent'
    }
  ];
})(typeof module === 'undefined' ? (this['auditType'] = {}) : module);
