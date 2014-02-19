/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Други типове документи на Физичеко лице
  module.exports = [
    {
      nomValueId: 6193, code: '14', name: 'Контролна карта', nameAlt: 'Контролна карта', alias: 'CtrlCard',
      textContent: {
        isPersonsOnly:'N'
      }
    },
    {
      nomValueId: 6194, code: '15', name: 'Контролен талон', nameAlt: 'Контролен талон', alias: 'CtrlTalon',
      textContent: {
        isPersonsOnly:'N'
      }
    },
    {
      nomValueId: 6197, code: '20', name: 'Писмо', nameAlt: 'Писмо', alias: 'Letter',
      textContent: {
        isPersonsOnly:'N'
      }
    },
    {
      nomValueId: 6227, code: '22', name: 'Справка', nameAlt: 'Справка', alias: 'Report',
      textContent: {
        isPersonsOnly:'N'
      }
    },
    {
      nomValueId: 6257, code: '1', name: 'Удостоверение', nameAlt: 'Удостоверение', alias: null,
      textContent: {
        isPersonsOnly:'N'
      }
    },
    {
      nomValueId: 6258, code: '2', name: 'Протокол', nameAlt: 'Протокол', alias: 'Protocol',
      textContent: {
        isPersonsOnly:'N'
      }
    },
    {
      nomValueId: 6259, code: '3', name: 'Лична карта', nameAlt: 'Лична карта', alias: null,
      textContent: {
        isPersonsOnly:'Y'
      }
    },
    {
      nomValueId: 6260, code: '4', name: 'Задграничен паспорт', nameAlt: 'Задграничен паспорт', alias: null,
      textContent: {
        isPersonsOnly:'Y'
      }
    },
    {
      nomValueId: 6261, code: '5', name: 'Паспорт', nameAlt: 'Паспорт', alias: null,
      textContent: {
        isPersonsOnly:'N'
      }
    },
    {
      nomValueId: 6268, code: '7', name: 'Свидетелство', nameAlt: 'Certificate', alias: 'Certificate',
      textContent: {
        isPersonsOnly:'N'
      }
    }
];
})(typeof module === 'undefined' ? (this['personOtherDocumentType'] = {}) : module);
