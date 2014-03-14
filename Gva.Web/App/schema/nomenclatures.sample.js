/*global module, require*/
/*jshint maxlen:false*/
(function (module) {
  'use strict';

  module.exports = {
    getId: function (nom, alias) {
      return this[nom].filter(function (nomValue) {
        return nomValue.alias === alias;
      })[0].nomValueId;
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

    getById: function (nom, id) {
      return this[nom].filter(function (nomValue) {
        return nomValue.nomValueId === id;
      })[0];
    },

    docFileKinds: [
      { nomValueId: 1, code: '', name: 'Вътрешен файл', nameAlt: '', alias: 'PrivateAttachedFile' },
      { nomValueId: 2, code: '', name: 'Публичен файл', nameAlt: '', alias: 'PublicAttachedFile' }
    ],

    docFileTypes: [
     { nomValueId: 1, code: '', name: 'Документ Microsoft Word (.doc)', nameAlt: '', alias: 'DOC' },
     { nomValueId: 2, code: '', name: 'Документ Microsoft Word (.docx)', nameAlt: '', alias: 'DOCX' },
     { nomValueId: 3, code: '', name: 'Документ Microsoft Excel (.xls)', nameAlt: '', alias: 'XLS' },
     { nomValueId: 4, code: '', name: 'Документ Microsoft Excel (.xlsx)', nameAlt: '', alias: 'XLSX' },
     { nomValueId: 5, code: '', name: 'Документ в преносим формат (.pdf)', nameAlt: '', alias: 'PDF' },
     { nomValueId: 6, code: '', name: 'Текстов документ(.txt)', nameAlt: '', alias: 'TXT' }
    ],

    unit: require('./unit'),

    assignmentType: require('./assignmentType'),

    docSourceType: require('./docSourceType'),

    docDestinationType: require('./docDestinationType'),

    docStatus: require('./docStatus'),

    docSubject: require('./docSubject'),

    correspondentGroup: require('./correspondentGroup'),

    correspondentType: require('./correspondentType'),

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
        nomValueId: 1, code: '1', name: 'Документ за самоличност', nameAlt: 'DocumentId', alias: 'DocumentId', textContent: {
          setPartId: 1
        }
      },
      {
        nomValueId: 2, code: '2', name: 'Образования', nameAlt: 'DocumentEducation', alias: 'DocumentEducation', textContent: {
          setPartId: 2
        }
      },
      {
        nomValueId: 3, code: '3', name: 'Месторабота', nameAlt: 'DocumentEmployment', alias: 'DocumentEmployment', textContent: {
          setPartId: 3
        }
      },
      {
        nomValueId: 4, code: '4', name: 'Медицински', nameAlt: 'DocumentMed', alias: 'DocumentMed', textContent: {
          setPartId: 4
        }
      },
      {
        nomValueId: 5, code: '5', name: 'Проверка', nameAlt: 'DocumentCheck', alias: 'DocumentCheck', textContent: {
          setPartId: 5
        }
      },
      {
        nomValueId: 6, code: '6', name: 'Обучение', nameAlt: 'DocumentTraining', alias: 'DocumentTraining', textContent: {
          setPartId: 6
        }
      },
      {
        nomValueId: 7, code: '7', name: 'Друг документ', nameAlt: 'DocumentOther', alias: 'DocumentOther', textContent: {
          setPartId: 7
        }
      },
      {
        nomValueId: 8, code: '8', name: 'Заявление', nameAlt: 'DocumentApplication', alias: 'DocumentApplication', textContent: {
          setPartId: 8
        }
      }
    ],

    graduations: require('./graduation'),

    documentRoles: require('./documentRole'),

    documentTypes: require('./documentType'),

    employmentCategories: require('./employmentCategory'),

    personStatusTypes: require('./personStatusType'),

    medDocPublishers: require('./medDocPublisher'),

    //Номенклатура Издатели на документи - Други
    OtherDocPublishers: [
      { nomValueId: 1, code: '', name: 'ЛИЧНО', nameAlt: '' },
      { nomValueId: 301, code: '', name: 'МВР', nameAlt: '', alias: 'Mvr' }
    ],

    ratingTypes: require('./ratingType'),

    ratingClassGroups: require('./ratingClassGroup'),

    ratingClasses: require('./ratingClass'),

    //Номенклатура Подкласове ВС за екипажи
    ratingSubClasses: [
      { nomValueId: 1, code: 'A1', name: 'Подклас А1', nameAlt: 'Подклас А1', alias: 'A1' },
      { nomValueId: 2, code: 'A2', name: 'Подклас А2', nameAlt: 'Подклас А2', alias: 'A2' },
      { nomValueId: 3, code: 'A3', name: 'Подклас А3', nameAlt: 'Подклас А3', alias: 'A3' },
      { nomValueId: 4, code: 'A4', name: 'Подклас А4', nameAlt: 'Подклас А4', alias: 'A4' }
    ],

    //Номенклатура Модел на квалификация на Физическо лице
    personRatingModels: [
      { nomValueId: 1, code: 'permanent', name: 'Постоянно', nameAlt: 'Постоянно', alias: 'permanent' },
      { nomValueId: 2, code: 'temporary', name: 'Временно', nameAlt: 'Временно', alias: 'temporary' }
    ],

    authorizationGroups: require('./authorizationGroup'),

    authorizations: require('./authorization'),

    licenceTypes: require('./licenceType'),

    //Номенклатура Нива на владеене на английски език
    engLangLevels: [
      { nomValueId: 1, code: 'L4', name: 'Работно (Ниво 4)', nameAlt: 'Operational (Level  4)', alias: 'L4' },
      { nomValueId: 2, code: 'L5', name: 'Разширено (Ниво  5)', nameAlt: 'Extended (Level  5)', alias: 'L5' },
      { nomValueId: 3, code: 'L6', name: 'Експерт (Ниво  6)', nameAlt: 'Expert (Level  6)', alias: 'L6' }
    ],

    locationIndicators: require('./locationIndicator'),

    //Номенклатура Държатели на ТС за ВС
    aircraftTCHolders: [
      { nomValueId: 1, code: '', name: 'Еърбъс', nameAlt: 'Airbus' },
      { nomValueId: 3, code: '', name: 'Чесна Еъркрафт Къмпани', nameAlt: 'Cessna Aircraft Company' },
      { nomValueId: 23, code: '', name: 'Saab AB, Saab Aerosystems', nameAlt: 'Saab AB, Saab Aerosystems' },
      { nomValueId: 24, code: '', name: 'Avions de Transport Regional (ATR)', nameAlt: 'Avions de Transport Regional (ATR)' },
      { nomValueId: 25, code: '', name: 'Бритиш Аероспейс Системс (БАе Системс)', nameAlt: 'British Aerospace Systems (BAe Systems)' },
      { nomValueId: 26, code: '', name: 'Construcciones Aeronauticas, S.A.', nameAlt: 'Construcciones Aeronauticas, S.A.' },
      { nomValueId: 27, code: '', name: 'Бомбардиер', nameAlt: 'Bombardier Inc.' }
    ],

    aircraftTypes: require('./aircraftType'),

    aircraftTypeGroups: require('./aircraftTypeGroup'),

    docFormatType: require('./docFormatType'),

    docCasePartType: require('./docCasePartType'),

    docDirection: require('./docDirection'),

    docTypeGroup: require('./docTypeGroup'),

    docType: require('./docType'),

    testCorrespondent: require('./testCorrespondent'),

    electronicServiceStage: require('./electronicServiceStage'),

    docWorkflowAction: require('./docWorkflowAction'),

    medClasses: require('./medicalClass'),

    medLimitation: require('./medicalLimitation'),

    publisherTypes: require('./publisherType'),

    //оценки при проверка на Физическо лице
    personCheckRatingValues: [
       { nomValueId: 1, code: 'Goog', name: 'Добро', nameAlt: 'good', alias: 'good' },
       { nomValueId: 2, code: 'Sat', name: 'Задоволително', nameAlt: 'Задоволително', alias: 'satisfactory' },
       { nomValueId: 3, code: 'Ins', name: 'Недостатъчно', nameAlt: 'Недостатъчно', alias: 'insufficient' },
       { nomValueId: 4, code: 'Unac', name: 'Неприемливо', nameAlt: 'Неприемливо', alias: 'unacceptable' },
       { nomValueId: 4, code: 'Comp', name: 'Компетентен', nameAlt: 'Компетентен', alias: 'competent' },
       { nomValueId: 4, code: 'Incomp', name: 'Некомпетентен', nameAlt: 'Некомпетентен', alias: 'incompetent' },
    ],

    aircrafts: require('./aircraft'),

    experienceRoles: require('./experienceRole'),

    experienceMeasures: require('./experienceMeasure'),

    //Номенклатура Степени на квалификационен клас на Физичеко лице
    personRatingLevels: [
     { nomValueId: 1, code: 'A', name: 'степен А', nameAlt: 'ratingA', alias: 'A' },
     { nomValueId: 2, code: 'B', name: 'степен Б', nameAlt: 'ratingB', alias: 'B' },
     { nomValueId: 3, code: 'C', name: 'степен C', nameAlt: 'ratingC', alias: 'C' }
    ],

    inspectors: [
      { nomValueId: 1, code: '1', name: 'Владимир Бонев Текнеджиев', nameAlt: 'Vladimi Bonev Teknedjiev', alias: 'Vladimir' },
      { nomValueId: 2, code: '2', name: 'Ваня Наумова Георгиева', nameAlt: 'Vanq Naumova Georgieva', alias: 'Vanq' },
      { nomValueId: 3, code: '3', name: 'Георги Мишев Христов', nameAlt: 'Georgi Mishev Hristov', alias: 'Georgi' }
    ],

    //Oграничения за класове
    ratingLimitationTypes: [
      { nomValueId: 1, code: 'MCL', name: 'MCL', nameAlt: 'MCL', alias: 'MCL' },
      { nomValueId: 2, code: 'OCL', name: 'OCL', nameAlt: 'OCL', alias: 'OCL' },
      { nomValueId: 3, code: 'OFL', name: 'OFL', nameAlt: 'OFL', alias: 'OFL' },
      { nomValueId: 4, code: 'OML', name: 'OML', nameAlt: 'OML', alias: 'OML' }
    ],

    //Номенклатура Клас
    ratingCategories: [
      { nomValueId: 1, code: 'A', name: 'A', nameAlt: 'A', alias: 'A' },
      { nomValueId: 2, code: 'A1', name: 'A1', nameAlt: 'A1', alias: 'A1' },
      { nomValueId: 3, code: 'A2', name: 'A2', nameAlt: 'A2', alias: 'A2' },
      { nomValueId: 4, code: 'B1', name: 'B1', nameAlt: 'B1', alias: 'B1' }
    ],

    //Номенклатура Видове заявления
    applicationTypes: require('./applicationType'),

    //Номенклатура Видове плащания по заявления
    applicationPaymentTypes: require('./applicationPaymentType'),

    //Номенклатура Парични единици
    currencies: require('./currency')

  };
})(typeof module === 'undefined' ? (this['nomenclatures.sample'] = {}) : module);
