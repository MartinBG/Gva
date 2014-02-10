/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Ограничения за медицинска годност
  module.exports = [
    { nomTypeValueId: 7828, code: 'OSL', name: 'OSL', nameAlt: 'OSL', nomTypeParentValueId: null, alias: 'OSL' },
		{ nomTypeValueId: 7829, code: 'OFL', name: 'OFL', nameAlt: 'OFL', nomTypeParentValueId: null, alias: 'OFL' },
		{ nomTypeValueId: 7830, code: 'VML', name: 'VML', nameAlt: 'VML', nomTypeParentValueId: null, alias: 'VML' },
		{ nomTypeValueId: 7831, code: 'OCL', name: 'OCL', nameAlt: 'OCL', nomTypeParentValueId: null, alias: 'OCL' },
		{ nomTypeValueId: 7832, code: 'VDL', name: 'VDL', nameAlt: 'VDL', nomTypeParentValueId: null, alias: 'VDL' },
		{ nomTypeValueId: 7833, code: 'TML', name: 'TML', nameAlt: 'TML', nomTypeParentValueId: null, alias: 'TML' },
		{ nomTypeValueId: 7834, code: 'VNL', name: 'VNL', nameAlt: 'VNL', nomTypeParentValueId: null, alias: 'VNL' },
		{ nomTypeValueId: 7835, code: 'OML', name: 'OML', nameAlt: 'OML', nomTypeParentValueId: null, alias: 'OML' },
		{ nomTypeValueId: 7836, code: 'MCL', name: 'MCL', nameAlt: 'MCL', nomTypeParentValueId: null, alias: 'MCL' },
  ];
})(typeof module === 'undefined' ? (this['medicalLimitation'] = {}) : module);