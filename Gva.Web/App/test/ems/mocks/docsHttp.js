/*global angular, require, _*/
(function (angular, _) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    var nomenclatures = require('./nomenclatures.sample');

    $httpBackendConfiguratorProvider
      .when('GET', '/api/docs?fromDate&toDate&regUri&docName&docTypeId&docStatusId&corrs&units',
        function ($params, docs) {
          var t = nomenclatures.get('countries', 'Belgium'),
            result = _(docs).filter(function (d) {
              var isMatch = true;


              if (d || $params) {
                return true;
              }
              else {
                return false;
              }

              return isMatch;
            })
          .value();

          t = undefined;

          return [200, result];
        })
      .when('GET', '/api/docs/new',
        function () {
          var newDoc = {
            docId: undefined,
            docStatusId: undefined,
            docStatusName: undefined,
            docSubject: undefined,
            docSubjectLabel: undefined,
            docTypeId: undefined,
            docDirectionName: undefined,
            docDirectionId: undefined,
            docTypeName: undefined,
            regDate: undefined,
            regUri: undefined,
            regIndex: undefined,
            regNumber: undefined,
            correspondentName: undefined,
            caseRegUri: undefined,//
            docFormatTypeId: undefined,
            docCasePartTypeId: undefined,
            docTypeGroupId: undefined,//
            docCorrespondents: [],//
            numberOfDocuments: undefined//
          };

          return [200, newDoc];

        })
      .when('POST', '/api/docs/saveNew',
        function ($jsonData, docs) {
          if (!$jsonData) {
            return [400];
          }

          var nextDocId = _(docs).pluck('docId').max().value() + 1;

          $jsonData.docId = nextDocId;
          $jsonData.regUri = '000030-' + $jsonData.docId + '-05.01.2014';
          docs.push($jsonData);

          return [200, $jsonData];
        })
      .when('GET', '/api/docs/:docId',
        function ($params, docs) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          return [200, doc];
        })
       .when('POST', '/api/docs/:docId',
        function ($params, $jsonData, $filter, docs) {
          var docId = parseInt($params.docId, 10),
            docIndex = docs.indexOf($filter('filter')(docs, { docId: docId })[0]);

          if (docIndex === -1) {
            return [400];
          }

          docs[docIndex] = $jsonData;

          return [200];
        });
  });
}(angular, _));
