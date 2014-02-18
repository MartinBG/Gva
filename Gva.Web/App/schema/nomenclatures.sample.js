/*global module, require*/
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

    boolean: require('./boolean'),

    gender: require('./gender'),

    cities: require('./city'),

    addressTypes: require('./addressType'),

    organizations: require('./organization'),

    staffTypes: require('./staffType'),

    countries: require('./country'),

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
      },
      {
        nomTypeValueId: 6, code: '6', name: 'Обучение', nameAlt: 'DocumentTraining', alias: 'DocumentTraining', content: {
          setPartId: 6
        }
      }
      //{
      //  nomTypeValueId: 7, code: '7', name: '*', nameAlt: 'DocumentOther', alias: 'DocumentOther', content: {
      //    setPartId: 7
      //  }
      //}
    ],

    graduations: require('./graduation'),

    personIdDocumentTypes: require('./personIdDocumentType'),

    personOtherDocumentTypes: require('./personOtherDocumentType'),

    documentRoles: require('./documentRole'),

    employmentCategories: require('./employmentCategory'),

    personStatusTypes: require('./personStatusType'),

    medDocPublishers: require('./medDocPublisher'),

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

    medicalClasses: require('./medicalClass'),

    medicalLimitations: require('./medicalLimitation'),

    publisherTypes: require('./publisherType'),

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
