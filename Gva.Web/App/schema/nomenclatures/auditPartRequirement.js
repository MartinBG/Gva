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
      nomValueId: 23, code: '23', name: 'Докладване на събития', nameAlt: 'Докладване на събития', parentValueId: null, alias: 'events announcement', type: 'organizations'
    },
    {
      nomValueId: 24, code: '24', name: 'Процедури и качество', nameAlt: 'Процедури и качество', parentValueId: null, alias: 'procedures and quality', type: 'organizations'
    },
    {
      nomValueId: 25, code: '25', name: 'Корпоративен ангажимент на отговорния ръководител', nameAlt: 'Корпоративен ангажимент на отговорния ръководител', parentValueId: null, type: 'organizationRecommendations', group: 'Част 0 Обща организация', auditPart: '0.1'
    },
    {
      nomValueId: 26, code: '26', name: 'Обща информация', nameAlt: 'Обща информация', parentValueId: null, type: 'organizationRecommendations', group: 'Част 0 Обща организация', auditPart: '0.2'
    },
    {
      nomValueId: 27, code: '27', name: 'Ръководен персонал', nameAlt: 'Ръководен персонал', parentValueId: null, type: 'organizationRecommendations', group: 'Част 0 Обща организация', auditPart: '0.3'
    },
    {
      nomValueId: 28, code: '28', name: 'Организационна структурна схема', nameAlt: 'Организационна структурна схема', parentValueId: null, type: 'organizationRecommendations', group: 'Част 0 Обща организация', auditPart: '0.4'
    },
    {
      nomValueId: 29, code: '29', name: 'Използване на технически борден дневник и прилагане на МЕЛ(за търговски въздушни превози)Използване на система за записи на постоянна летателна годност(за нетърговски въздушни превози)', parentValueId: null, type: 'organizationRecommendations', group: 'Част 1 Процедури за управление постоянна ЛГ', auditPart: '1.1'
    },
    {
      nomValueId: 30, code: '30', name: 'Програма за ТО на ВС – разработка, изменение и одобрение', parentValueId: null, type: 'organizationRecommendations', group: 'Част 1 Процедури за управление постоянна ЛГ', auditPart: '1.2'
    },
    {
      nomValueId: 31, code: '31', name: 'Записи за времена и за постоянна летателна годност, отговорности, съхранение, достъп.', parentValueId: null, type: 'organizationRecommendations', group: 'Част 1 Процедури за управление постоянна ЛГ', auditPart: '1.3'
    },
    {
      nomValueId: 32, code: '32', name: 'Политика за качество на дейността по поддържане постоянна летателна годност, план и процедури за одит.', parentValueId: null, type: 'organizationRecommendations', group: 'Част 2 Система за качество', auditPart: '2.1'
    },
    {
      nomValueId: 33, code: '33', name: 'Наблюдения върху дейността по управление на постоянна ЛГ.', parentValueId: null, type: 'organizationRecommendations', group: 'Част 2 Система за качество', auditPart: '2.2'
    },


    {
      nomValueId: 34,  name: 'Сгради и съоръжения', nameAlt: 'Сгради и съоръжения', parentValueId: null, alias: 'Buildings', type: 'airports'
    },
    {
      nomValueId: 35,  name: 'Персонал', nameAlt: 'Персонал', parentValueId: null, alias: 'staff', type: 'airports'
    },
    {
      nomValueId: 36,  name: 'Удостоверяващ персонал', nameAlt: 'Удостоверяващ персонал', parentValueId: null, alias: 'cert staff', type: 'airports'
    },
    {
      nomValueId: 37,  name: 'Оборудване, инструменти и пр', nameAlt: 'Оборудване, инструменти и пр', parentValueId: null, alias: 'equipment', type: 'airports'
    },
    {
      nomValueId: 38,  name: 'Приемане на компоненти', nameAlt: 'Приемане на компоненти', parentValueId: null, alias: 'components acceptance', type: 'airports'
    },
    {
      nomValueId: 39,  name: 'Данни за ТО', nameAlt: 'Данни за ТО', parentValueId: null, alias: 'data for TO', type: 'airports'
    },
    {
      nomValueId: 40,  name: 'Производствено планиране', nameAlt: 'Производствено планиране', parentValueId: null, alias: 'production planning', type: 'airports'
    },
    {
      nomValueId: 41,  name: 'Удостоверяване на ТО', nameAlt: 'Удостоверяване на ТО', parentValueId: null, alias: 'cert for TO', type: 'airports'
    },
    {
      nomValueId: 42,  name: 'Технически записи', nameAlt: 'Технически записи', parentValueId: null, alias: 'technical notes', type: 'airports'
    },
    {
      nomValueId: 43,  name: 'Докладване на събития', nameAlt: 'Докладване на събития', parentValueId: null, alias: 'events announcement', type: 'airports'
    },
    {
      nomValueId: 44,  name: 'Процедури и качество', nameAlt: 'Процедури и качество', parentValueId: null, alias: 'procedures and quality', type: 'airports'
    },
  ];
})(typeof module === 'undefined' ? (this['auditPartRequirement'] = {}) : module);
