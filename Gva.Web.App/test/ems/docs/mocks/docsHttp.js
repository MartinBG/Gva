/*global angular, _, require, jQuery, moment*/
(function (angular, _, require, jQuery, moment) {
  'use strict';
  angular.module('app').config(function ($httpBackendConfiguratorProvider) {

    var nomenclatures = require('./nomenclatures.sample');

    $httpBackendConfiguratorProvider
        .when('GET', '/api/docs?' +
          'filter&fromDate&toDate&regUri&docName&' +
          'docTypeId&docStatusId&hideRead&isCase&corrs&units&ds&hasLot',
        function ($params, docs, applicationsFactory) {

          var searchParams = _.cloneDeep($params);
          delete searchParams.hasLot;
          delete searchParams.isCase;
          delete searchParams.fromDate;
          delete searchParams.toDate;
          delete searchParams.filter;

          var result = docs;

          if (!!$params.filter) {
            if ($params.filter === 'current') {
              result = _(result).filter(function (doc) {
                return doc.docStatusId !== 4 && doc.docStatusId !== 5;
              });
            }
            else if ($params.filter === 'finished') {
              result = _(result).filter(function (doc) {
                return doc.docStatusId === 4;
              });
            }
            else if ($params.filter === 'draft') {
              result = _(result).filter(function (doc) {
                return doc.docStatusId === 1;
              });
            }
            else if ($params.filter === 'portal') {
              result = _(result).filter(function (doc) {
                return !!doc.docSourceType && doc.docSourceType.nomValueId === 1;
              });
            }
          }

          result = _(result).filter(function (doc) {

            if ($params.hasLot && $params.hasLot.toLowerCase() === 'true') {
              if (!applicationsFactory.getByDocId(doc.docId)) {
                return false;
              }
            }
            else if ($params.hasLot && $params.hasLot.toLowerCase() === 'false') {
              if (applicationsFactory.getByDocId(doc.docId)) {
                return false;
              }
            }

            if ($params.isCase && $params.isCase.toLowerCase() === 'true') {
              if (!!doc.parentDocId) {
                return false;
              }
            }
            else if ($params.isCase && $params.isCase.toLowerCase() === 'false') {
              if (!doc.parentDocId) {
                return false;
              }
            }

            if ($params.fromDate) {
              if (moment(doc.regDate).startOf('day') < moment($params.fromDate)) {
                return false;
              }
            }

            if ($params.toDate) {
              if (moment($params.toDate) < moment(doc.regDate).startOf('day')) {
                return false;
              }
            }

            var isMatch = true;

            _.forOwn(searchParams, function (value, param) {
              if (!value || param === 'exact') {
                return;
              }

              if (searchParams.exact) {
                isMatch =
                  isMatch && doc[param] && doc[param] === searchParams[param];
              } else {
                isMatch =
                  isMatch && doc[param] && doc[param].toString().indexOf(searchParams[param]) >= 0;
              }

              //short circuit forOwn if not a match
              return isMatch;
            });

            return isMatch;
          })
          .value();

          return [200, {
            documents: result,
            documentCount: result.length,
            msg: ''
          }];
        })
        .when('POST', '/api/docs/new/create',
        function ($jsonData, docsFactory) {

          if (!$jsonData) {
            return [400];
          }

          var newDoc = docsFactory.createDoc($jsonData);

          return [200, { docId: newDoc.docId }];
        })
        .when('POST', '/api/docs/new/register',
        function ($jsonData, docsFactory) {
          if (!$jsonData) {
            return [400];
          }

          var newDoc = docsFactory.registerDoc($jsonData);

          return [200, { docId: newDoc.docId, regUri: newDoc.regUri }];
        })
        .when('GET', '/api/docs/:id',
        function ($params, docs) {
          var doc = _(docs).filter({ docId: parseInt($params.id, 10) }).first();

          if (!doc) {
            return [400];
          }

          return [200, doc];
        })
        .when('POST', '/api/docs/:id',
        function ($params, $jsonData, $filter, docs) {
          var docId = parseInt($params.id, 10),
            docIndex = docs.indexOf($filter('filter')(docs, { docId: docId })[0]);

          if (docIndex === -1) {
            return [400];
          }

          docs[docIndex] = $jsonData;

          return [200];
        })
        .when('POST', '/api/docs/:docId/nextStatus',
        function ($params, docs) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          var currentStatus = _(nomenclatures.docStatus)
              .filter({ nomValueId: doc.docStatusId })
              .first();

          if (currentStatus.alias === 'Draft' ||
              currentStatus.alias === 'Prepared' ||
              currentStatus.alias === 'Processed') {
            var newStatus = _(nomenclatures.docStatus)
              .filter({ nomValueId: currentStatus.nomValueId + 1 })
              .first();

            doc.docStatusId = newStatus.nomValueId;
            doc.docStatusName = newStatus.name;
            doc.docStatusAlias = newStatus.alias;
          }

          return [200, { docId: doc.docId }];
        })
        .when('POST', '/api/docs/:docId/reverseStatus',
        function ($params, docs) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          var currentStatus = _(nomenclatures.docStatus)
              .filter({ nomValueId: doc.docStatusId })
              .first();

          var newStatus;
          if (currentStatus.alias === 'Prepared' ||
              currentStatus.alias === 'Processed' ||
              currentStatus.alias === 'Finished') {
            newStatus = _(nomenclatures.docStatus)
              .filter({ nomValueId: currentStatus.nomValueId - 1 })
              .first();

            doc.docStatusId = newStatus.nomValueId;
            doc.docStatusName = newStatus.name;
            doc.docStatusAlias = newStatus.alias;
          }
          else if (currentStatus.alias === 'Canceled') {
            newStatus = _(nomenclatures.docStatus)
              .filter({ alias: 'Processed' })
              .first();

            doc.docStatusId = newStatus.nomValueId;
            doc.docStatusName = newStatus.name;
            doc.docStatusAlias = newStatus.alias;
          }

          return [200, { docId: doc.docId }];
        })
        .when('POST', '/api/docs/:docId/cancelStatus',
        function ($params, docs) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          var cancelStatus = _(nomenclatures.docStatus)
              .filter({ alias: 'Canceled' })
              .first();

          doc.docStatusId = cancelStatus.nomValueId;
          doc.docStatusName = cancelStatus.name;
          doc.docStatusAlias = cancelStatus.alias;

          return [200, { docId: doc.docId }];
        })
        .when('POST', '/api/docs/:docId/setRegUri',
        function ($params, docs) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          if (!doc.regUri) {
            doc.regUri = '000030-' + doc.docId + '-' + moment().format('YYYY-MM-DD');
          }

          return [200, { docId: doc.docId }];
        })
        .when('POST', '/api/docs/:docId/setCasePart',
        function ($params, $jsonData, docs, docCases) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          var newDocCasePartType = _(nomenclatures.docCasePartType)
            .filter({ docCasePartTypeId: $jsonData.docCasePartTypeId })
            .first();

          if (!doc || !newDocCasePartType) {
            return [400];
          }

          doc.docCasePartTypeId = newDocCasePartType.docCasePartTypeId;
          doc.docCasePartTypeName = newDocCasePartType.name;

          var docCase = _(docCases).filter(function (item) {
            return _(item.docCase).any({ docId: doc.docId });
          }).first().docCase;

          var docItem = _(docCase).filter({ docId: doc.docId }).first();
          docItem.casePartType = newDocCasePartType.name;

          return [200, { docId: doc.docId }];
        })
        .when('POST', '/api/docs/:docId/setDocType',
        function ($params, $jsonData, docs, docCases) {
          var doc = _(docs).filter({ docId: parseInt($params.docId, 10) }).first();

          if (!doc) {
            return [400];
          }

          doc.docTypeGroupId = $jsonData.docTypeGroupId;
          doc.docTypeId = $jsonData.docTypeId;
          doc.docTypeName = $jsonData.docType.name;
          doc.docDirectionId = $jsonData.docDirectionId;
          doc.docDirectionName = $jsonData.docDirection.name;

          doc.docUnitsFrom = $jsonData.docUnitsFrom;
          doc.docUnitsTo = $jsonData.docUnitsTo;
          doc.docUnitsCCopy = $jsonData.docUnitsCCopy;
          doc.docUnitsImportedBy = $jsonData.docUnitsImportedBy;
          doc.docUnitsMadeBy = $jsonData.docUnitsMadeBy;
          doc.docUnitsInCharge = $jsonData.docUnitsInCharge;
          doc.docUnitsControlling = $jsonData.docUnitsControlling;
          doc.docUnitsRoleReaders = $jsonData.docUnitsRoleReaders;
          doc.docUnitsEditors = $jsonData.docUnitsEditors;
          doc.docUnitsRoleRegistrators = $jsonData.docUnitsRoleRegistrators;

          doc.isDocIncoming = $jsonData.isDocIncoming;
          doc.isDocInternal = $jsonData.isDocInternal;
          doc.isDocOutgoing = $jsonData.isDocOutgoing;
          doc.isDocInternalOutgoing = $jsonData.isDocInternalOutgoing;
          doc.isResolution = $jsonData.isResolution;
          doc.isRemark = $jsonData.isRemark;
          doc.isTask = $jsonData.isTask;

          var docCase = _(docCases).filter(function (item) {
            return _(item.docCase).any({ docId: doc.docId });
          }).first().docCase;

          var docItem = _(docCase).filter({ docId: doc.docId }).first();
          docItem.direction = $jsonData.docDirection.name;

          return [200, { docId: doc.docId }];
        });
  });
}(angular, _, require, jQuery, moment));
