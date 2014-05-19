/*global module, require*/
(function (module) {
  'use strict';

  //Номенклатура Класификатор на организации
  module.exports = [
    { nomValueId: 7919, code: 'ET', name: 'ET - БГ собственици - ЕТ, АД, ООД, ЕООД..', nameAlt: 'ET - BG Owners type ET, AD, OOD, EOOD..', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7920, code: 'LAP', name: 'LAP - Чуждестранни авиокомпании - ЛАП', nameAlt: 'LAP - Foreign Operators - FCL', nomTypeParentValueId: null, alias: 'LAP' },
    { nomValueId: 7921, code: 'OLF', name: 'OLF - Неактивни чуждестранни собственици', nameAlt: 'OLF - Old Foreign Owners', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7922, code: 'EF', name: 'EF - Чуждестранни собственици', nameAlt: 'EF - Foreign Owners', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7923, code: 'N/A', name: 'Неизвестно', nameAlt: 'Not applicable', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7924, code: 'AC', name: 'AC - Оператори -  търговска дейност', nameAlt: 'AC - Commercial Aviation Operators', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7925, code: 'AR', name: 'AR - Друга въздухоплавателна дейност (АХР)', nameAlt: 'AR - Other Aerial Works', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7926, code: 'AS', name: 'AS - Специализирани авиационни работи', nameAlt: 'AS - Specialized Aerial Works', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7927, code: 'AT', name: 'AT - Летателно обучение ( FTO )', nameAlt: 'AT - Flight Training Organisation ( FTO )', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7928, code: 'AX', name: 'AX - Организации в процес на одобрение', nameAlt: 'AX - Organisation on Approval Process', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7929, code: 'AY', name: 'AY - Временно спряни организации', nameAlt: 'AY - Temporarily Revoked Organisation', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7930, code: 'AZ', name: 'AZ - Други авиационни организации (некласирани)', nameAlt: 'AZ - Non Classified Aviation Organisation', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7931, code: 'CF', name: 'CF - Самостоятелни организации по Част-MF', nameAlt: 'CF - Part-MF Separate Organisations', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7932, code: 'CG', name: 'CG - Самостоятелни организации по Част-MG', nameAlt: 'CG - Part-MG Separate Organisations', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7933, code: 'CM', name: 'CM - Самостоятелни организации по Част-145', nameAlt: 'CM - Part-145 Separate Organisations', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7934, code: 'CT', name: 'CT - Самостоятелни организации по Част-147', nameAlt: 'CT - Part-147 Separate Organisations', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7935, code: 'EP', name: 'EP - Частни лица - собственици', nameAlt: 'EP - Private Persons - Owners', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7936, code: 'FN', name: 'FN - Други собственици, кредитори', nameAlt: 'FN - Other Owners, Mortgagee', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7937, code: 'GV', name: 'GV - Правителствени - министерства, агенции, летища', nameAlt: 'GV - Government Offices, Agencies, Airports', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7938, code: 'INT', name: 'Чуждестранни - собственици, лизингодатели', nameAlt: 'Чуждестранни - собственици, лизингодатели', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7939, code: 'OLD', name: 'OLD - Явно стари или грешни записи (без активни ВС)', nameAlt: 'OLD - Old (without aircrafts) or mistakes records ', nomTypeParentValueId: null, alias: null },
    { nomValueId: 7940, code: 'AAZ', name: 'Отпаднали организации - AO, OTO, УЦ, летища', nameAlt: 'Inactive Organizations - AOC, MO, TC, airports', nomTypeParentValueId: null, alias: null },
  ];
})(typeof module === 'undefined' ? (this['organizationsType'] = {}) : module);
