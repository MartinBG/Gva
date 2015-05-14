/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsCaseCtrl(
    $scope,
    $state,
    scModal,
    application,
    doc
  ) {
    $scope.doc = doc;
    $scope.appId = application.id;
    $scope.docId = doc.docId;
    $scope.isCase = doc.isCase;

    $scope.docRelations = [];
    $scope.relations = _.cloneDeep(doc.docRelations);

    var index = _.findIndex($scope.relations, function (item) {
      return item.parentDocId === undefined || item.parentDocId === null;
    }),
      parentDoc = $scope.relations.splice(index, 1)[0];
    parentDoc.rowId = 1;
    $scope.docRelations.push(parentDoc);

    function buildHierarchy(currentParent) {
      var childId = 1;
      var children = _.where($scope.relations, { 'parentDocId': currentParent.docId });

      _.forEach(children, function (docRelation) {
        docRelation.parentId = currentParent.rowId;
        docRelation.rowId = docRelation.parentId + '.' + childId++;

        $scope.docRelations.push(docRelation);
        $scope.relations.splice(_.findIndex($scope.relations, { 'docId': docRelation.docId }), 1);

        if (_.where($scope.relations, { 'parentDocId': docRelation.docId }).length > 0) {
          buildHierarchy(docRelation);
        }
      });
    }

    buildHierarchy(parentDoc);

    $scope.viewDoc = function (docId) {
      return $state.go('root.docs.edit.case', { id: docId });
    };

    $scope.viewApplication = function (applicationId) {
      return $state.go('root.applications.edit.case', { id: applicationId });
    };

    $scope.linkApplication = function () {
      var modalInstance = scModal.open('linkApplication', { 
        docId: $scope.docId,
        isFromPortal: doc.docStatusAlias === 'FromPortal',
        docRegUri: doc.regUri
      });

      modalInstance.result.then(function (res) {
        return $state.go('root.applications.edit.data', {
          id: res.gvaApplicationId,
          set: res.set,
          lotId: res.lotId,
          ind: res.partIndex
        });
      });

      return modalInstance.opened;
    };
  }

  DocsCaseCtrl.$inject = [
    '$scope',
    '$state',
    'scModal',
    'application',
    'doc'
  ];

  DocsCaseCtrl.$resolve = {
    doc: [
      'doc',
      function (doc) {
        return doc;
      }
    ],
    application: [
      '$stateParams',
      'Applications',
      function resolveApplication($stateParams, Applications) {
        return Applications.getApplicationByDocId({ docId: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('ems').controller('DocsCaseCtrl', DocsCaseCtrl);
}(angular, _));
