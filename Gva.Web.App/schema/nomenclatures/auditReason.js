/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Причини за одит
  module.exports = [
    {
      nomValueId: 1, code: 'I', name: 'Първоначално', nameAlt: 'Първоначално', parentValueId: null, alias: 'Initial'
    },
    {
      nomValueId: 2, code: 'C', name: 'Промяна', nameAlt: 'Промяна', parentValueId: null, alias: 'Change'
    },
    {
      nomValueId: 3, code: 'S', name: 'Надзор', nameAlt: 'Надзор', parentValueId: null, alias: 'Superintendance'
    }
  ];
})(typeof module === 'undefined' ? (this['auditReason'] = {}) : module);
