/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Изисквания към раздел
  module.exports = [
    {
      nomValueId: 1, code: '1', name: 'Пилотска кабина', nameAlt: 'Пилотска кабина', parentValueId: null, alias: 'Cockpit', type: 'aircrafts'
    },
    {
      nomValueId: 2, code: '2', name: 'Пътнически салони, тоалетни и кухни', nameAlt: 'Пътнически салони, тоалетни и кухни', parentValueId: null, alias: 'Facilities', type: 'aircrafts'
    },
    {
      nomValueId: 3, code: '3', name: 'Фюзелаж', nameAlt: 'Фюзелаж', parentValueId: null, alias: 'Fuselage', type: 'aircrafts'
    },
    {
      nomValueId: 4, code: '4', name: 'Крила', nameAlt: 'Крила', parentValueId: null, alias: 'Wings', type: 'aircrafts'
    },
    {
      nomValueId: 5, code: '5', name: 'Стабилизатори', nameAlt: 'Стабилизатори', parentValueId: null, alias: 'Stabilizers', type: 'aircrafts'
    },
    {
      nomValueId: 6, code: '6', name: 'Колесник', nameAlt: 'Колесник', parentValueId: null, alias: 'Landing', type: 'aircrafts'
    },
    {
      nomValueId: 7, code: '7', name: 'Двигатели / пилони / витла', nameAlt: 'Двигатели / пилони / витла', parentValueId: null, alias: 'Engines', type: 'aircrafts'
    },
    {
      nomValueId: 8, code: '8', name: 'Багажни / товарни отсеци', nameAlt: 'Багажни / товарни отсеци', parentValueId: null, alias: 'Baggage', type: 'aircrafts'
    },
    {
      nomValueId: 9, code: '9', name: 'Допълнителни отсеци', nameAlt: 'Допълнителни отсеци', parentValueId: null, alias: 'Additional compartments', type: 'aircrafts'
    },
    {
      nomValueId: 10, code: '10', name: 'СЕА', nameAlt: 'СЕА', parentValueId: null, alias: 'CEA', type: 'aircrafts'
    },
    {
      nomValueId: 11, code: '11', name: 'Съответствие с изискванията на ACAS/EGPWS/RVSM/BRNAV/AWO/FM immunity /други	', nameAlt: 'Съответствие с изискванията на ACAS/EGPWS/RVSM/BRNAV/AWO/FM immunity /други', parentValueId: null, alias: 'Accordance with the requirements', type: 'aircrafts'
    },
    {
      nomValueId: 12, code: '12', name: 'Изпитания', nameAlt: 'Изпитания', parentValueId: null, alias: 'Tests', type: 'aircrafts'
    },
    {
      nomValueId: 13, code: '13', name: 'Други специфични', nameAlt: 'Други специфични', parentValueId: null, alias: 'Other specific', type: 'aircrafts'
    },

    {
      nomValueId: 14, code: '14', name: 'Сгради и съоръжения', nameAlt: 'Сгради и съоръжения', parentValueId: null, alias: 'Buildings', type: 'organizations'
    },
    {
      nomValueId: 15, code: '15', name: 'Персонал', nameAlt: 'Персонал', parentValueId: null, alias: 'staff', type: 'organizations'
    },
    {
      nomValueId: 16, code: '16', name: 'Удостоверяващ персонал', nameAlt: 'Удостоверяващ персонал', parentValueId: null, alias: 'cert staff', type: 'organizations'
    },
    {
      nomValueId: 17, code: '17', name: 'Оборудване, инструменти и пр', nameAlt: 'Оборудване, инструменти и пр', parentValueId: null, alias: 'equipment', type: 'organizations'
    },
    {
      nomValueId: 18, code: '18', name: 'Приемане на компоненти', nameAlt: 'Приемане на компоненти', parentValueId: null, alias: 'components acceptance', type: 'organizations'
    },
    {
      nomValueId: 19, code: '19', name: 'Данни за ТО', nameAlt: 'Данни за ТО', parentValueId: null, alias: 'data for TO', type: 'organizations'
    },
    {
      nomValueId: 20, code: '20', name: 'Производствено планиране', nameAlt: 'Производствено планиране', parentValueId: null, alias: 'production planning', type: 'organizations'
    },
    {
      nomValueId: 21, code: '21', name: 'Удостоверяване на ТО', nameAlt: 'Удостоверяване на ТО', parentValueId: null, alias: 'cert for TO', type: 'organizations'
    },
    {
      nomValueId: 22, code: '22', name: 'Технически записи', nameAlt: 'Технически записи', parentValueId: null, alias: 'technical notes', type: 'organizations'
    },
    {
      nomValueId: 23, code: '24', name: 'Докладване на събития', nameAlt: 'Докладване на събития', parentValueId: null, alias: 'events announcement', type: 'organizations'
    },
    {
      nomValueId: 24, code: '25', name: 'Процедури и качество', nameAlt: 'Процедури и качество', parentValueId: null, alias: 'procedures and quality', type: 'organizations'
    }
  ];
})(typeof module === 'undefined' ? (this['auditPartRequirement'] = {}) : module);
