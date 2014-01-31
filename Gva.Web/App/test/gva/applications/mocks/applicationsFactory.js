/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').factory('applicationsFactory', [
    'docs',
    'personLots',
    'docCases',
    'applicationLotFiles',
    function (docs, personLots, docCases, applicationLotFiles) {
      function docFileMapper(d) {
        var appLotFile = _(applicationLotFiles).filter({ docFileId: d.key }).first();

        if (!!appLotFile) {
          d.partIndex = appLotFile.partIndex;
          d.part = appLotFile.part;
          d.setPartName = appLotFile.setPartName;
          d.setPartId = appLotFile.setPartId;
        }

        return d;
      }

      function docCaseMapper(p) {
        var publicDocFiles = _(docs).filter({ docId: p.docId }).first().publicDocFiles;
        if (publicDocFiles) {
          p.docFiles = publicDocFiles.map(docFileMapper);
        }
        return p;
      }

      var applications = [
        {
          applicationId: 1,
          lotId: 1,
          docId: 1
        }
      ];

      return {
        getApplication: function (id) {
          var application = _.cloneDeep(_(applications).filter({ applicationId: id }).first());

          application.docCase = _(docCases)
              .filter({ docCaseId: application.docId }).first().docCase.map(docCaseMapper);
          application.personData = _(personLots)
              .filter({ lotId: application.lotId }).first().personData;
          application.doc = _(docs)
              .filter({ docId: application.docId }).first();

          return application;
        },
        getApplicationByDocAndPerson: function (docId, lotId) {
          return _(applications).filter({ docId: docId, lotId: lotId }).first();
        },
        getNextApplicationId: function () {
          return _(applications).pluck('applicationId').max().value() + 1;
        },
        saveApplication: function (application) {
          applications.push(application);
        }
      };

    }
  ]);
}(angular, _));
