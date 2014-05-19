/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Видове за одит
  module.exports = [
    {
      nomValueId: 1, code: 'P', name: 'Планиран', nameAlt: 'Планиран', parentValueId: null, alias: 'Planned'
    },
    {
      nomValueId: 2, code: 'R', name: 'В ход', nameAlt: 'В ход', parentValueId: null, alias: 'Running'
    },
    {
      nomValueId: 3, code: 'F', name: 'Завършен', nameAlt: 'Завършен', parentValueId: null, alias: 'Finished'
    }
  ];
})(typeof module === 'undefined' ? (this['auditState'] = {}) : module);
