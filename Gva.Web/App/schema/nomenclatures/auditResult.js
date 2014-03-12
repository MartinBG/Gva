/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Резултати от одит
  module.exports = [
    {
      nomValueId: 1, code: '1', name: 'Не е изпълнявано', nameAlt: 'Не е изпълнявано', parentValueId: null, alias: 'Not executed'
    },
    {
      nomValueId: 2, code: '2', name: 'Демонстрирано съответствие', nameAlt: 'Демонстрирано съответствие', parentValueId: null, alias: 'Demonstrated accordance'
    },
    {
      nomValueId: 3, code: '3', name: 'Изискват се малки коригиращи действия', nameAlt: 'Изискват се малки коригиращи действия', parentValueId: null, alias: 'Small corrective actions are required'
    },
    {
      nomValueId: 4, code: '4', name: 'Изискват се незабавни коригиращи действия', nameAlt: 'Изискват се незабавни коригиращи действия', parentValueId: null, alias: 'Immediate corrective actions are required'
    },
    {
      nomValueId: 5, code: '5', name: 'Не съответства на обхвата на одобрение', nameAlt: 'Не съответства на обхвата на одобрение', parentValueId: null, alias: 'Does not correspond'
    }
  ];
})(typeof module === 'undefined' ? (this['auditResult'] = {}) : module);
