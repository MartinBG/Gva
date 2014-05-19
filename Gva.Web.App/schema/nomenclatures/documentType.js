/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Типове документи
  module.exports = [
    {
      nomValueId: 6442, code: '3', name: 'Лична карта', nameAlt: 'Лична карта', alias: 'Id',
      textContent: {
        isIdDocument: 'true',
        isPersonsOnly: 'Y'
      }
    },
    {
      nomValueId: 6443, code: '4', name: 'Задграничен паспорт', nameAlt: 'Задграничен паспорт', alias: null,
      textContent: {
        isIdDocument: 'true',
        isPersonsOnly: 'Y'
      }
    },
    {
      nomValueId: 6444, code: '5', name: 'Паспорт', nameAlt: 'Паспорт', alias: 'passport',
      textContent: {
        isIdDocument: 'true',
        isPersonsOnly: 'N'
      }
    },
    {
      nomValueId: 6193, code: '14', name: 'Контролна карта', nameAlt: 'Контролна карта', alias: 'CtrlCard',
        textContent: {
          isIdDocument: 'false',
          isPersonsOnly: 'N'
        }
      },
    {
      nomValueId: 6194, code: '15', name: 'Контролен талон', nameAlt: 'Контролен талон', alias: 'CtrlTalon',
      textContent: {
        isIdDocument: 'false',
        isPersonsOnly: 'N'
      }
    },
    {
      nomValueId: 6197, code: '20', name: 'Писмо', nameAlt: 'Писмо', alias: 'Letter',
      textContent: {
        isIdDocument: 'false',
        isPersonsOnly: 'N'
      }
    },
    {
      nomValueId: 6227, code: '22', name: 'Справка', nameAlt: 'Справка', alias: 'Report',
      textContent: {
        isIdDocument: 'false',
        isPersonsOnly: 'N'
      }
    },
    {
      nomValueId: 6257, code: '1', name: 'Удостоверение', nameAlt: 'Удостоверение', alias: null,
      textContent: {
        isIdDocument: 'false',
        isPersonsOnly: 'N'
      }
    },
    {
      nomValueId: 6258, code: '2', name: 'Протокол', nameAlt: 'Протокол', alias: 'Protocol',
      textContent: {
        isIdDocument: 'false',
        isPersonsOnly: 'N'
      }
    },
    {
      nomValueId: 6268, code: '7', name: 'Свидетелство', nameAlt: 'Certificate', alias: 'Certificate',
      textContent: {
        isIdDocument: 'false',
        isPersonsOnly: 'N'
      }
    }
  ];
})(typeof module === 'undefined' ? (this['documentType'] = {}) : module);
