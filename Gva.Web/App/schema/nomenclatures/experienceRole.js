/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Роли в натрупан летателният опит
  module.exports = [
    {
      nomTypeValueId: 7837, code: 'IE', name: 'Инструктор', nameAlt: 'Инструктор', alias: 'instructor',
      content: {
        codeCA: null
      }
    },
    {
      nomTypeValueId: 7838, code: 'EI', name: 'Обучаем (с инструктор)', nameAlt: 'Обучаем (с инструктор)', alias: 'training',
      content: {
        codeCA: null
      }
    },
    {
      nomTypeValueId: 7839, code: 'CI', name: 'Под наблюдение на инструктор', nameAlt: 'Под наблюдение на инструктор', alias: 'control',
      content: {
        codeCA: null
      }
    },
    {
      nomTypeValueId: 7840, code: 'IN', name: 'Самостоятелен', nameAlt: 'Самостоятелен', alias: 'independent',
      content: {
        codeCA: null
      }
    }
  ];
})(typeof module === 'undefined' ? (this['experienceRole'] = {}) : module);
