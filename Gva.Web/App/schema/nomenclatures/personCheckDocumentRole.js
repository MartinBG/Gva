/*global module*/
(function (module) {
  'use strict';

  //номенклатура Роли документи за проверка на Физичеко лице
  module.exports = [
    {
      nomTypeValueId: 6445, code: '49A', name: 'Проверка на работното място', nameAlt: 'Проверка на работното място', alias: 'CheckAtWork',
      content: {
        direction: 6177,
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
    {
      nomTypeValueId: 6446, code: '15', name: 'Практическа проверка', nameAlt: 'Практическа проверка', alias: 'PracticalCheck',
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
    {
      nomTypeValueId: 6447, code: '1', name: 'Летателна проверка', nameAlt: 'Летателна проверка', alias: 'FlightTest',
      content: {
        direction: 6173,
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
    {
      nomTypeValueId: 6448, code: '7', name: 'Тренажор', nameAlt: 'Тренажор', alias: 'Trainer',
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
  ];
})(typeof module === 'undefined' ? (this['personCheckDocumentRole'] = {}) : module);
