/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Групи Класове ВС за екипажи
  module.exports = [
    { nomTypeValueId: 6986, code: 'F', name: 'Екипажи', nameAlt: '', nomTypeParentValueId: 5591 },
    { nomTypeValueId: 6987, code: 'M', name: 'ТО на СУВД', nameAlt: '', nomTypeParentValueId: 5589 },
    { nomTypeValueId: 6985, code: 'T', name: 'ОВД', nameAlt: '', nomTypeParentValueId: 5592 },
    { nomTypeValueId: 6984, code: 'G', name: 'ТО на ВС', nameAlt: '', nomTypeParentValueId: 5590 }
  ];
})(typeof module === 'undefined' ? (this['ratingClassGroup'] = {}) : module);
