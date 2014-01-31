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
        p.docFiles = _(docs).filter({ docId: p.docId }).first().publicDocFiles.map(docFileMapper);
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
        saveApplication: function (application) {
          applications.push(application);
        }
      };

    }
  ]);
}(angular, _));
