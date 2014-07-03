/*global angular, _*/
(function (angular, _) {
  'use strict';

  function DocsCaseCtrl(
    $scope,
    $state,
    Admissions,
    doc
  ) {
    $scope.docId = doc.docId;

    $scope.isCase = doc.isCase;

    $scope.isLoaded = false;
    $scope.lotType = undefined;
    $scope.lotId = undefined;

    Admissions.findDocLotLink({
      id: $scope.docId
    }).$promise.then(function (result) {
      $scope.isLoaded = true;
      $scope.lotType = result.lotType;
      $scope.lotId = result.lotId;
    });

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
      return $state.go('root.docs.edit.view', { id: docId });
    };

    $scope.gotoLot = function () {
      if ($scope.lotType === 'ДОИ') {
        return $state.go('root.admissions.edit', { id: $scope.lotId });
      } else if ($scope.lotType === 'Сигнал') {
        return $state.go('root.signals.edit', { id: $scope.lotId });
      } else if ($scope.lotType === 'Предложение') {
        return $state.go('root.suggestions.edit', { id: $scope.lotId });
      }
    };

    $scope.createAdmission = function () {
      Admissions.createDocLotLink({
        id: $scope.docId,
        lotType: 'admission'
      }, {}).$promise.then(function (result) {
        return $state.go('root.admissions.edit', { id: result.id });
      });
    };

    $scope.createSignal = function () {
      Admissions.createDocLotLink({
        id: $scope.docId,
        lotType: 'signal'
      }, {}).$promise.then(function (result) {
        return $state.go('root.signals.edit', { id: result.id });
      });
    };

    $scope.createSuggestion = function () {
      Admissions.createDocLotLink({
        id: $scope.docId,
        lotType: 'suggestion'
      }, {}).$promise.then(function (result) {
        return $state.go('root.suggestions.edit', { id: result.id });
      });
    };
  }

  DocsCaseCtrl.$inject = [
    '$scope',
    '$state',
    'Admissions',
    'doc'
  ];

  angular.module('ems').controller('DocsCaseCtrl', DocsCaseCtrl);
}(angular, _));
