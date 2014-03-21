/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Състяние на регистрация на ВС
  module.exports = [
  {
    nomValueId: 1, code: 'R1', name: 'Неизползван (Пропуснат / Запазен) запис', nameAlt: null, alias: 'R1'
  },
  {
    nomValueId: 2, code: 'R2', name: 'Първоначална регистрация (няма промени)', nameAlt: null, alias: 'R2'
  },
  {
    nomValueId: 3, code: 'R3', name: 'Последна (действаща) регистрация', nameAlt: null, alias: 'R3'
  },
  {
    nomValueId: 4, code: 'R4', name: 'Последна (действаща) регистрация, с отнета Летателна Годност', nameAlt: null, alias: 'R4'
  },
  {
    nomValueId: 5, code: 'R5', name: 'Вписан, но нерегистриран', nameAlt: null, alias: 'R5'
  },
  {
    nomValueId: 6, code: 'R6', name: 'Смяна на собственост - отписан', nameAlt: null, alias: 'R6'
  },
  {
    nomValueId: 7, code: 'R7', name: 'Дерегистриран поради брак', nameAlt: null, alias: 'R7'
  },
  {
    nomValueId: 8, code: 'R8', name: 'Изтичане на срока на договора - отписан', nameAlt: null, alias: 'R8'
  },
  {
    nomValueId: 9, code: 'R9', name: 'Пререгистриран', nameAlt: null, alias: 'R9'
  },
  {
    nomValueId: 10, code: 'R10', name: 'Промяна в статуса съгласно ЗГВ от гражданско на военно ВС', nameAlt: null, alias: 'R10'
  },
  {
    nomValueId: 11, code: 'R11', name: 'Отписан', nameAlt: null, alias: 'R11'
  },
  {
    nomValueId: 12, code: 'R12', name: 'ЛГ не е последна', nameAlt: null, alias: 'R12'
  }
  ];
})(typeof module === 'undefined' ? (this['aircraftRegStatus'] = {}) : module);
