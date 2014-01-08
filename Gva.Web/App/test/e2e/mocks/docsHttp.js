/*global angular, require*/
(function (angular) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var nomenclatures = require('./nomenclatures.sample'),
      today = new Date(),
      docs = [{
        docId: 1,
        docStatusId: 1,
        docStatusName: 'Чернова',
        docSubject: 'Заявление за обновяване на автомати',
        docSubjectLabel: 'Относно',
        docTypeId: 1,
        docDirectionName: 'Входящ',
        docDirectionId: 1,
        docTypeName: 'ИСКАНЕ по чл. 19, ал. 2, т. 2 от Наредбата за документите',
        regDate: today,
        regUri: '000030-1-05.01.2014',
        regIndex: '000030',
        regNumber: 1,
        correspondentName: 'Ви Ем Уеър'
      }, {
        docId: 2,
        docStatusId: 2,
        docStatusName: 'Изготвен',
        docSubjectLabel: 'Относно',
        docSubject: 'Заявление за добавяне на автомати',
        docTypeId: 1,
        docDirectionName: 'Входящ',
        docDirectionId: 1,
        docTypeName: 'ИСКАНЕ по чл. 21, ал. 2, т. 2 от Наредбата за документите',
        regDate: new Date(today.getTime() + (24 * 60 * 60 * 1000)),
        regUri: '000030-2-05.01.2014',
        regIndex: '000030',
        regNumber: 2,
        correspondentName: 'Стефанстрой ЕООД'
      }, {
        docId: 3,
        docStatusId: 3,
        docStatusName: 'Обработен',
        docSubjectLabel: 'Относно',
        docSubject: 'Искане за закриване на обект',
        docTypeId: 2,
        docDirectionName: 'Входящ',
        docDirectionId: 1,
        docTypeName: 'Писмо',
        regDate: new Date(today.getTime() + (48 * 60 * 60 * 1000)),
        regUri: '000030-3-05.01.2014',
        regIndex: '000030',
        regNumber: 2,
        correspondentName: 'Казино Легал'
      }];

    $httpBackendConfiguratorProvider
      .when('GET', '/api/docs?fromDate&toDate',
        function () { //$params, $filter
          var t = nomenclatures.get('countries', 'Belgium');
          t = undefined;
          return [200, docs];
        })
      .when('GET', '/api/docs/:docId',
        function ($params, $filter) {
          var docId = parseInt($params.docId, 10),
            doc = $filter('filter')(docs, { docId: docId })[0];

          if (!doc) {
            return [400];
          }

          return [200, doc];
        })
       .when('POST', '/api/docs/:docId',
        function ($params, $jsonData, $filter) {
          var docId = parseInt($params.docId, 10),
            docIndex = docs.indexOf($filter('filter')(docs, { docId: docId })[0]);

          if (docIndex === -1) {
            return [400];
          }

          docs[docIndex] = $jsonData;

          return [200];
        });
  });
}(angular));
