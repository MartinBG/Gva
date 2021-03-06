﻿/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Степени на образование
  module.exports = [
    {
      nomValueId: 5629, code: 'HM', name: 'Висше образование (магистър)', nameAlt: 'Висше образование (магистър)', alias: 'HM',
      content: {
        rating: 81
      }
    },
    {
      nomValueId: 5630, code: 'PQ', name: 'Професионална квалификация', nameAlt: 'Професионална квалификация', alias: 'PQ',
      content: {
        rating: 0
      }
    },
    {
      nomValueId: 5631, code: 'ESS', name: 'Средно специално образование', nameAlt: 'Средно специално образование', alias: 'ESS',
      content: {
        rating: 51
      }
    },
    {
      nomValueId: 5632, code: 'HS', name: 'Висше образование (бакалавър)', nameAlt: 'Висше образование (бакалавър)', alias: 'HS',
      content: {
        rating: 80
      }
    },
    {
      nomValueId: 5633, code: 'ES', name: 'Средно образование', nameAlt: 'Средно образование', alias: 'ES',
      content: {
        rating: 50
      }
    },
    {
      nomValueId: 5634, code: 'HH', name: 'Полувисше (специалист)', nameAlt: 'Полувисше (специалист)', alias: 'HH',
      content: {
        rating: 70
      }
    },
    {
      nomValueId: 5635, code: 'PE', name: 'Основно образование', nameAlt: 'Primary Education', alias: 'PE',
      content: {
        rating: 1
      }
    },
  ];
})(typeof module === 'undefined' ? (this['graduation'] = {}) : module);
