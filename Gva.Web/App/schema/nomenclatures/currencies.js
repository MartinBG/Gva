/*global module*/
(function (module) {
  'use strict';
  //Номенклатура Парични единици
  module.exports = [
		{ nomValueId: 8058, code: 'BGL', name: 'Български лев', nameAlt: 'Български лев', nomTypeParentValueId: null, alias: 'BGL' },
		{ nomValueId: 8059, code: 'BGN', name: 'Нов български лев', nameAlt: 'Нов български лев', nomTypeParentValueId: null, alias: 'BGN' },
		{ nomValueId: 8060, code: 'EUR', name: 'Евро', nameAlt: 'Евро', nomTypeParentValueId: null, alias: 'EUR' },
  ];
})(typeof module === 'undefined' ? (this['currency'] = {}) : module);
