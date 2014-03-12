/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Изисквания към раздел
  module.exports = [
    {
      nomValueId: 1, code: '1', name: 'Пилотска кабина', nameAlt: 'Пилотска кабина', parentValueId: null, alias: 'Cockpit'
    },
    {
      nomValueId: 2, code: '2', name: 'Пътнически салони, тоалетни и кухни', nameAlt: 'Пътнически салони, тоалетни и кухни', parentValueId: null, alias: 'Facilities'
    },
    {
      nomValueId: 3, code: '3', name: 'Фюзелаж', nameAlt: 'Фюзелаж', parentValueId: null, alias: 'Fuselage'
    },
    {
      nomValueId: 4, code: '4', name: 'Крила', nameAlt: 'Крила', parentValueId: null, alias: 'Wings'
    },
    {
      nomValueId: 5, code: '5', name: 'Стабилизатори', nameAlt: 'Стабилизатори', parentValueId: null, alias: 'Stabilizers'
    },
    {
      nomValueId: 6, code: '6', name: 'Колесник', nameAlt: 'Колесник', parentValueId: null, alias: 'Landing'
    },
    {
      nomValueId: 7, code: '7', name: 'Двигатели / пилони / витла', nameAlt: 'Двигатели / пилони / витла', parentValueId: null, alias: 'Engines'
    },
    {
      nomValueId: 8, code: '8', name: 'Багажни / товарни отсеци', nameAlt: 'Багажни / товарни отсеци', parentValueId: null, alias: 'Baggage'
    },
    {
      nomValueId: 9, code: '9', name: 'Допълнителни отсеци', nameAlt: 'Допълнителни отсеци', parentValueId: null, alias: 'Additional compartments'
    },
    {
      nomValueId: 10, code: '10', name: 'СЕА', nameAlt: 'СЕА', parentValueId: null, alias: 'CEA'
    },
    {
      nomValueId: 11, code: '11', name: 'Съответствие с изискванията на ACAS/EGPWS/RVSM/BRNAV/AWO/FM immunity /други	', nameAlt: 'Съответствие с изискванията на ACAS/EGPWS/RVSM/BRNAV/AWO/FM immunity /други', parentValueId: null, alias: 'Accordance with the requirements'
    },
    {
      nomValueId: 12, code: '12', name: 'Изпитания', nameAlt: 'Изпитания', parentValueId: null, alias: 'Tests'
    },
    {
      nomValueId: 13, code: '13', name: 'Други специфични', nameAlt: 'Други специфични', parentValueId: null, alias: 'Other specific'
    }
  ];
})(typeof module === 'undefined' ? (this['auditPartRequirement'] = {}) : module);
