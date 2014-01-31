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

      return {
        getInstance: function () {
          return [
            {
              applicationId: 1,
              person: {
                lotId: 1,
                personData: _(personLots).filter({ lotId: 1 }).first().personData
              },
              doc: {
                docId: 1,
                docTypeName: _(docs).filter({ docId: 1 }).first().docTypeName
              },
              docCase: _(docCases).filter({ docCaseId: 1 }).first().docCase.map(docCaseMapper)
            },
            {
              applicationId: 2,
              docId: 2,
              personLotId: 2
            }
          ];
        }

      };

    }
  ]);
}(angular, _));
