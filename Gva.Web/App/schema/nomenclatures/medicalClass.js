/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Класове за медицинска годност
  module.exports = [
    { nomTypeValueId: 7824, code: '01', name: 'Class-1', nameAlt: 'Class-1', nomTypeParentValueId: null, alias: 'class1' },
    { nomTypeValueId: 7825, code: '02', name: 'Class-2', nameAlt: 'Class-2', nomTypeParentValueId: null, alias: 'class2' },
    { nomTypeValueId: 7826, code: '03', name: 'Class-3', nameAlt: 'Class-3', nomTypeParentValueId: null, alias: 'class3' },
    { nomTypeValueId: 7827, code: '04', name: 'Class-4', nameAlt: 'Class-4', nomTypeParentValueId: null, alias: 'class4' }
  ];
})(typeof module === 'undefined' ? (this['medicalClass'] = {}) : module);
