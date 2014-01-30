﻿/*global angular, _*/
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
          docCase: [
            {
              docId: 1,
              regNumberCol: '000030-2-08.01.2014<br/>Входящ | Публичен',
              docStatusName: 'Чернова',
              descriptionCol: 'ИСКАНЕ по чл. 6 и 18 от Наредбата за документите',
              docFiles: [
                {
                  docFileId: 1,
                  docFileTypeId: 1,
                  docFileTypeName: 'PDF',
                  docFileTypeAlias: 'PDF',
                  gvaLotFileId: null,
                  part: null,
                  partIndex: null
                },
                {
                  docFileId: 2,
                  docFileTypeId: 2,
                  docFileTypeName: 'WORD',
                  docFileTypeAlias: 'WORD',
                  gvaLotFileId: null,
                  part: null,
                  partIndex: null
                },
                {
                  docFileId: 3,
                  docFileTypeId: 3,
                  docFileTypeName: 'EXCEL',
                  docFileTypeAlias: 'EXCEL',
                  gvaLotFileId: null,
                  part: null,
                  partIndex: null
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
                  docFileTypeId: 1,
                  docFileTypeName: 'PDF',
                  docFileTypeAlias: 'PDF',
                  gvaLotFileId: null,
                  part: null,
                  partIndex: null
                },
                {
                  docFileId: 5,
                  docFileTypeId: 2,
                  docFileTypeName: 'WORD',
                  docFileTypeAlias: 'PDF',
                  gvaLotFileId: null,
                  part: null,
                  partIndex: null
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
