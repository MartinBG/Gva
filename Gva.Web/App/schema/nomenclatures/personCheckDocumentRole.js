/*global module*/
(function (module) {
  'use strict';

  //номенклатура Роли документи за проверка на Физичеко лице
  module.exports = [
    {
      nomValueId: 6445, code: '49A', name: 'Проверка на работното място', nameAlt: 'Проверка на работното място', alias: 'CheckAtWork',
      textContent: {
        direction: 6177,
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
    {
      nomValueId: 6446, code: '15', name: 'Практическа проверка', nameAlt: 'Практическа проверка', alias: 'PracticalCheck',
      textContent: {
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
    {
      nomValueId: 6447, code: '1', name: 'Летателна проверка', nameAlt: 'Летателна проверка', alias: 'FlightTest',
      textContent: {
        direction: 6173,
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
    {
      nomValueId: 6448, code: '7', name: 'Тренажор', nameAlt: 'Тренажор', alias: 'Trainer',
      textContent: {
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
  ];
})(typeof module === 'undefined' ? (this['personCheckDocumentRole'] = {}) : module);
