/*global angular, _*/
(function (angular, _) {
  'use strict';

  angular.module('app').factory('applicationsFactory', [
    'docs',
    'personLots',
    'docCases',
    'applicationLotFiles',
    function (docs, personLots, docCases, applicationLotFiles) {
      function docFileMapper(value) {
        var docFiles = {
          docFile: value,
          appFile: null
        };
        var appLotFile = _(applicationLotFiles).filter({ docFileKey: value.key }).first();

        if (!!appLotFile) {
          docFiles.appFile = {};
          docFiles.appFile.partIndex = appLotFile.partIndex;
          docFiles.appFile.part = appLotFile.part;
          docFiles.appFile.setPartName = appLotFile.setPartName;
          docFiles.appFile.setPartAlias = appLotFile.setPartAlias;
        }

        return docFiles;
      }

      function docCaseMapper(value) {
        var docCase = {
          docInfo: value,
          docFiles: null
        };
        var docFiles = _(docs).filter({ docId: value.docId }).first().publicDocFiles;

        if (!!docFiles) {
          docCase.docFiles = docFiles.map(docFileMapper);
        }

        return docCase;
      }

      var applications = [
        {
          applicationId: 1,
          lotId: 1,
          docId: 1
        }
      ];

      return {
        getAll: function () {
          var self = this;
          return _(applications).map(function (application) {
            return self.getApplication(application.applicationId);
          }).value();
        },
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
        getByDocIdAndLotId: function (docId, lotId) {
          return _(applications).filter({ docId: docId, lotId: lotId }).first();
        },
        getByDocId: function (docId) {
          return _(applications).filter({ docId: docId}).first();
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
