/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Класове за медицинска годност
  module.exports = [
    { nomValueId: 7824, code: '01', name: 'Class-1', nameAlt: 'Class-1', parentValueId: null, alias: 'class1' },
    { nomValueId: 7825, code: '02', name: 'Class-2', nameAlt: 'Class-2', parentValueId: null, alias: 'class2' },
    { nomValueId: 7826, code: '03', name: 'Class-3', nameAlt: 'Class-3', parentValueId: null, alias: 'class3' },
    { nomValueId: 7827, code: '04', name: 'Class-4', nameAlt: 'Class-4', parentValueId: null, alias: 'class4' }
  ];
})(typeof module === 'undefined' ? (this['medicalClass'] = {}) : module);
