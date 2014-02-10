/*global module*/
(function (module) {
  'use strict';

  //Номенклатура Роли документи
  module.exports = [
    {
      nomTypeValueId: 6321, code: '76', name: 'Нарушение', nameAlt: 'Нарушение', alias: null,
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'A'
      }
    },
    {
      nomTypeValueId: 6326, code: '81', name: 'Удостоверение за актуално състояние', nameAlt: 'Certificate of good standing ', alias: null,
      content: {
        direction: 6175,
        isPersonsOnly: 'N',
        categoryCode: 'A'
      }
    },
    {
      nomTypeValueId: 6327, code: '83', name: 'Удостоверение, че няма образувана процедура по обявяване в несъстоятелност', nameAlt: 'No-bankruptcy procedure initiated Certificate', alias: null,
      content: {
        direction: 6175,
        isPersonsOnly: 'N',
        categoryCode: 'A'
      }
    },
    {
      nomTypeValueId: 6328, code: '82', name: 'Съдебна регистрация', nameAlt: 'Legal registration', alias: null,
      content: {
        direction: 6175,
        isPersonsOnly: 'N',
        categoryCode: 'A'
      }
    },
    {
      nomTypeValueId: 6329, code: '84', name: 'Списък на персонала', nameAlt: 'Personnel List', alias: null,
      content: {
        direction: 6175,
        isPersonsOnly: 'N',
        categoryCode: 'A'
      }
    },
    {
      nomTypeValueId: 6330, code: '85', name: 'Декларация за съответствие с изискванията на Част М', nameAlt: 'Part M Compliance Declaration', alias: null,
      content: {
        direction: 6175,
        isPersonsOnly: 'N',
        categoryCode: 'A'
      }
    },
    {
      nomTypeValueId: 6357, code: 'ENG', name: 'Свидетелство за ниво на владеене на английски език', nameAlt: 'Свидетелство за ниво на владеене на английски език', alias: 'engTraining',
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'O'
      }
    },
    {
      nomTypeValueId: 6361, code: '49A', name: 'Проверка на работното място', nameAlt: 'Проверка на работното място', alias: null,
      content: {
        direction: 6177,
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
    {
      nomTypeValueId: 6373, code: '15', name: 'Практическа проверка', nameAlt: 'Практическа проверка', alias: null,
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
    {
      nomTypeValueId: 6377, code: '17', name: 'Контролна карта', nameAlt: 'Контролна карта', alias: 'CtrlCard',
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'A'
      }
    },
    {
      nomTypeValueId: 6378, code: '18', name: 'Контролен талон', nameAlt: 'Контролен талон', alias: 'CtrlTalon',
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'A'
      }
    },
    {
      nomTypeValueId: 6388, code: '50', name: 'Проверяващ', nameAlt: 'Проверяващ', alias: null,
      content: {
        isPersonsOnly: 'Y',
        categoryCode: 'F'
      }
    },
    {
      nomTypeValueId: 6389, code: '51', name: 'Спиране', nameAlt: 'Спиране', alias: null,
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'F'
      }
    },
    {
      nomTypeValueId: 6404, code: '31', name: 'Обучение', nameAlt: 'Обучение', alias: 'Training',
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'O'
      }
    },
    {
      nomTypeValueId: 6407, code: '1', name: 'Летателна проверка', nameAlt: 'Летателна проверка', alias: 'FlightTest',
      content: {
        direction: 6173,
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
    {
      nomTypeValueId: 6408, code: '2', name: 'Документ за самоличност', nameAlt: 'Документ за самоличност', alias: 'documentId',
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'P'
      }
    },
    {
      nomTypeValueId: 6409, code: '3', name: 'Диплома за завършено образование', nameAlt: 'Диплома за завършено образование', alias: null,
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'O'
      }
    },
    {
      nomTypeValueId: 6410, code: '4', name: 'Теоретично обучение', nameAlt: 'Теоретично обучение', alias: 'TheoreticalTraining',
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'O'
      }
    },
    {
      nomTypeValueId: 6411, code: '5', name: 'Летателно обучение', nameAlt: 'Летателно обучение', alias: 'FlightTest',
      content: {
        direction: 6173,
        isPersonsOnly: 'N',
        categoryCode: 'O'
      }
    },
    {
      nomTypeValueId: 6412, code: '6', name: 'Теоретичен изпит', nameAlt: 'Теоретичен изпит',  alias: null,
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'O'
      }
    },
    {
      nomTypeValueId: 6413, code: '7', name: 'Тренажор', nameAlt: 'Тренажор', alias: null,
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'T'
      }
    },
    {
      nomTypeValueId: 6416, code: '9', name: 'Летателно обучение - копие от всичките записи', nameAlt: 'Flight training - copy of all records', alias: null,
      content: {
        direction: 6173,
        isPersonsOnly: 'N',
        categoryCode: 'O'
      }
    },
    {
      nomTypeValueId: 6417, code: '08', name: 'Образование', nameAlt: 'Education', alias: null,
      content: {
        isPersonsOnly: 'N',
        categoryCode: 'E'
      }
    },
    {
      nomTypeValueId: 6418, code: '10', name: 'Летателна книжка', nameAlt: 'Log book', alias: null,
      content: {
        direction: 6173,
        isPersonsOnly: 'N',
        categoryCode: 'A'
      }
    },
    {
      nomTypeValueId: 6440, code: '120', name: 'Разрешение за пребиваване', nameAlt: 'Разрешение за пребиваване', alias: null,
      content: {
        isPersonsOnly: 'Y',
        categoryCode: 'P'
      }
    },
  ];
})(typeof module === 'undefined' ? (this['documentRole'] = {}) : module);
