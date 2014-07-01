/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsCaseCtrl(
    $scope,
    $state,
    Aops,
    doc
  ) {
    $scope.aopAppId = doc.aopAppId;
    $scope.docId = doc.docId;
    
    $scope.isCase = doc.isCase;

    $scope.aopApplicationId = undefined;

    Aops.findAopApp({
      id: $scope.docId
    }).$promise.then(function (result) {
      $scope.aopApplicationId = result.aopApplicationId;
    });

    $scope.docRelations = [];
    $scope.relations = _.cloneDeep(doc.docRelations);

    var index = _.findIndex($scope.relations, { 'parentDocId': undefined }),
      parentDoc = $scope.relations.splice(index, 1)[0];
    parentDoc.rowId = 1;
    $scope.docRelations.push(parentDoc);

    function buildHierarchy(currentParent){
      var childId = 1;
      var children = _.where($scope.relations, {'parentDocId': currentParent.docId});

      _.forEach(children, function(docRelation){
        docRelation.parentId = currentParent.rowId;
        docRelation.rowId =  docRelation.parentId + '.' + childId++;

        $scope.docRelations.push(docRelation);
        $scope.relations.splice(_.findIndex($scope.relations, { 'docId': docRelation.docId }), 1);

        if(_.where($scope.relations, {'parentDocId': docRelation.docId}).length > 0){
          buildHierarchy(docRelation);
        }
      });
    }

    buildHierarchy(parentDoc);

    $scope.viewDoc = function (docId) {
      return $state.go('root.docs.edit.view', { id: docId });
    };

    $scope.gotoAopApp = function () {
      return $state.go('root.apps.edit', { id: $scope.aopApplicationId });
    };
  }

  DocsCaseCtrl.$inject = [
    '$scope',
    '$state',
    'Aops',
    'doc'
  ];

  angular.module('ems').controller('DocsCaseCtrl', DocsCaseCtrl);
}(angular, _));
