/*global angular, require*/
(function (angular) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var nomenclatures = require('./nomenclatures.sample'),
      docs = [{
        docId: 1,
        docStatusId: 1,
        docStatusName: 'Чернова',
        docSubjectLabel: 'Относно',
        docTypeId: 1,
        docDirectionName: 'Входящ',
        docDirectionId: 1,
        //docSubject: 'Подадено заявление',
        docTypeName: 'ИСКАНЕ по чл. 19, ал. 2, т. 2 от Наредбата за документите',
        regDate: '05.01.2014 17:44',
        regUri: '000030-1-05.01.2014',
        regIndex: '000030',
        regNumber: 1,
        docSubject: nomenclatures.correspondentGroups[0]
      }, {
        docId: 2
      }];

    $httpBackendConfiguratorProvider
      .when('GET', '/api/docs/:docId',
        function ($params, $filter) {
          var docId = parseInt($params.docId, 10),
            doc = $filter('filter')(docs, { docId: docId })[0];

          if (!doc) {
            return [400];
          }

          return [200, doc];
        });
  });
}(angular));
