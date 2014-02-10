/*global module*/
(function (module) {
  'use strict';

  module.exports = [
    {
      nomTypeValueId: 5593, code: 'FDA', name: 'Асистент координатор на полетите', nameAlt: 'FDA', nomTypeParentValueId: '5592', alias: null,
      content: { "codeCA": "FDA", "dateValidFrom": "1900-01-01T00:00:00.000Z", "dateValidTo": "2100-01-01T00:00:00.000Z" }
    },
    {
      nomTypeValueId: 5594, code: 'cat', name: 'Координатор по УВД ', nameAlt: 'САТМ', nomTypeParentValueId: '5592', alias: 'cat',
      content: { "codeCA": "catm", "dateValidFrom": "1900-01-01T00:00:00.000Z", "dateValidTo": "2100-01-01T00:00:00.000Z" }
    },
    {
      nomTypeValueId: 5595, code: '19', name: 'Отговорен ръководител - Част 145', nameAlt: 'Accountable Manager - Part 145', nomTypeParentValueId: null, alias: '19',
      content: { "codeCA": null, "dateValidFrom": "1900-01-01T00:00:00.000Z", "dateValidTo": "2100-01-01T00:00:00.000Z" }
    },
    {
      nomTypeValueId: 5596, code: 'CI', name: 'Ръководител ОТО - Част М, Подчаст Ф', nameAlt: 'Maintenance Organization Manager  - Part M, Subpart F', nomTypeParentValueId: null, alias: 'CI',
      content: { "codeCA": "CI", "dateValidFrom": "1900-01-01T00:00:00.000Z", "dateValidTo": "2100-01-01T00:00:00.000Z" }
    },
    {
      nomTypeValueId: 5597, code: '47A', name: 'Отговорен ръководител - Част 147', nameAlt: 'Acoountable manager - Part 147', nomTypeParentValueId: null, alias: '47A',
      content: { "codeCA": "47A", "dateValidFrom": "1900-01-01T00:00:00.000Z", "dateValidTo": "2100-01-01T00:00:00.000Z" }
    },
    {
      nomTypeValueId: 5598, code: '47B', name: 'Ръководител Техническо Обучение - Част 147', nameAlt: 'Technical Training Manager  - Part 147', nomTypeParentValueId: null, alias: '47B',
      content: { "codeCA": "47B", "dateValidFrom": "1900-01-01T00:00:00.000Z", "dateValidTo": "2100-01-01T00:00:00.000Z" }
    },
    {
      nomTypeValueId: 5599, code: '47С', name: 'Ръководител Kачество - Част 147', nameAlt: 'Quality Manager - Part 147', nomTypeParentValueId: null, alias: '47С',
      content: { "codeCA": "47С", "dateValidFrom": "1900-01-01T00:00:00.000Z", "dateValidTo": "2100-01-01T00:00:00.000Z" }
    },
    {
      nomTypeValueId: 5600, code: '35', name: 'Ученик Ръководител Полети', nameAlt: 'Ученик Ръководител Полети', nomTypeParentValueId: '5592', alias: '35',
      content: { "codeCA": "35", "dateValidFrom": "1900-01-01T00:00:00.000Z", "dateValidTo": "2100-01-01T00:00:00.000Z" }
    },
    {
      nomTypeValueId: 5601, code: 'РП', name: 'Ръководител полети', nameAlt: 'Ръководител полети', nomTypeParentValueId: '5592', alias: 'РП',
      content: { "codeCA": "РП", "dateValidFrom": "1900-01-01T00:00:00.000Z", "dateValidTo": "2100-01-01T00:00:00.000Z" }
    }
  ];
})(typeof module === 'undefined' ? (this['employmentCategory'] = {}) : module);
