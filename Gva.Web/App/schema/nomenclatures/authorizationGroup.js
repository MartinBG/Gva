/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Групи Разрешения към квалификация
  module.exports = [
    {
      nomValueId: 7049, code: 'FT', name: 'За провеждане обучение', parentValueId: 5591, alias: 'FT',
      textContent: { }
    },
    {
      nomValueId: 7046, code: 'T', name: 'За ОВД', parentValueId: 5592, alias: 'T',
      textContent: {
        ratingClassGroupId: 6985
      }
    },
    {
      nomValueId: 7047, code: 'G', name: 'За ТО (AML)', parentValueId: 5590, alias: 'G',
      textContent: {
        ratingClassGroupId: 6984
      }
    },
    {
      nomValueId: 7048, code: 'F', name: 'За екипаж на ВС', parentValueId: 5591, alias: 'F',
      textContent: { }
    },
    {
      nomValueId: 7045, code: 'FC', name: 'Проверяващи', parentValueId: 5591, alias: 'FC',
      textContent: { }
    },
    {
      nomValueId: 7050, code: 'M', name: 'За ТО (СУВД)', parentValueId: 5589, alias: 'M',
      textContent: {
        ratingClassGroupId: 6987
      }
    }
  ];
})(typeof module === 'undefined' ? (this['authorizationGroup'] = {}) : module);
