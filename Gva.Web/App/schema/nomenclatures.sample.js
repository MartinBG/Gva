/*global module*/
/*jshint maxlen:false*/
(function (module) {
  'use strict';

  module.exports = {
    getId: function (nom, alias) {
      return this[nom].filter(function (nomValue) {
        return nomValue.alias === alias;
      })[0].nomTypeValueId;
    },

    getName: function (nom, alias) {
      return this[nom].filter(function (nomValue) {
        return nomValue.alias === alias;
      })[0].name;
    },

    //Номенклатура Полове
    sex: [
      {nomTypeValueId: 1, code: '', name: 'Мъж', nameAlt: 'Мъж', alias: 'male'},
      {nomTypeValueId: 2, code: '', name: 'Жена', nameAlt: 'Female'},
      {nomTypeValueId: 3, code: '', name: 'Неопределен', nameAlt: 'Unknown'}
    ],
    
    //Номеклатура Държави
    countries: [
      {nomTypeValueId: 1, code: 'AT', name: 'Austria', nameAlt: 'Austria'},
      {nomTypeValueId: 2, code: 'BE', name: 'Belgium', nameAlt: 'Belgium'},
      {nomTypeValueId: 3, code: 'CY', name: 'Cyprus', nameAlt: 'Cyprus'},
      {nomTypeValueId: 4, code: 'CZ', name: 'Czech Republic', nameAlt: 'Czech Republic'},
      {nomTypeValueId: 33, code: 'BG', name: 'Република България', nameAlt: 'Republic of Bulgaria', alias: 'Bulgaria', content: {
        nationalityCode_CA: 'BGR',
        heading:'РЕПУБЛИКА БЪЛГАРИЯ',
        headingTrans:'REPUBLIC OF BULGARIA',
        licenceCode_CA: 'BGR.'
      }}
    ],

    //Населени места
    cities: [
      {nomTypeValueId: 4159, code: '68134', name: 'София', nameAlt: 'Sofia', alias: 'Sofia'},
      {nomTypeValueId: 4661, code: '56784', name: 'гр.Пловдив', nameAlt: 'gr.Plovdiv', alias: 'Plovdiv'}
    ],

    //Номенклатура Типове адреси
    addressTypes: [
      {nomTypeValueId: 1, code: 'PER', name: 'Постоянен адрес', nameAlt: 'Постоянен адрес', alias: 'Permanent'},
      {nomTypeValueId: 2, code: 'TMP', name: 'Настоящ адрес', nameAlt: 'Настоящ адрес'},
      {nomTypeValueId: 3, code: 'COR', name: 'Адрес за кореспонденция', nameAlt: 'Адрес за кореспонденция', alias: 'Correspondence'},
      {nomTypeValueId: 4, code: 'O', name: 'Седалище', nameAlt: 'Седалище'},
      {nomTypeValueId: 101, code: 'TOP', name: 'Данни за ръководител', nameAlt: 'Данни за ръководител'},
      {nomTypeValueId: 102, code: 'BOS', name: 'Данни за ръководител TO', nameAlt: 'Данни за ръководител TO'},
      {nomTypeValueId: 290, code: 'TO', name: 'Адрес за базово ослужване на ВС', nameAlt: 'Адрес за базово ослужване на ВС'}
    ],

    //Номенклатура Организации
    Organizations: [
      {nomTypeValueId: 203, code: '203', name: 'AAK Progres', nameAlt: 'AAK Progres', alias: 'AAK Progres'}
    ],

    //Номенклатура Категории персонал
    EmploymentCategories: [
      {nomTypeValueId: 631, code: '1', name: 'Директор', nameAlt: 'Director', content: {
        Code_CA: '',
        StaffTypeId: undefined
      }},
      {nomTypeValueId: 650, code: '11', name: 'Втори пилот', nameAlt: 'First officer', alias: 'First officer', content: {
        Code_CA: '11',
        StaffTypeId: 1
      }}
    ],

    //Номенклатура Учебни заведения
    Schools: [
      {nomTypeValueId: 673, code: '4', name: 'Университет за национално и световно стопанство (УНСС)-София', nameAlt: 'Университет за национално и световно стопанство (УНСС)-София', alias: 'UNSS', content: {
        graduationId: 1,
        graduationIds: [1, 3, 450],
        pilotTraining: false
      }},
      {nomTypeValueId: 1349, code: '218', name: 'Български въздухоплавателен център', nameAlt: 'Български въздухоплавателен център', alias: 'BAC', content: {
        graduationId: 450,
        graduationIds: [450],
        pilotTraining: true
      }}
    ],

    //Номенклатура Степени на образование
    Graduations: [
      {nomTypeValueId: 1, code: 'HS', name: 'Висше образование (бакалавър)', nameAlt: 'Висше образование (бакалавър)', alias: 'HS'},
      {nomTypeValueId: 3, code: 'HM', name: 'Висше образование (магистър)', nameAlt: 'Висше образование (магистър)', alias: 'HM'},
      {nomTypeValueId: 450, code: 'PQ', name: 'Професионална квалификация', nameAlt: 'Професионална квалификация', alias: 'PQ'}
    ],

    //Номенклатура Типове документи за самоличност на Физичеко лице
    PersonIdDocumentTypes: [
      {nomTypeValueId: 3, code: '3', name: 'Лична карта', nameAlt: 'Лична карта', alias: 'Id'},
      {nomTypeValueId: 4, code: '4', name: 'Задграничен паспорт', nameAlt: 'Задграничен паспорт'},
      {nomTypeValueId: 5, code: '5', name: 'Паспорт', nameAlt: 'Паспорт'}
    ],

    //Номенклатура Други типове документи на Физичеко лице
    PersonOtherDocumentTypes: [
      {nomTypeValueId: 683, code: '20', name: 'Писмо', nameAlt: 'Писмо', alias: 'Letter'},
      {nomTypeValueId: 702, code: '22', name: 'Справка', nameAlt: 'Справка', alias: 'Report'}
    ],

    //Номенклатура Други роли на документи на Физичеко лице
    PersonOtherDocumentRoles: [
      {nomTypeValueId: 683, code: '20', name: 'Писмо', nameAlt: 'Писмо', alias: 'Letter'},
      {nomTypeValueId: 702, code: '22', name: 'Справка', nameAlt: 'Справка', alias: 'Report'}
    ],

    //Номенклатура Типове сътояния на Физичеко лице
    PersonStatusTypes: [
      {nomTypeValueId: 1, code: '3', name: 'Негоден', nameAlt: 'Негоден', alias: 'Disabled'},
      {nomTypeValueId: 2, code: '4', name: 'Временно негоден', nameAlt: 'Временно негоден'},
      {nomTypeValueId: 3, code: '5', name: 'Майчинство', nameAlt: 'Майчинство'}
    ],

    //Номенклатура Издатели на документи - Други
    OtherDocPublishers: [
      {nomTypeValueId: 1, code: '', name: 'ЛИЧНО', nameAlt: ''},
      {nomTypeValueId: 301, code: '', name: 'МВР', nameAlt: '', alias: 'Mvr'}
    ]

  };
})(typeof module === 'undefined' ? (this['nomenclatures.sample'] = {}) : module);