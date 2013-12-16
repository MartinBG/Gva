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

    //Номенклатура Кореспондентска група
    CorrespondentGroups: [
      {nomTypeValueId: 1, code: '', name: 'Министерски съвет', nameAlt: '', alias: '' },
      {nomTypeValueId: 2, code: '', name: 'Заявители', nameAlt: 'Applicants', alias: 'Applicants' },
      {nomTypeValueId: 3, code: '', name: 'Системни', nameAlt: 'System', alias: 'System' }
    ],

    //Номенклатура Тип кореспондент
    CorrespondentTypes: [
     { nomTypeValueId: 1, code: '', name: 'Български гражданин', nameAlt: 'BulgarianCitizen', alias: 'BulgarianCitizen' },
     { nomTypeValueId: 2, code: '', name: 'Чужденец', nameAlt: 'Foreigner', alias: 'Foreigner' },
     { nomTypeValueId: 3, code: '', name: 'Юридическо лице', nameAlt: 'LegalEntity', alias: 'LegalEntity' },
     { nomTypeValueId: 4, code: '', name: 'Чуждестранно юридическо лице', nameAlt: 'ForeignLegalEntity', alias: 'ForeignLegalEntity' }
    ],

    //Номенклатура Булеви стойности
    boolean: [
      {nomTypeValueId: 1, code: 'Y', name: 'Да', nameAlt: 'Yes', alias: 'true'},
      {nomTypeValueId: 2, code: 'N', name: 'Не', nameAlt: 'No', alias: 'false'}
    ],

    //Номенклатура Полове
    sex: [
      {nomTypeValueId: 1, code: '', name: 'Мъж', nameAlt: 'Male', alias: 'male'},
      {nomTypeValueId: 2, code: '', name: 'Жена', nameAlt: 'Female', alias: 'female'},
      {nomTypeValueId: 3, code: '', name: 'Неопределен', nameAlt: 'Unknown', alias: 'unknown'}
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
    organizations: [
      {nomTypeValueId: 203, code: '203', name: 'AAK Progres', nameAlt: 'AAK Progres', alias: 'AAK Progres'}
    ],

    //Номенклатура Типове персонал
    staffTypes: [
      {nomTypeValueId: 5, code: 'M', name: 'Наземен авиационен персонал за TO на СУВД', nameAlt: 'Наземен авиационен персонал за TO на СУВД'},
      {nomTypeValueId: 4, code: 'G', name: 'Наземен авиационен персонал за TO на ВС', nameAlt: 'Наземен авиационен персонал за TO'},
      {nomTypeValueId: 1, code: 'F', name: 'Членове на екипажа', nameAlt: 'Членове на екипажа', alias: 'Crew'},
      {nomTypeValueId: 2, code: 'T', name: 'Наземен авиационен персонал за ОВД', nameAlt: 'Наземен авиационен персонал за ОВД'}
    ],

    //Номенклатура Категории персонал
    employmentCategories: [
      {nomTypeValueId: 631, code: '1', name: 'Директор', nameAlt: 'Director', content: {
        Code_CA: '',
        StaffTypeId: null
      }},
      {nomTypeValueId: 650, code: '11', name: 'Втори пилот', nameAlt: 'First officer', alias: 'First officer', content: {
        Code_CA: '11',
        StaffTypeId: 1
      }}
    ],

    //Номенклатура Учебни заведения
    schools: [
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
    graduations: [
      {nomTypeValueId: 1, code: 'HS', name: 'Висше образование (бакалавър)', nameAlt: 'Висше образование (бакалавър)', alias: 'HS'},
      {nomTypeValueId: 3, code: 'HM', name: 'Висше образование (магистър)', nameAlt: 'Висше образование (магистър)', alias: 'HM'},
      {nomTypeValueId: 450, code: 'PQ', name: 'Професионална квалификация', nameAlt: 'Професионална квалификация', alias: 'PQ'}
    ],

    //Номенклатура Типове документи за самоличност на Физичеко лице
    personIdDocumentTypes: [
      {nomTypeValueId: 3, code: '3', name: 'Лична карта', nameAlt: 'Лична карта', alias: 'Id'},
      {nomTypeValueId: 4, code: '4', name: 'Задграничен паспорт', nameAlt: 'Задграничен паспорт'},
      {nomTypeValueId: 5, code: '5', name: 'Паспорт', nameAlt: 'Паспорт'}
    ],

    //Номенклатура Други типове документи на Физичеко лице
    personOtherDocumentTypes: [
      {nomTypeValueId: 1, code: '1', name: 'Удостоверение', nameAlt: 'Удостоверение'},
      {nomTypeValueId: 2, code: '2', name: 'Протокол', nameAlt: 'Протокол', alias: 'Protocol'},
      {nomTypeValueId: 3, code: '3', name: 'Лична карта', nameAlt: 'Лична карта'},
      {nomTypeValueId: 4, code: '4', name: 'Задграничен паспорт', nameAlt: 'Задграничен паспорт'},
      {nomTypeValueId: 5, code: '5', name: 'Паспорт', nameAlt: 'Паспорт'},
      {nomTypeValueId: 643, code: '7', name: 'Свидетелство', nameAlt: 'Certificate', alias: 'Certificate'},
      {nomTypeValueId: 662, code: '14', name: 'Контролна карта', nameAlt: 'Контролна карта', alias: 'CtrlCard'},
      {nomTypeValueId: 663, code: '15', name: 'Контролен талон', nameAlt: 'Контролен талон', alias: 'CtrlTalon'},
      {nomTypeValueId: 683, code: '20', name: 'Писмо', nameAlt: 'Писмо', alias: 'Letter'},
      {nomTypeValueId: 702, code: '22', name: 'Справка', nameAlt: 'Справка', alias: 'Report'}
    ],

    //Номенклатура Други роли на документи на Физичеко лице
    personOtherDocumentRoles: [
      {nomTypeValueId: 1, code: '1', name: 'Летателна проверка', nameAlt: '', alias: 'FlightTest'},
      {nomTypeValueId: 2, code: '2', name: 'Документ за самоличност', nameAlt: ''},
      {nomTypeValueId: 3, code: '3', name: 'Диплома за завършено образование', nameAlt: ''},
      {nomTypeValueId: 4, code: '4', name: 'Теоретично обучение', nameAlt: '', alias: 'TheoreticalTraining'},
      {nomTypeValueId: 5, code: '5', name: 'Летателно обучение', nameAlt: ''},
      {nomTypeValueId: 6, code: '6', name: 'Теоретичен изпит', nameAlt: ''},
      {nomTypeValueId: 7, code: '7', name: 'Тренажор', nameAlt: ''},
      {nomTypeValueId: 731, code: '08', name: 'Образование', nameAlt: ''},
      {nomTypeValueId: 732, code: '9', name: 'Летателно обучение - копие от всичките записи', nameAlt: ''},
      {nomTypeValueId: 733, code: '10', name: 'Летателна книжка', nameAlt: ''},
      {nomTypeValueId: 759, code: '17', name: 'Контролна карта', nameAlt: 'Контролна карта', alias: 'CtrlCard'},
      {nomTypeValueId: 760, code: '18', name: 'Контролен талон', nameAlt: 'Контролен талон', alias: 'CtrlTalon'},
      {nomTypeValueId: 813, code: '31', name: 'Обучение', nameAlt: 'Обучение', alias: 'Training'}
    ],

    //Номенклатура Типове състояния на Физичеко лице
    personStatusTypes: [
      {nomTypeValueId: 1, code: '3', name: 'Негоден', nameAlt: 'Негоден', alias: 'Disabled'},
      {nomTypeValueId: 2, code: '4', name: 'Временно негоден', nameAlt: 'Временно негоден'},
      {nomTypeValueId: 3, code: '5', name: 'Майчинство', nameAlt: 'Майчинство'}
    ],


    //Номенклатура Издатели на документи - Други
    OtherDocPublishers: [
      {nomTypeValueId: 1, code: '', name: 'ЛИЧНО', nameAlt: ''},
      {nomTypeValueId: 301, code: '', name: 'МВР', nameAlt: '', alias: 'Mvr'}
    ],

    //Номенклатура Типове ВС за екипажи
    ratingTypes: [
      {nomTypeValueId: 1, code: 'MD80', name: 'McDonnell Douglas MD80', nameAlt: 'McDonnell Douglas MD80'},
      {nomTypeValueId: 2, code: 'B737', name: 'Boeing 737', nameAlt: 'B737'},
      {nomTypeValueId: 3, code: 'L410', name: 'Let L-410 Turbolet', nameAlt: 'Let L-410 Turbolet'},
      {nomTypeValueId: 831, code: 'IR(MEA)', name: 'Полети по прибори', nameAlt: 'Instrument rating (MEA)', content: { Code_CA: 'IR(MEA)'}},
      {nomTypeValueId: 850, code: 'Тu 154', name: 'Tу 154', nameAlt: 'Tu 154', content: { Code_CA: 'Тu 154'}},
      {nomTypeValueId: 851, code: 'BAe 146', name: 'BAe 146', nameAlt: 'BAe 146', content: { Code_CA: 'BAe 146'}}
    ],

    //Номенклатура Групи Класове ВС за екипажи
    ratingClassGroups: [
      {nomTypeValueId: 1, code: 'F', name: 'Екипажи', nameAlt: '', content: { StaffTypeId:1}},
      {nomTypeValueId: 8, code: 'M', name: 'ТО на СУВД', nameAlt: '', content: { StaffTypeId:5}},
      {nomTypeValueId: 9, code: 'T', name: 'ОВД', nameAlt: '', content: { StaffTypeId:2}},
      {nomTypeValueId: 30, code: 'G', name: 'ТО на ВС', nameAlt: '', content: { StaffTypeId:4}}
    ],

    //Номенклатура Класове ВС за екипажи
    ratingClasses: [
      {nomTypeValueId: 1, code: 'VLA', name: 'Много леки самолети', nameAlt: 'Много леки самолети', content: { GroupId: 1}},
      {nomTypeValueId: 2, code: 'ULA', name: 'Свръхлеки самолети', nameAlt: 'Свръхлеки самолети', content: { GroupId: 1}},
      {nomTypeValueId: 5, code: 'C', name: 'Радиолокационен обзор', nameAlt: 'Surveillance', content: { GroupId: 8, Code_CA: 'C'}},
      {nomTypeValueId: 6, code: 'D', name: 'Обработка на данни', nameAlt: 'Data processing', content: { GroupId: 8, Code_CA: 'D'}},
      {nomTypeValueId: 7, code: 'E', name: 'Аеронавигационно метеорологично оборудване', nameAlt: 'Met', content: { GroupId: 8, Code_CA: 'E'}},
      {nomTypeValueId: 8, code: 'F', name: 'Светотехнически средства', nameAlt: 'Agl', content: { GroupId: 8, Code_CA: 'F'}},
      {nomTypeValueId: 9, code: 'A', name: 'Комуникация', nameAlt: 'Communications', content: { GroupId: 8, Code_CA: 'A'}},
      {nomTypeValueId: 100, code: 'APP', name: 'Процедурно ОВД в летищния контролиран район', nameAlt: 'Aerodrome Control Procedural', content: { GroupId: 9, Code_CA: 'APP'}},
      {nomTypeValueId: 101, code: 'APS', name: 'ОВД в летищния контролиран районн чрез средства за обзор', nameAlt: 'Approach Control Surveillance', content: { GroupId: 9, Code_CA: 'APS'}},
      {nomTypeValueId: 102, code: 'ACP', name: 'Процедурно ОВД в контролирания район', nameAlt: 'Area Control Procedural', content: { GroupId: 9, Code_CA: 'ACP'}}
    ],

    //Номенклатура Групи Разрешения към квалификация 
    authorizationGroups: [
      {nomTypeValueId: 1, code: 'FT', name: 'За провеждане обучение', content: { StaffTypeId:1}},
      {nomTypeValueId: 100, code: 'T', name: 'За ОВД', content: { RatingClassGroupId:9, StaffTypeId:2}},
      {nomTypeValueId: 200, code: 'G', name: 'За ТО (AML)', content: { RatingClassGroupId:30, StaffTypeId:4}},
      {nomTypeValueId: 421, code: 'F', name: 'За екипаж на ВС', content: {StaffTypeId:1}},
      {nomTypeValueId: 441, code: 'FC', name: 'Проверяващи', content: {StaffTypeId:1}},
      {nomTypeValueId: 461, code: 'M', name: 'За ТО (СУВД)', content: { RatingClassGroupId:8, StaffTypeId:5}}
    ],

    //Номенклатура Разрешения към квалификация 
    authorizations: [
      {nomTypeValueId: 1, code: 'FI(A)', name: 'Летателен инструктор на самолет', nameAlt: 'Летателен инструктор на самолет', content: { GroupId: 1}},
      {nomTypeValueId: 10, code: 'CAT II', name: 'CAT II (cop)', nameAlt: 'CAT II (cop)', content: { GroupId: 421, Code_CA: 'CAT II'}},
      {nomTypeValueId: 12, code: 'CAT IIIA', name: 'CAT III A (cop)', nameAlt: 'CAT IIIA (cop)', content: { GroupId: 421, Code_CA: 'CAT IIIA'}},
      {nomTypeValueId: 14, code: 'CAT IIIB', name: 'CAT IIIB (cop)', nameAlt: 'CAT IIIB (cop)', content: { GroupId: 421, Code_CA: 'CAT IIIB'}},
      {nomTypeValueId: 16, code: 'LV-TO', name: 'LV-TO (cop)', nameAlt: 'LV-TO (cop)', content: { GroupId: 421, Code_CA: 'LV-TO'}},
      {nomTypeValueId: 100, code: 'GMS', name: 'КВД по маневрената площ на летището чрез средства за обзор', nameAlt: 'Ground Movement Surveillance ', content: { GroupId: 100, Code_CA: 'GMS'}},
      {nomTypeValueId: 101, code: 'RAD', name: 'КВД чрез радар', nameAlt: 'Radar', content: { GroupId: 100, Code_CA: 'RAD'}},
      {nomTypeValueId: 102, code: 'ADS', name: 'КВД чрез автоматичен зависим обзор', nameAlt: 'Automatic Dependent Surveillance', content: { GroupId: 100, Code_CA: 'ADS'}},
      {nomTypeValueId: 103, code: 'PAR', name: 'КВД чрез прецизен радар за подход', nameAlt: 'Precision Approach Radar', content: { GroupId: 100, Code_CA: 'PAR'}},
      {nomTypeValueId: 104, code: 'SRA', name: 'КВД чрез обзорен радар за подход', nameAlt: 'Surveillance Radar Approach ', content: { GroupId: 100, Code_CA: 'SRA'}},
      {nomTypeValueId: 200, code: 'ASM', name: 'Планиране и разпределение на въздушното пространство', nameAlt: 'Air Space management', content: { GroupId: 100, Code_CA: 'ASM'}},
      {nomTypeValueId: 201, code: 'ATFM', name: 'Организация на потоците въздушно движение', nameAlt: 'Air Traffic Flow Management', content: { GroupId: 100, Code_CA: 'ATFM'}},
      {nomTypeValueId: 203, code: 'FIS', name: 'Полетно информационно обслужване на полетите', nameAlt: 'Flight Information Service', content: { GroupId: 100, Code_CA: 'FIS'}},
      {nomTypeValueId: 205, code: 'SAR', name: 'Търсене и спасяване', nameAlt: 'Search and Rescue', content: { GroupId: 100, Code_CA: 'SAR'}},
      {nomTypeValueId: 206, code: 'AFIS', name: 'Летищно полетно-информационно обслужване', nameAlt: 'Aerodrome Flight Information Service ', content: { GroupId: 100, Code_CA: 'AFIS'}}
    ],

    //Номенклатура Видове(типове) правоспособност 
    licenceTypes: [
      {nomTypeValueId: 1, code: 'PPL(A)', name: 'Любител пилот на самолет (PPL(A))', nameAlt: 'Private Pilot  (Aeroplane) (PPL(A))', content: { StaffTypeId:1, SeqNo:1, DictionaryId:1, Code_CA: 'PPL(A)', PRT_MAX_RATING_COUNT:11, _CODE:'PPL(A)'}},
      {nomTypeValueId: 2, code: 'CPL(A)', name: 'Професионален пилот на самолет CPL(A)', nameAlt: 'Commercial Pilot  (Aeroplane) (CPL(A))', content: { StaffTypeId:1, SeqNo:2, DictionaryId:1, Code_CA: 'CPL(A)', PRT_MAX_RATING_COUNT:14, _CODE:'CPL(A)'}},
      {nomTypeValueId: 3, code: 'ATPL(A)', name: 'Транспортен пилот на самолет ATPL(A)', nameAlt: 'Airline transport Pilot  (Aeroplane) (ATPL(A))', content: { StaffTypeId:1, SeqNo:3, DictionaryId:1, Code_CA: 'ATPL(A)', PRT_MAX_RATING_COUNT:11, _CODE:'ATPL(A)'}},
      {nomTypeValueId: 4, code: 'PPL(H)', name: 'Любител пилот на вертолет (PPL(H))', nameAlt: 'Private Pilot  (Helicopter) (PPL(H))', content: { StaffTypeId:1, SeqNo:4, DictionaryId:1, Code_CA: 'PPL(H)', PRT_MAX_RATING_COUNT:11, _CODE:'PPL(H)'}},
      {nomTypeValueId: 5, code: 'CPL(H)', name: 'Професионален пилот на вертолет (CPL(H))', nameAlt: 'Commercial Pilot  (CPL(H))', content: { StaffTypeId:1, SeqNo:5, DictionaryId:1, Code_CA: 'CPL(H)', PRT_MAX_RATING_COUNT:11, _CODE:'CPL(H)'}},
      {nomTypeValueId: 6, code: 'ATPL(H)', name: 'Транспортен пилот на вертолет (ATPL(H))', nameAlt: 'Airline Transport Pilot  (Helocopter) (ATPL(H))', content: { StaffTypeId:1, SeqNo:6, DictionaryId:1, Code_CA: 'ATPL(H)', PRT_MAX_RATING_COUNT:11, _CODE:'ATPL(H)'}},
      {nomTypeValueId: 7, code: 'PL(G)', name: 'Пилот на планер (PL(G))', nameAlt: 'Pilot  (Glider)  (PL(G))', content: { StaffTypeId:1, SeqNo:7, DictionaryId:1, Code_CA: 'PL(G)', PRT_MAX_RATING_COUNT:11, _CODE:'PLG'}},
      {nomTypeValueId: 8, code: 'PL(FB)', name: 'Пилот на свободен балон (PL(FB))', nameAlt: 'Pilot  (Free baloons) (PL(FB))', content: { StaffTypeId:1, SeqNo:8, DictionaryId:1, Code_CA: 'PL(FB)', PRT_MAX_RATING_COUNT:11, _CODE:'PFB'}},
      {nomTypeValueId: 9, code: 'PPL(SA)', name: 'Любител пилот на малки въздухоплавателни средства PPL(SA)', nameAlt: 'Private Pilot  (Small Aircraft) (PPL(SA))', content: { StaffTypeId:1, SeqNo:9, DictionaryId:1, Code_CA: 'PPL(SA)', PRT_MAX_RATING_COUNT:11, _CODE:'PPSA'}},
      {nomTypeValueId: 10, code: 'C/AL', name: 'Стюард(-еса) (CAL)', nameAlt: 'Cabin Attendante l (CAL)', content: { StaffTypeId:1, SeqNo:10, DictionaryId:2, Code_CA: 'C/AL', PRT_MAX_RATING_COUNT:11, _CODE:'CA'}},
      {nomTypeValueId: 1092, code: 'FOREIGN', name: 'Признаване на лиценз, издаден от чужда страна', nameAlt: 'Признаване на лиценз, издаден от чужда страна', content: { StaffTypeId:1, SeqNo:11, Code_CA: 'FOREIGN', PRT_MAX_RATING_COUNT:11, _CODE:'FOREIGN'}},
      {nomTypeValueId: 1271, code: 'SP(A)', name: 'Обучаем пилот на самолет (SP(A))', nameAlt: 'Student pilot (Aeroplane) (SP(A))', content: { StaffTypeId:1, SeqNo:12, DictionaryId:5, Code_CA: 'SPA', PRT_MAX_RATING_COUNT:11, _CODE:'SPA'}},
      {nomTypeValueId: 1290, code: 'FDA', name: 'Асистент - координатор на полети', nameAlt: 'Flight data assistant', content: { StaffTypeId:2, SeqNo:13, DictionaryId:7, Code_CA: 'FDAL', PRT_MAX_RATING_COUNT:11, _CODE:'FDAL'}},
      {nomTypeValueId: 1411, code: 'CPA', name: 'Професионален пилот-CPA', nameAlt: 'Commercial pilot licence-CPA', content: { StaffTypeId:1, SeqNo:21, DictionaryId:1, Code_CA: 'CPL(A)', PRT_MAX_RATING_COUNT:25, _CODE:'CPA'}, alias: 'CPA'}
    ],

    //Номенклатура Нива на владеене на английски език 
    engLangLevels: [
      {nomTypeValueId: 1, code: 'L4', name: 'Работно (Ниво 4)', nameAlt: 'Operational (Level  4)'},
      {nomTypeValueId: 2, code: 'L5', name: 'Разширено (Ниво  5)', nameAlt: 'Extended (Level  5)'},
      {nomTypeValueId: 3, code: 'L6', name: 'Експерт (Ниво  6)', nameAlt: 'Expert (Level  6)'}
    ],

    //Номенклатура Индикатори на местоположение
    locationIndicators: [
      {nomTypeValueId: 1, code: 'LBBG', name: 'БУРГАС', nameAlt: 'BURGAS'},
      {nomTypeValueId: 2, code: 'LBBO', name: 'БОХОТ-LM ', nameAlt: 'BOHOT-LM'},
      {nomTypeValueId: 3, code: 'LBDB', name: 'ДОЛНА БАНЯ', nameAlt: 'DOLNA BANYA'},
      {nomTypeValueId: 4, code: 'LBGO', name: 'ГОРНА ОРХЯХОВИЦА', nameAlt: 'GORNA ORYAHOVITSA'},
      {nomTypeValueId: 5, code: 'LBGR', name: 'ГРИВИЦА', nameAlt: 'GRIVITSA'},
      {nomTypeValueId: 6, code: 'LBHT', name: 'ИХТИМАН', nameAlt: 'IHTIMAN'}
    ],

    //Номенклатура Държатели на ТС за ВС 
    aircraftTCHolders: [
      {nomTypeValueId: 1, code: '', name: 'Еърбъс', nameAlt: 'Airbus'},
      {nomTypeValueId: 3, code: '', name: 'Чесна Еъркрафт Къмпани', nameAlt: 'Cessna Aircraft Company'},
      {nomTypeValueId: 23, code: '', name: 'Saab AB, Saab Aerosystems', nameAlt: 'Saab AB, Saab Aerosystems'},
      {nomTypeValueId: 24, code: '', name: 'Avions de Transport Regional (ATR)', nameAlt: 'Avions de Transport Regional (ATR)'},
      {nomTypeValueId: 25, code: '', name: 'Бритиш Аероспейс Системс (БАе Системс)', nameAlt: 'British Aerospace Systems (BAe Systems)'},
      {nomTypeValueId: 26, code: '', name: 'Construcciones Aeronauticas, S.A.', nameAlt: 'Construcciones Aeronauticas, S.A.'},
      {nomTypeValueId: 27, code: '', name: 'Бомбардиер', nameAlt: 'Bombardier Inc.'}
    ],

    //Номенклатура Типове ВС
    aircraftTypes: [
      {nomTypeValueId: 1, code: 'LA', name: 'Големи ВС', nameAlt: 'Large aircraft', content: { description: 'Aeroplanes with a maximum take-off mass of more than 5700 kg, requiring type training and individual type rating.'}},
      {nomTypeValueId: 2, code: 'A-tr', name: 'ВС до 5700кг и по малко', nameAlt: 'Aeroplanes of 5700kg and below', content: { description: 'Requiring type training and individual type rating.'}},
      {nomTypeValueId: 3, code: 'AMTE', name: 'Самолети multiple turbine engines of 5700kg and below', nameAlt: 'Aeroplanes multiple turbine engines of 5700kg and below', content: { description: 'Eligible for type examinations and manufacturer group ratings.'}},
      {nomTypeValueId: 4, code: 'ASTE', name: 'Самолети single turbine engine of 5700kg and below', nameAlt: 'Aeroplanes single turbine engine of 5700kg and below', content: { description: 'Eligible for type examinations and group ratings.'}},
      {nomTypeValueId: 5, code: 'AMPE-MS', name: 'Самолети multiple piston engines – metal structure (AMPE-MS)', nameAlt: 'Aeroplane multiple piston engines – metal structure (AMPE-MS)', content: { description: 'Eligible for type examinations and group ratings.'}},
      {nomTypeValueId: 6, code: 'ASPE-MS', name: 'Самолети single piston engine – metal structure (ASPE-MS)', nameAlt: 'Aeroplane single piston engine – metal structure (ASPE-MS)', content: { description: 'Eligible for type examinations and group ratings.'}},
      {nomTypeValueId: 7, code: 'AMPE-WS', name: 'Самолети multiple piston engines – wooden structure', nameAlt: 'Aeroplane multiple piston engines – wooden structure', content: { description: 'Eligible for type examinations and group ratings.'}},
      {nomTypeValueId: 8, code: 'ASPE-WS', name: 'Самолети single piston engine – wooden structure.', nameAlt: 'Aeroplane single piston engine – wooden structure.', content: { description: 'Eligible for type examinations and group ratings'}},
      {nomTypeValueId: 9, code: 'AMPE-CS', name: 'Самолети multiple piston engines – composite structure ', nameAlt: 'Aeroplane multiple piston engines – composite structure ', content: { description: 'Eeligible for type examinations and group ratings.'}},
      {nomTypeValueId: 10, code: 'ASPE-CS', name: 'Самолети single piston engine – composite structure', nameAlt: 'Aeroplane single piston engine – composite structure', content: { description: 'Eligible for type examinations and group ratings.'}},
      {nomTypeValueId: 11, code: 'MEH', name: 'Multi-engine хеликоптери ', nameAlt: 'Multi-engine helicopters ', content: { description: 'Requiring type training and individual type rating.'}}
    ],

    //Номенклатура Групи ВС
    aircraftTypeGroups: [
      {nomTypeValueId: -100, code: '', name: 'Без ТС', nameAlt: 'No type', content: { aircraftTypeId: 1, aircraftTCHolderId: 163}},
      {nomTypeValueId: 600, code: '', name: '(Gates) Learjet 60 (PWC305)', nameAlt: '(Gates) Learjet 60 (PWC305)', content: { aircraftTypeId: 1, aircraftTCHolderId: 43}},
      {nomTypeValueId: 601, code: '', name: '(Hawker Beechcraft) Beech 200 Series (PWC PT6)', nameAlt: '(Hawker Beechcraft) Beech 200 Series (PWC PT6)', content: { aircraftTypeId: 1, aircraftTCHolderId: 36}},
      {nomTypeValueId: 602, code: '', name: 'Airbus A318/A319/A320/A321 (CFM56)', nameAlt: 'Airbus A318/A319/A320/A321 (CFM56)', content: { aircraftTypeId: 1, aircraftTCHolderId: 1}},
      {nomTypeValueId: 603, code: '', name: 'Airbus A319/A320/A321 (IAE V2500)', nameAlt: 'Airbus A319/A320/A321 (IAE V2500)', content: { aircraftTypeId: 1, aircraftTCHolderId: 1}},
      {nomTypeValueId: 604, code: '', name: 'Airbus A319/A320/A321 (PW JT8D)', nameAlt: 'Airbus A319/A320/A321 (PW JT8D)', content: { aircraftTypeId: 1, aircraftTCHolderId: 1}}
    ]
  };
})(typeof module === 'undefined' ? (this['nomenclatures.sample'] = {}) : module);