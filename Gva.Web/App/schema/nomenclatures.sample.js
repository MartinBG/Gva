﻿/*global module, require*/
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

    get: function (nom, alias) {
      return this[nom].filter(function (nomValue) {
        return nomValue.alias === alias;
      })[0];
    },

    units: [
     { nomTypeValueId: 1, code: '', name: 'admin (Администратор)', nameAlt: '', alias: '' },
     { nomTypeValueId: 2, code: '', name: 'Анка Ангелова Тонова (СТАРШИ ЕКСПЕРТ)', nameAlt: '', alias: '' },
     { nomTypeValueId: 3, code: '', name: 'Анна Аристидова Андонова (НАЧАЛНИК НА ОТДЕЛ)', nameAlt: '', alias: '' },
     { nomTypeValueId: 4, code: '', name: 'Антонио Красимиров Донов (ИНСПЕКТОР)', nameAlt: '', alias: '' },
     { nomTypeValueId: 5, code: '', name: 'Петър', nameAlt: '', alias: '' }
    ],

    assignmentTypes: [
     { nomTypeValueId: 1, code: '', name: 'Със срок', nameAlt: '', alias: '' },
     { nomTypeValueId: 2, code: '', name: 'Без срок', nameAlt: '', alias: '' }
    ],

    docSourceTypes: [
      { nomTypeValueId: 1, code: '', name: 'Интернет', nameAlt: '', alias: '' },
      { nomTypeValueId: 2, code: '', name: 'Подадено на гише', nameAlt: '', alias: '' }
    ],

    docDestinationTypes: [
       { nomTypeValueId: 1, code: '', name: 'Имейл', nameAlt: '', alias: '' },
       { nomTypeValueId: 2, code: '', name: 'По куриер', nameAlt: '', alias: '' }
    ],

    //Номенклатура Статуси на документ
    docStatuses: [
      { nomTypeValueId: 1, code: '', name: 'Чернова', nameAlt: '', alias: 'Draft' },
      { nomTypeValueId: 2, code: '', name: 'Изготвен', nameAlt: '', alias: 'Prepared' },
      { nomTypeValueId: 3, code: '', name: 'Обработен', nameAlt: '', alias: 'Processed' },
      { nomTypeValueId: 4, code: '', name: 'Приключен', nameAlt: '', alias: 'Finished' },
      { nomTypeValueId: 5, code: '', name: 'Отхвърлен', nameAlt: '', alias: 'Canceled' }
    ],

    docSubjects: [
      { nomTypeValueId: 1, code: '', name: 'Подадено заявление', nameAlt: '', alias: '' },
      { nomTypeValueId: 2, code: '', name: 'Резолюция', nameAlt: '', alias: '' },
      { nomTypeValueId: 3, code: '', name: 'Забелвжка', nameAlt: '', alias: '' },
      { nomTypeValueId: 4, code: '', name: 'Задача', nameAlt: '', alias: '' },
      { nomTypeValueId: 5, code: '', name: 'Други', nameAlt: '', alias: '' }
    ],

    //Номенклатура Кореспондентска група
    correspondentGroups: [
      { nomTypeValueId: 1, code: '', name: 'Министерски съвет', nameAlt: '', alias: '' },
      { nomTypeValueId: 2, code: '', name: 'Заявители', nameAlt: 'Applicants', alias: 'Applicants' },
      { nomTypeValueId: 3, code: '', name: 'Системни', nameAlt: 'System', alias: 'System' }
    ],

    //Номенклатура Тип кореспондент
    correspondentTypes: [
     { nomTypeValueId: 1, code: '', name: 'Български гражданин', nameAlt: 'BulgarianCitizen', alias: 'BulgarianCitizen' },
     { nomTypeValueId: 2, code: '', name: 'Чужденец', nameAlt: 'Foreigner', alias: 'Foreigner' },
     { nomTypeValueId: 3, code: '', name: 'Юридическо лице', nameAlt: 'LegalEntity', alias: 'LegalEntity' },
     { nomTypeValueId: 4, code: '', name: 'Чуждестранно юридическо лице', nameAlt: 'ForeignLegalEntity', alias: 'ForeignLegalEntity' }
    ],

    parentNom: [
      { nomTypeValueId: 1, code: '', name: 'P1', nameAlt: '', alias: 'P1' },
      { nomTypeValueId: 2, code: '', name: 'P2', nameAlt: '', alias: 'P2' },
    ],

    childrenNom: [
      { nomTypeValueId: 1, code: '', name: 'CH1', nameAlt: '', alias: 'CH1', parentId: 1 },
      { nomTypeValueId: 2, code: '', name: 'CH2', nameAlt: '', alias: 'CH2', parentId: 1 },
      { nomTypeValueId: 3, code: '', name: 'CH3', nameAlt: '', alias: 'CH3', parentId: 2 },
      { nomTypeValueId: 4, code: '', name: 'CH4', nameAlt: '', alias: 'CH4', parentId: 2 }
    ],

    //Номенклатура Булеви стойности
    boolean: [
      { nomTypeValueId: 1, code: 'Y', name: 'Да', nameAlt: 'Yes', alias: 'true' },
      { nomTypeValueId: 2, code: 'N', name: 'Не', nameAlt: 'No', alias: 'false' }
    ],

    //Номенклатура Полове
    sex: [
      { nomTypeValueId: 1, code: 'M', name: 'Мъж', nameAlt: 'Male', alias: 'male' },
      { nomTypeValueId: 2, code: 'W', name: 'Жена', nameAlt: 'Female', alias: 'female' },
      { nomTypeValueId: 3, code: 'U', name: 'Неизвестен', nameAlt: 'Unknown', alias: 'unknown' }
    ],

    //Номеклатура Държави
    countries: [
      {
        nomTypeValueId: 26, code: 'AT', name: 'Austria', nameAlt: 'Austria', content: {
          nationalityCodeCA: 'AT',
          heading: '-', 
          headingAlt: '-', 
          licenceCodeCA: 'A'
        }
      },
      {
        nomTypeValueId: 27, code: 'BE', name: 'Belgium', nameAlt: 'Belgium', content: {
          nationalityCodeCA: 'BE',
          heading: '-',
          headingAlt: '-',
          licenceCodeCA: 'B'
        }
      },
      {
        nomTypeValueId: 28, code: 'CY', name: 'Cyprus', nameAlt: 'Cyprus', content: {
          nationalityCodeCA: 'CY',
          heading: '-',
          hHeadingAlt: '-',
          licenceCodeCA: 'CY'
        }
      },
      {
        nomTypeValueId: 29, code: 'CZ', name: 'Czech Republic', nameAlt: 'Czech Republic', content: {
          nationalityCodeCA: 'CZ',
          heading: '-',
          headingAlt: '-',
          licenceCodeCA: 'CZ'
        }
      },
      {
        nomTypeValueId: 33, code: 'BG', name: 'Република България', nameAlt: 'Republic of Bulgaria', alias: 'Bulgaria', content: {
          nationalityCodeCA: 'BGR',
          heading: 'РЕПУБЛИКА БЪЛГАРИЯ',
          headingAlt: 'REPUBLIC OF BULGARIA',
          licenceCodeCA: 'BGR.'
        }
      }
    ],

    //Населени места
    cities: [
      { nomTypeValueId: 4159, code: '68134', name: 'София', nameAlt: 'Sofia', alias: 'Sofia' },
      { nomTypeValueId: 4661, code: '56784', name: 'гр.Пловдив', nameAlt: 'gr.Plovdiv', alias: 'Plovdiv' }
    ],

    //Номенклатура Типове адреси
    addressTypes: [
      {
        nomTypeValueId: 5583, code: 'PER', name: 'Постоянен адрес', nameAlt: 'Постоянен адрес', alias: 'Permanent', content: {
          type: 'P'
        }
      },
      {
        nomTypeValueId: 5584, code: 'TMP', name: 'Настоящ адрес', nameAlt: 'Настоящ адрес', content: {
          type: 'P'
        }
      },
      {
        nomTypeValueId: 5585, code: 'COR', name: 'Адрес за кореспонденция', nameAlt: 'Адрес за кореспонденция', alias: 'Correspondence', content: {
          type: 'P'
        }
      },
      {
        nomTypeValueId: 5586, code: 'O', name: 'Седалище', nameAlt: 'Седалище', content: {
          type: 'F'
        }
      },
      {
        nomTypeValueId: 5587, code: 'TOP', name: 'Данни за ръководител', nameAlt: 'Данни за ръководител', content: {
          type: 'F'
        }
      },
      { 
        nomTypeValueId: 5588, code: 'BOS', name: 'Данни за ръководител TO', nameAlt: 'Данни за ръководител TO', content: {
          type: 'F'
        }
      },
      {
        nomTypeValueId: 5582, code: 'TO', name: 'Адрес за базово ослужване на ВС', nameAlt: 'Адрес за базово ослужване на ВС', content: {
          type: 'F'
        }
      }
    ],

    organizations: require('./organization'),

    staffTypes: require('./staffType'),

    //Номенклатура Категории персонал
    employmentCategories: [
      {
        nomTypeValueId: 631, code: '1', name: 'Директор', nameAlt: 'Director', content: {
          Code_CA: '',
          StaffTypeId: null
        }
      },
      {
        nomTypeValueId: 650, code: '11', name: 'Втори пилот', nameAlt: 'First officer', alias: 'First officer', content: {
          Code_CA: '11',
          StaffTypeId: 1
        }
      }
    ],

    schools: require('./school'),

    documentParts: [
      {
        nomTypeValueId: 1, code: '1', name: 'Документ за самоличност', nameAlt: 'DocumentId', alias: 'DocumentId', content: {
          setPartId: 1
        }
      },
      {
        nomTypeValueId: 2, code: '2', name: 'Образования', nameAlt: 'DocumentEducation', alias: 'DocumentEducation', content: {
          setPartId: 2
        }
      },
      {
        nomTypeValueId: 3, code: '3', name: 'Месторабота', nameAlt: 'DocumentEmployment', alias: 'DocumentEmployment', content: {
          setPartId: 3
        }
      },
      {
        nomTypeValueId: 4, code: '4', name: 'Медицински', nameAlt: 'DocumentMed', alias: 'DocumentMed', content: {
          setPartId: 4
        }
      },
      {
        nomTypeValueId: 5, code: '5', name: 'Проверка', nameAlt: 'DocumentCheck', alias: 'DocumentCheck', content: {
          setPartId: 5
        }
      }
      //{
      //  nomTypeValueId: 6, code: '6', name: '*', nameAlt: 'DocumentTraining', alias: 'DocumentTraining', content: {
      //    setPartId: 6
      //  }
      //},
      //{
      //  nomTypeValueId: 7, code: '7', name: '*', nameAlt: 'DocumentOther', alias: 'DocumentOther', content: {
      //    setPartId: 7
      //  }
      //}
    ],

    graduations: require('./graduation'),

    //Номенклатура Типове документи за самоличност на Физичеко лице
    personIdDocumentTypes: [
      { nomTypeValueId: 3, code: '3', name: 'Лична карта', nameAlt: 'Лична карта', alias: 'Id' },
      { nomTypeValueId: 4, code: '4', name: 'Задграничен паспорт', nameAlt: 'Задграничен паспорт' },
      { nomTypeValueId: 5, code: '5', name: 'Паспорт', nameAlt: 'Паспорт', alias: 'passport' },
      { nomTypeValueId: 5, code: '5', name: 'Разрешение за пребиваване', nameAlt: 'Разрешение за пребиваване', alias: 'allowance' }
    ],

    personOtherDocumentTypes: require('./personOtherDocumentType'),

    documentRoles: require('./documentRole'),

    //Номенклатура Типове състояния на Физичеко лице
    personStatusTypes: [
      { nomTypeValueId: 1, code: '3', name: 'Негоден', nameAlt: 'Негоден', alias: 'permanently unfit' },
      { nomTypeValueId: 2, code: '4', name: 'Временно негоден', nameAlt: 'Временно негоден', alias: 'temporary unfit' },
      { nomTypeValueId: 3, code: '5', name: 'Майчинство', nameAlt: 'Майчинство', alias: 'maternity leave' }
    ],

    //Номенклатура Издатели на документи - Медицински
    medDocPublishers: [
      { nomTypeValueId: 6839, name: 'AMC Latvia', alias: 'AMC Latvia' },
      { nomTypeValueId: 6830, name: 'AMC PRAGUE', alias: 'CAA France' },
      { nomTypeValueId: 6829, name: 'AME-SWETZERLAND', alias: 'AME-SWETZERLAND' },
      { nomTypeValueId: 6828, name: 'Austro Control', alias: 'Austro Control' },
      { nomTypeValueId: 6834, name: 'CAA France', alias: 'CAA France' },
      { nomTypeValueId: 6832, name: 'FAA', alias: 'FAA' },
      { nomTypeValueId: 6831, name: 'FR AMC', alias: 'FR AMC' },
      { nomTypeValueId: 6833, name: 'GCAA UAE', alias: 'GCAA UAE' },
      { nomTypeValueId: 6835, name: 'GR AME', alias: 'GR AME' },
      { nomTypeValueId: 6827, name: 'ROMANIAN CIVIL AERONAUTICAL AUTHORITY-AME No.02' },
      { nomTypeValueId: 6838, name: 'TR-AME-008/2', alias: 'TR-AME-008/2' },
      { nomTypeValueId: 6836, name: 'UK AME', alias: 'UK AME' },
      { nomTypeValueId: 6837, name: 'АМЦ01', alias: 'AMC01' },
      { nomTypeValueId: 6826, name: 'КАМО', alias: 'KAMO' }
    ],

    //Номенклатура Издатели на документи - Други
    OtherDocPublishers: [
      { nomTypeValueId: 1, code: '', name: 'ЛИЧНО', nameAlt: '' },
      { nomTypeValueId: 301, code: '', name: 'МВР', nameAlt: '', alias: 'Mvr' }
    ],

    ratingTypes: require('./ratingType'),

    ratingClassGroups: require('./ratingClassGroup'),

    ratingClasses: require('./ratingClass'),

    //Номенклатура Подкласове ВС за екипажи
    ratingSubClasses: [
      { nomTypeValueId: 1, code: 'A1', name: 'Подклас А1', nameAlt: 'Подклас А1', alias: 'A1' },
      { nomTypeValueId: 2, code: 'A2', name: 'Подклас А2', nameAlt: 'Подклас А2', alias: 'A2' },
      { nomTypeValueId: 3, code: 'A3', name: 'Подклас А3', nameAlt: 'Подклас А3', alias: 'A3' },
      { nomTypeValueId: 4, code: 'A4', name: 'Подклас А4', nameAlt: 'Подклас А4', alias: 'A4' }
    ],

    //Номенклатура Модел на квалификация на Физическо лице
    personRatingModels: [
      { nomTypeValueId: 1, code: 'permanent', name: 'Постоянно', nameAlt: 'Постоянно', alias: 'permanent' },
      { nomTypeValueId: 2, code: 'temporary', name: 'Временно', nameAlt: 'Временно', alias: 'temporary' }
    ],

    authorizationGroups: require('./authorizationGroup'),

    authorizations: require('./authorization'),

    licenceTypes: require('./licenceType'),

    //Номенклатура Нива на владеене на английски език
    engLangLevels: [
      { nomTypeValueId: 1, code: 'L4', name: 'Работно (Ниво 4)', nameAlt: 'Operational (Level  4)', alias: 'L4' },
      { nomTypeValueId: 2, code: 'L5', name: 'Разширено (Ниво  5)', nameAlt: 'Extended (Level  5)', alias: 'L5' },
      { nomTypeValueId: 3, code: 'L6', name: 'Експерт (Ниво  6)', nameAlt: 'Expert (Level  6)', alias: 'L6' }
    ],

    locationIndicators: require('./locationIndicator'),

    //Номенклатура Държатели на ТС за ВС
    aircraftTCHolders: [
      { nomTypeValueId: 1, code: '', name: 'Еърбъс', nameAlt: 'Airbus' },
      { nomTypeValueId: 3, code: '', name: 'Чесна Еъркрафт Къмпани', nameAlt: 'Cessna Aircraft Company' },
      { nomTypeValueId: 23, code: '', name: 'Saab AB, Saab Aerosystems', nameAlt: 'Saab AB, Saab Aerosystems' },
      { nomTypeValueId: 24, code: '', name: 'Avions de Transport Regional (ATR)', nameAlt: 'Avions de Transport Regional (ATR)' },
      { nomTypeValueId: 25, code: '', name: 'Бритиш Аероспейс Системс (БАе Системс)', nameAlt: 'British Aerospace Systems (BAe Systems)' },
      { nomTypeValueId: 26, code: '', name: 'Construcciones Aeronauticas, S.A.', nameAlt: 'Construcciones Aeronauticas, S.A.' },
      { nomTypeValueId: 27, code: '', name: 'Бомбардиер', nameAlt: 'Bombardier Inc.' }
    ],

    aircraftTypes: require('./aircraftType'),

    aircraftTypeGroups: require('./aircraftTypeGroup'),

    //Номенклатура DocFormatTypes
    docFormatTypes: [
      {
        "docFormatTypeId": 1,
        "name": "Електронен",
        "alias": "Electronic",
        "isActive": true,
        "version": "AAAAAAAAIBI=",
        "docs": []
      },
      {
        "docFormatTypeId": 2,
        "name": "Електронен с хартия",
        "alias": "ElectronicWithPaper",
        "isActive": false,
        "version": "AAAAAAAAIBM=",
        "docs": []
      },
      {
        "docFormatTypeId": 3,
        "name": "Хартиен",
        "alias": "Paper",
        "isActive": false,
        "version": "AAAAAAAAIBQ=",
        "docs": []
      }
    ],

    //Номенклатура DocCasePartTypes
    docCasePartTypes: [
      {
        "docCasePartTypeId": 1,
        "name": "Публичен",
        "alias": "Public",
        "description": null,
        "version": "AAAAAAAAHtw=",
        "isActive": true,
        "docCasePartMovements": [],
        "docs": []
      },
      {
        "docCasePartTypeId": 2,
        "name": "Вътрешен",
        "alias": "Internal",
        "description": null,
        "version": "AAAAAAAAHt0=",
        "isActive": false,
        "docCasePartMovements": [],
        "docs": []
      },
      {
        "docCasePartTypeId": 3,
        "name": "Контролен",
        "alias": "Control",
        "description": null,
        "version": "AAAAAAAAHt4=",
        "isActive": false,
        "docCasePartMovements": [],
        "docs": []
      }
    ],

    //Номенклатура DocDirections
    docDirections: [
      {
        "docDirectionId": 1,
        "name": "Входящ",
        "alias": "Incomming",
        "isActive": true,
        "version": "AAAAAAAAHsg=",
        "docTypeClassifications": [],
        "docTypeUnitRoles": [],
        "docs": []
      },
      {
        "docDirectionId": 2,
        "name": "Вътрешен",
        "alias": "Internal",
        "isActive": false,
        "version": "AAAAAAAAHsk=",
        "docTypeClassifications": [],
        "docTypeUnitRoles": [],
        "docs": []
      },
      {
        "docDirectionId": 3,
        "name": "Изходящ",
        "alias": "Outgoing",
        "isActive": false,
        "version": "AAAAAAAAHso=",
        "docTypeClassifications": [],
        "docTypeUnitRoles": [],
        "docs": []
      },
      {
        "docDirectionId": 4,
        "name": "Циркулярен",
        "alias": "InternalOutgoing",
        "isActive": false,
        "version": "AAAAAAAAHss=",
        "docTypeClassifications": [],
        "docTypeUnitRoles": [],
        "docs": []
      }
    ],

    //Номенклатура DocTypeGroups
    docTypeGroups: [
      { nomTypeValueId: 1, code: '', name: 'Общи', nameAlt: '', alias: 'common' },
      { nomTypeValueId: 2, code: '', name: 'Електронни услуги', nameAlt: '', alias: 'electronicService' },
      { nomTypeValueId: 3, code: '', name: 'Отгвори на услуги', nameAlt: '', alias: 'serviceAnswers' },
      { nomTypeValueId: 4, code: '', name: 'Други', nameAlt: '', alias: 'others' }
    ],

    //Номенклатура DocTypes
    docTypes: [
      { nomTypeValueId: 1, code: '', name: 'Резолюция', nameAlt: '', alias: 'resolution', parentId: 1 },
      { nomTypeValueId: 2, code: '', name: 'Задача', nameAlt: '', alias: 'task', parentId: 1 },
      { nomTypeValueId: 3, code: '', name: 'Забележка', nameAlt: '', alias: 'note', parentId: 1 },
      { nomTypeValueId: 4, code: '', name: 'Писмо', nameAlt: '', alias: 'letter', parentId: 1 },

      { nomTypeValueId: 5, code: 'М12.1.5', name: 'Издаване на свидетелство за правоспособност на авиационен персонал – пилоти', nameAlt: '', alias: '', parentId: 2 },
      { nomTypeValueId: 6, code: 'М12.1.6', name: 'Издаване на свидетелство за правоспособност на авиационен персонал – кабинен екипаж, полетни диспечери, бордни инженери, щурмани, бордни съпроводители', nameAlt: '', alias: '', parentId: 2 },
      { nomTypeValueId: 7, code: 'М12.1.8', name: 'Признаване на свидетелство за правоспособност на чужди граждани', nameAlt: '', alias: '', parentId: 2 },
      { nomTypeValueId: 8, code: 'М12.1.14', name: 'Издаване на свидетелство за правоспособност на ръководители на полети', nameAlt: '', alias: '', parentId: 2 },
      { nomTypeValueId: 9, code: 'М12.1.15', name: 'Издаване на свидетелство за правоспособност на инженерно-технически състав по обслужване на средствата за управление на въздушното движение (УВД), на ученик -  ръководители на полети, на асистент координатори на полети и на координатори по УВД', nameAlt: '', alias: '', parentId: 2 },
      { nomTypeValueId: 10, code: 'М12.1.7', name: 'Издаване на свидетелство за правоспособност за техническо обслужване на самолети и хеликоптери', nameAlt: '', alias: '', parentId: 2 },

      { nomTypeValueId: 11, code: '', name: 'Приемно предавателен протокол', nameAlt: '', alias: 'protocol', parentId: 4 }
    ],

    //тестови данни за кореспондент
    testCorrespondents: [
      { nomTypeValueId: 1, code: '', name: 'Мирослав Георгиев', nameAlt: '', alias: 'mirko' },
      { nomTypeValueId: 2, code: '', name: 'Янислав Гальов', nameAlt: '', alias: 'yani' },
      { nomTypeValueId: 3, code: '', name: 'Цветан Белчев', nameAlt: '', alias: 'seso' },
      { nomTypeValueId: 4, code: '', name: 'Георги Йорданов', nameAlt: '', alias: 'georgi' },
      { nomTypeValueId: 5, code: '', name: 'Ангел Йорданов', nameAlt: '', alias: 'angel' }
    ],

    //kласове за медицинси
    medicalClassTypes: [
      { nomTypeValueId: 7824, code: '01', name: 'Class-1', nameAlt: 'Class-1', alias: 'class1' },
      { nomTypeValueId: 7825, code: '02', name: 'Class-2', nameAlt: 'Class-2', alias: 'class2' },
      { nomTypeValueId: 7826, code: '03', name: 'Class-3', nameAlt: 'Class-3', alias: 'class3' },
      { nomTypeValueId: 7827, code: '04', name: 'Class-4', nameAlt: 'Class-4', alias: 'class4' }
    ],

    //Oграничения за медицински
    medicalLimitationTypes: [
      { nomTypeValueId: 7836, code: 'MCL', name: 'MCL', nameAlt: 'MCL', alias: 'MCL' },
      { nomTypeValueId: 7831, code: 'OCL', name: 'OCL', nameAlt: 'OCL', alias: 'OCL' },
      { nomTypeValueId: 7829, code: 'OFL', name: 'OFL', nameAlt: 'OFL', alias: 'OFL' },
      { nomTypeValueId: 7835, code: 'OML', name: 'OML', nameAlt: 'OML', alias: 'OML' },
      { nomTypeValueId: 7828, code: 'OSL', name: 'OSL', nameAlt: 'OSL', alias: 'OSL' },
      { nomTypeValueId: 7833, code: 'TML', name: 'TML', nameAlt: 'TML', alias: 'TML' },
      { nomTypeValueId: 7832, code: 'VDL', name: 'VDL', nameAlt: 'VDL', alias: 'VDL' },
      { nomTypeValueId: 7830, code: 'VML', name: 'VML', nameAlt: 'VML', alias: 'VML' },
      { nomTypeValueId: 7834, code: 'VNL', name: 'VNL', nameAlt: 'VNL', alias: 'VNL' }
    ],

    personCheckDocumentTypes: require('./personCheckDocumentType'),

    //оценки при проверка на Физическо лице
    personCheckRatingValues: [
       { nomTypeValueId: 1, code: 'Goog', name: 'Добро', nameAlt: 'good', alias: 'good' },
       { nomTypeValueId: 2, code: 'Sat', name: 'Задоволително', nameAlt: 'Задоволително', alias: 'satisfactory' },
       { nomTypeValueId: 3, code: 'Ins', name: 'Недостатъчно', nameAlt: 'Недостатъчно', alias: 'insufficient' },
       { nomTypeValueId: 4, code: 'Unac', name: 'Неприемливо', nameAlt: 'Неприемливо', alias: 'unacceptable' },
       { nomTypeValueId: 4, code: 'Comp', name: 'Компетентен', nameAlt: 'Компетентен', alias: 'competent' },
       { nomTypeValueId: 4, code: 'Incomp', name: 'Некомпетентен', nameAlt: 'Некомпетентен', alias: 'incompetent' },
    ],
    
    personCheckDocumentRoles: require('./personCheckDocumentRole'),

    aircrafts: require('./aircraft'),

    experienceRoles: require('./experienceRole'),

    experienceMeasures: require('./experienceMeasure'),

    //Номенклатура Степени на квалификационен клас на Физичеко лице
    personRatingLevels: [
     { nomTypeValueId: 1, code: 'A', name: 'степен А', nameAlt: 'ratingA', alias: 'A' },
     { nomTypeValueId: 2, code: 'B', name: 'степен Б', nameAlt: 'ratingB', alias: 'B' },
     { nomTypeValueId: 3, code: 'C', name: 'степен C', nameAlt: 'ratingC', alias: 'C' }
    ],

    inspectors: [
      { nomTypeValueId: 1, code: '1', name: 'Владимир Бонев Текнеджиев', nameAlt: 'Vladimi Bonev Teknedjiev', alias: 'Vladimir' },
      { nomTypeValueId: 2, code: '2', name: 'Ваня Наумова Георгиева', nameAlt: 'Vanq Naumova Georgieva', alias: 'Vanq' },
      { nomTypeValueId: 3, code: '3', name: 'Георги Мишев Христов', nameAlt: 'Georgi Mishev Hristov', alias: 'Georgi' }
    ],

    //Oграничения за класове
    ratingLimitationTypes: [
      { nomTypeValueId: 1, code: 'MCL', name: 'MCL', nameAlt: 'MCL', alias: 'MCL' },
      { nomTypeValueId: 2, code: 'OCL', name: 'OCL', nameAlt: 'OCL', alias: 'OCL' },
      { nomTypeValueId: 3, code: 'OFL', name: 'OFL', nameAlt: 'OFL', alias: 'OFL' },
      { nomTypeValueId: 4, code: 'OML', name: 'OML', nameAlt: 'OML', alias: 'OML' }
    ],

    //Номенклатура Клас
    ratingCategories: [
      { nomTypeValueId: 1, code: 'A', name: 'A', nameAlt: 'A', alias: 'A' },
      { nomTypeValueId: 2, code: 'A1', name: 'A1', nameAlt: 'A1', alias: 'A1' },
      { nomTypeValueId: 3, code: 'A2', name: 'A2', nameAlt: 'A2', alias: 'A2' },
      { nomTypeValueId: 4, code: 'B1', name: 'B1', nameAlt: 'B1', alias: 'B1' }
    ],

  };
})(typeof module === 'undefined' ? (this['nomenclatures.sample'] = {}) : module);
