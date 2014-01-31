/*global angular*/
(function (angular) {
  'use strict';

  angular.module('app').constant('docCases', [
    {
      docCaseId: 1,
      docCase: [
        {
          docId: 1,
          regDate: '2014-01-08T17:22:14.58',
          regNumber: '000030-2-08.01.2014',
          direction: 'Входящ',
          casePartType: 'Публичен',
          statusName: 'Чернова',
          description: 'Издаване на свидетелство за правоспособност на авиационен персонал'
        },
        {
          docId: 2,
          regDate: '2014-01-08T17:22:14.58',
          regNumber: '000030-2-08.01.2014',
          direction: 'Входящ',
          casePartType: 'Входящ',
          statusName: 'Изготвен',
          description: 'Резолюция'
        },
        {
          docId: 3,
          regDate: '2014-01-08T17:22:14.58',
          regNumber: '000030-2-08.01.2014',
          direction: 'Входящ',
          casePartType: 'Входящ',
          statusName: 'Обработен',
          description: 'Писмо'
        }
      ]
    }
  ]);

}(angular));

