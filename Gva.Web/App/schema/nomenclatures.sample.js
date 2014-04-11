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
    aircraftCategories: require('./aircraftCategory'),
    aircraftProducers: require('./aircraftProducer'),
    aircraftSCodeTypes: require('./aircraftSCodeType'),
    aircraftCertificateTypes: require('./aircraftCertificateType'),
    aircraftTypeCertificateTypes: require('./aircraftTypeCertificateType'),
    registers: require('./register'),
    operationTypes: require('./operationType'),
    aircraftNewOld: require('./aircraftNewOld'),
    aircraftRadioTypes: require('./aircraftRadioType'),

    cofATypes: require('./CofAType'),
    easaTypes: require('./EASAType'),
    euRegTypes: require('./EURegType'),
    easaCategories: require('./EASACategory'),

    aircraftDebtTypes: require('./aircraftDebtType'),
    aircraftCreditors: require('./aircraftCreditor'),


    aircraftParts: require('./aircraftPart'),
    aircraftPartProducers: require('./aircraftPartProducer'),
    aircraftPartStatuses: require('./aircraftPartStatus'),

    aircraftLimitations: require('./aircraftLimitation'),
    aircraftRegStatuses: require('./aircraftRegStatus'),

    //aircraftRelations: require('./aircraftRelation'),

    aircraftRelations: [
      {
        nomValueId: 1, code: 'R1', name: 'Собственик', nameAlt: null, alias: 'Owner'
      },
      {
        nomValueId: 2, code: 'R2', name: 'Оператор', nameAlt: null, alias: 'Oper'
      },
      {
        nomValueId: 3, code: 'R3', name: 'Наемател', nameAlt: null, alias: 'Loanee'
      },
      {
        nomValueId: 4, code: 'R4', name: 'Лизингодател', nameAlt: null, alias: 'Loaner'
      },
      {
        nomValueId: 5, code: 'R5', name: 'Организация за УЛППГ', nameAlt: null, alias: 'ULPPG'
      },
      {
        nomValueId: 6, code: 'R6', name: 'Организация за ТО', nameAlt: null, alias: 'TO'
      }
    ],

    airportTypes: require('./airportType'),
    
    airportRelations: require('./airportRelation'),

    //Номенклатура DocFormatTypes
    //docFormatTypes: [
    //  {
    //    "docFormatTypeId": 1,
    //    "name": "Електронен",
    //    "alias": "Electronic",
    //    "isActive": true,
    //    "version": "AAAAAAAAIBI=",
    //    "docs": []
    //  },
    //  {
    //    "docFormatTypeId": 2,
    //    "name": "Електронен с хартия",
    //    "alias": "ElectronicWithPaper",
    //    "isActive": false,
    //    "version": "AAAAAAAAIBM=",
    //    "docs": []
    //  },
    //  {
    //    "docFormatTypeId": 3,
    //    "name": "Хартиен",
    //    "alias": "Paper",
    //    "isActive": false,
    //    "version": "AAAAAAAAIBQ=",
    //    "docs": []
    //  }
    //],

    docFormatType: require('./docFormatType'),

    docCasePartType: require('./docCasePartType'),

    docDirection: require('./docDirection'),

    docTypeGroup: require('./docTypeGroup'),

    docType: require('./docType'),

    correspondent: require('./correspondent'),

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
    currencies: require('./currency'),

    //Номенклатура Причини за одит
    auditReasons: require('./auditReason'),

    //Номенклатура Видове одит
    auditTypes: require('./auditType'),

    //Номенклатура Състояния за одит
    auditStates: require('./auditState'),

    //Номенклатура Резултати от одит
    auditResults: require('./auditResult'),

    //Номенклатура Изисквания към раздел
    auditPartRequirmants: require('./auditPartRequirement'),

    //Номенклатура Проверяващи
    examiners: require('./examiner'),

    //Номенклатура Ниво от несъответствие
    disparityLevels: require('./disparityLevel'),

    //Номенклатура Класове инциденти с ВС.
    aircraftOccurrenceClasses: require('./aircraftOccurrenceClass'),

    //Номенклатура Ограничения по част М/Ф и част 145
    lim145limitations: [
      { nomValueId: 1, code: 'A1', name: 'А1 - Самолети над 5700 кг', nameAlt: 'А1 - Самолети над 5700 кг', alias: 'A1' },
      { nomValueId: 2, code: 'A2', name: 'А2 - Самолети 5700 кг и по-малко', nameAlt: 'А2 - Самолети 5700 кг и по-малко', alias: 'A2' },
      { nomValueId: 3, code: 'A3', name: 'А3 - Хеликоптери', nameAlt: 'А3 - Хеликоптери', alias: 'A3' },
      { nomValueId: 4, code: 'B1', name: 'B1 - Турбини', nameAlt: 'B1 - Турбини', alias: 'B1' }
    ],

    //Номенклатура Физически лица
    persons: [
      { nomValueId: 1, code: 'P2', name: 'Петър Лалов', nameAlt: 'Петър Лалов', alias: 'P1' },
      { nomValueId: 2, code: 'P2', name: 'Пламен Илиев', nameAlt: 'Пламен Илиев', alias: 'P2' },
      { nomValueId: 3, code: 'P3', name: 'Пламен Пилев', nameAlt: 'Пламен Пилев', alias: 'P3' },
    ],

    //Номенклатура Класификатор на организации
    organizationTypes: require('./organizationsType'),

    //Номенклатура Видове организации
    organizationKinds: require('./organizationKind'),

    organizationOtherDocumentTypes: require('./otherDocumentType'),

    organizationOtherDocumentRoles: require('./documentRole'),

    audits: [
      { nomValueId: 1, code: 'A1', name: 'Audit1', nameAlt: 'Audit1', alias: 'A1' },
      { nomValueId: 2, code: 'A2', name: 'Audit2', nameAlt: 'Audit2', alias: 'A2' },
      { nomValueId: 3, code: 'A3', name: 'Audit3', nameAlt: 'Audit3', alias: 'A3' },
      { nomValueId: 4, code: 'A4', name: 'Audit4', nameAlt: 'Audit4', alias: 'A4' }
    ],

    // Номенклатура Летище/Площадки
    airports: [
      { nomValueId: 1, code: 'A1', name: 'airport1', nameAlt: 'airport1', alias: 'A1' },
      { nomValueId: 2, code: 'A2', name: 'airport2', nameAlt: 'airport2', alias: 'A2' },
      { nomValueId: 3, code: 'A3', name: 'airport3', nameAlt: 'airport3', alias: 'A3' },
      { nomValueId: 4, code: 'A4', name: 'airport4', nameAlt: 'airport4', alias: 'A4' }
    ],

    //Номенклатура Типове дейности на летищен оператор
    airportoperatorActivityTypes: [
      { nomValueId: 1, code: 'A1', name: 'airportoperatorActivityType1', nameAlt: 'airportoperatorActivityType1', alias: 'A1' },
      { nomValueId: 2, code: 'A2', name: 'airportoperatorActivityType2', nameAlt: 'airportoperatorActivityType2', alias: 'A2' },
      { nomValueId: 3, code: 'A3', name: 'airportoperatorActivityType3', nameAlt: 'airportoperatorActivityType3', alias: 'A3' },
      { nomValueId: 4, code: 'A4', name: 'airportoperatorActivityType4', nameAlt: 'airportoperatorActivityType4', alias: 'A4' }
    ],

    //Номенклатура Типове дейности на оператор по наземно обслужване или самообслужване
    groundServiceOperatorActivityTypes: [
      { nomValueId: 1, code: 'A1', name: 'groundserviceoperatorActivityType1', nameAlt: 'groundserviceoperatorActivityType1', alias: 'A1' },
      { nomValueId: 2, code: 'A2', name: 'groundserviceoperatorActivityType2', nameAlt: 'groundserviceoperatorActivityType2', alias: 'A2' },
      { nomValueId: 3, code: 'A3', name: 'groundserviceoperatorActivityType3', nameAlt: 'groundserviceoperatorActivityType3', alias: 'A3' },
      { nomValueId: 4, code: 'A4', name: 'groundserviceoperatorActivityType4', nameAlt: 'groundserviceoperatorActivityType4', alias: 'A4' }
    ],

    //Номенклатура Раздел
    auditParts: [
      { nomValueId: 50101, code: '1', name: 'Trial', nameAlt: 'Trial', alias: 'Trial' },
      { nomValueId: 50102, code: '2', name: 'BC - ACAM инспекция', nameAlt: 'BC - ACAM инспекция', alias: 'BC - ACAM' },
      { nomValueId: 50103, code: '3', name: 'Част 145', nameAlt: 'Част 145', alias: '145' },
      { nomValueId: 50104, code: '4', name: 'Част 147', nameAlt: 'Част 147', alias: '147' },
      { nomValueId: 50105, code: '5', name: 'Част М подчаст F', nameAlt: 'Част М подчаст F', alias: 'm/f' },
      { nomValueId: 50106, code: '6', name: 'Част М подчаст G', nameAlt: 'Част М подчаст G', alias: 'm/g' }
    ],

    approvalStates: [
      {
        nomValueId: 1, code: '1', name: 'Валидно', parentValueId: null, alias: '1'
      },
      {
        nomValueId: 2, code: '2', name: 'Върнато', parentValueId: null, alias: '2'
      },
      {
        nomValueId: 3, code: '3', name: 'Ограничено', parentValueId: null, alias: null
      },
      {
        nomValueId: 4, code: '4', name: 'Прекратено', parentValueId: null, alias: '3'
      },
      {
        nomValueId: 5, code: '5', name: 'Анулирано', parentValueId: null, alias: null
      },
      {
        nomValueId: 6, code: '6', name: 'Временно спрени всички права по одобрението', parentValueId: null, alias: null
      }
    ],

    lim147limitations: [
      { nomValueId: 1, code: 'A1', name: 'Самолети с турбинни двигатели', alias: 'A1' },
      { nomValueId: 2, code: 'A2', name: 'Самолети с бутални двигатели', alias: 'A2' },
      { nomValueId: 3, code: 'A3', name: 'Вертолети с турбинни двигатели', alias: 'A3' },
      { nomValueId: 4, code: 'B1', name: 'Вертолети с бутални двигатели', alias: 'B1' }
    ],

    recommendationPartNumbers: [
      { nomValueId: 1, code: '1', name: '1', alias: '1' },
      { nomValueId: 2, code: '2', name: '2', alias: '2' },
      { nomValueId: 3, code: '3', name: '3', alias: '3' },
      { nomValueId: 4, code: '4', name: '4', alias: '4' }
    ],
    
    //Номенклатура Ограничения (Part-66)
    limitation66Types: require('./limitation66Type'),

    //Номенклатура Видове действия относно правоспособност
    licenceActions: require('./licenceAction')
  };
})(typeof module === 'undefined' ? (this['nomenclatures.sample'] = {}) : module);
