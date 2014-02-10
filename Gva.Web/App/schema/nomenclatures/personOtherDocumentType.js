/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Други типове документи на Физичеко лице
  module.exports = [
    {
      nomTypeValueId: 6193, code: '14', name: 'Контролна карта', nameAlt: 'Контролна карта', alias: 'CtrlCard',
      content: {
        isPersonsOnly:'N'
      }
    },
    {
      nomTypeValueId: 6194, code: '15', name: 'Контролен талон', nameAlt: 'Контролен талон', alias: 'CtrlTalon',
      content: {
        isPersonsOnly:'N'
      }
    },
    {
      nomTypeValueId: 6197, code: '20', name: 'Писмо', nameAlt: 'Писмо', alias: 'Letter',
      content: {
        isPersonsOnly:'N'
      }
    },
    {
      nomTypeValueId: 6227, code: '22', name: 'Справка', nameAlt: 'Справка', alias: 'Report',
      content: {
        isPersonsOnly:'N'
      }
    },
    {
      nomTypeValueId: 6257, code: '1', name: 'Удостоверение', nameAlt: 'Удостоверение', alias: null,
      content: {
        isPersonsOnly:'N'
      }
    },
    {
      nomTypeValueId: 6258, code: '2', name: 'Протокол', nameAlt: 'Протокол', alias: 'Protocol',
      content: {
        isPersonsOnly:'N'
      }
    },
    {
      nomTypeValueId: 6259, code: '3', name: 'Лична карта', nameAlt: 'Лична карта', alias: null,
      content: {
        isPersonsOnly:'Y'
      }
    },
    {
      nomTypeValueId: 6260, code: '4', name: 'Задграничен паспорт', nameAlt: 'Задграничен паспорт', alias: null,
      content: {
        isPersonsOnly:'Y'
      }
    },
    {
      nomTypeValueId: 6261, code: '5', name: 'Паспорт', nameAlt: 'Паспорт', alias: null,
      content: {
        isPersonsOnly:'N'
      }
    },
    {
      nomTypeValueId: 6268, code: '7', name: 'Свидетелство', nameAlt: 'Certificate', alias: 'Certificate',
      content: {
        isPersonsOnly:'N'
      }
    }
];
})(typeof module === 'undefined' ? (this['personOtherDocumentType'] = {}) : module);
