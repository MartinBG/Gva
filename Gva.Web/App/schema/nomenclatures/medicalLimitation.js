/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Ограничения за медицинска годност
  module.exports = [
    { nomValueId: 7828, code: 'OSL', name: 'OSL', nameAlt: 'OSL', parentValueId: null, alias: 'OSL' },
    { nomValueId: 7829, code: 'OFL', name: 'OFL', nameAlt: 'OFL', parentValueId: null, alias: 'OFL' },
    { nomValueId: 7830, code: 'VML', name: 'VML', nameAlt: 'VML', parentValueId: null, alias: 'VML' },
    { nomValueId: 7831, code: 'OCL', name: 'OCL', nameAlt: 'OCL', parentValueId: null, alias: 'OCL' },
    { nomValueId: 7832, code: 'VDL', name: 'VDL', nameAlt: 'VDL', parentValueId: null, alias: 'VDL' },
    { nomValueId: 7833, code: 'TML', name: 'TML', nameAlt: 'TML', parentValueId: null, alias: 'TML' },
    { nomValueId: 7834, code: 'VNL', name: 'VNL', nameAlt: 'VNL', parentValueId: null, alias: 'VNL' },
    { nomValueId: 7835, code: 'OML', name: 'OML', nameAlt: 'OML', parentValueId: null, alias: 'OML' },
    { nomValueId: 7836, code: 'MCL', name: 'MCL', nameAlt: 'MCL', parentValueId: null, alias: 'MCL' },
  ];
})(typeof module === 'undefined' ? (this['medicalLimitation'] = {}) : module);