/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Роли в натрупан летателният опит
  module.exports = [
    {
      nomValueId: 7837, code: 'IE', name: 'Инструктор', nameAlt: 'Инструктор', alias: 'instructor',
      textContent: {
        codeCA: null
      }
    },
    {
      nomValueId: 7838, code: 'EI', name: 'Обучаем (с инструктор)', nameAlt: 'Обучаем (с инструктор)', alias: 'training',
      textContent: {
        codeCA: null
      }
    },
    {
      nomValueId: 7839, code: 'CI', name: 'Под наблюдение на инструктор', nameAlt: 'Под наблюдение на инструктор', alias: 'control',
      textContent: {
        codeCA: null
      }
    },
    {
      nomValueId: 7840, code: 'IN', name: 'Самостоятелен', nameAlt: 'Самостоятелен', alias: 'independent',
      textContent: {
        codeCA: null
      }
    }
  ];
})(typeof module === 'undefined' ? (this['experienceRole'] = {}) : module);
