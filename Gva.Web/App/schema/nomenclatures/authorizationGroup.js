/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Групи Разрешения към квалификация
  module.exports = [
    {
      nomTypeValueId: 7049, code: 'FT', name: 'За провеждане обучение', nomTypeParentValueId: 5591, alias: 'FT',
      content: { }
    },
    {
      nomTypeValueId: 7046, code: 'T', name: 'За ОВД', nomTypeParentValueId: 5592, alias: 'T',
      content: {
        ratingClassGroupId: 6985
      }
    },
    {
      nomTypeValueId: 7047, code: 'G', name: 'За ТО (AML)', nomTypeParentValueId: 5590, alias: 'G',
      content: {
        ratingClassGroupId: 6984
      }
    },
    {
      nomTypeValueId: 7048, code: 'F', name: 'За екипаж на ВС', nomTypeParentValueId: 5591, alias: 'F',
      content: { }
    },
    {
      nomTypeValueId: 7045, code: 'FC', name: 'Проверяващи', nomTypeParentValueId: 5591, alias: 'FC',
      content: { }
    },
    {
      nomTypeValueId: 7050, code: 'M', name: 'За ТО (СУВД)', nomTypeParentValueId: 5589, alias: 'M',
      content: {
        ratingClassGroupId: 6987
      }
    }
  ];
})(typeof module === 'undefined' ? (this['authorizationGroup'] = {}) : module);
