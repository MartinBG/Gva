/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Групи Класове ВС за екипажи
  module.exports = [
    { nomValueId: 6986, code: 'F', name: 'Екипажи', nameAlt: '', parentValueId: 5591 },
    { nomValueId: 6987, code: 'M', name: 'ТО на СУВД', nameAlt: '', parentValueId: 5589 },
    { nomValueId: 6985, code: 'T', name: 'ОВД', nameAlt: '', parentValueId: 5592 },
    { nomValueId: 6984, code: 'G', name: 'ТО на ВС', nameAlt: '', parentValueId: 5590 }
  ];
})(typeof module === 'undefined' ? (this['ratingClassGroup'] = {}) : module);
