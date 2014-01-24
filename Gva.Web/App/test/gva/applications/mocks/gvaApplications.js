/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').factory('gvaApplications', ['docs', 'personLots',
    function (docs, personLots) {
      return [
        {
          gvaApplicationId: 1,
          person: {
            lotId: 1,
            personData: _(personLots).filter({ lotId: 1 }).first().personData
          },
          doc: {
            docId: 1
          },
          'case': [
            {
              docId: 1,
              regNumberCol: '000030-2-08.01.2014<br/>Входящ | Публичен',
              docStatusName: 'Чернова',
              descriptionCol: 'ИСКАНЕ по чл. 6 и 18 от Наредбата за документите',
              docFiles: [
                {
                  docFileId: 1,
                  docFileTypeName: 'Лична карта',
                  gvaLotFileId: null
                },
                {
                  docFileId: 2,
                  docFileTypeName: 'Задграничен паспорт',
                  gvaLotFileId: null
                },
                {
                  docFileId: 3,
                  docFileTypeName: 'Медицинска годност',
                  gvaLotFileId: null
                }
              ]
            },
            {
              docId: 100,
              regNumberCol: 'Вътрешен | Вътрешен',
              docStatusName: 'Приключен',
              descriptionCol: 'Резолюция: Резолюция',
              docFiles: [
                {
                  docFileId: 4,
                  docFileTypeName: 'Издържан теоритичен изпит пред ГВА',
                  gvaLotFileId: null
                }
              ]
            },
            {
              docId: 101,
              regNumberCol: 'Вътрешен | Вътрешен',
              docStatusName: 'Приключен',
              descriptionCol: 'Задача: Задача',
              docFiles: []
            }
          ]
        },
        {
          gvaApplicationId: 2,
          docId: 2,
          personLotId: 2
        }
      ];
    }
  ]);
}(angular, _));
