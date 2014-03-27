/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Типове Летище или летателна площадка
  module.exports = [
    {
      nomValueId: 1, code: 'AT1', name: 'Международно летище', nameAlt: null, parentValueId: null, alias: 'AT1'
    },
    {
      nomValueId: 2, code: 'AT2', name: 'Военно летище', nameAlt: null, parentValueId: null, alias: 'AT2'
    },
    {
      nomValueId: 3, code: 'AT3', name: 'Летателна площадка', nameAlt: null, parentValueId: null, alias: 'AT3'
    }
  ];
})(typeof module === 'undefined' ? (this['airportType'] = {}) : module);
