/*global angular, _*/
(function (angular, _) {
  'use strict';

  function CaseFinishCtrl(
    $scope,
    $state,
    $stateParams,
    DocStatus,
    doc
  ) {
    $scope.checkedIds = [];

    if ($state.payload) {
      $scope.docRelations = _.map(_.cloneDeep($state.payload.docRelations), function (docRelation) {
        docRelation.check = true;
        return docRelation;
      });
    }
    else {
      return $state.go('^');
    }

    $scope.save = function () {
      _.forEach($scope.docRelations, function (docRelation) {
        if (docRelation.check) {
          $scope.checkedIds.push(docRelation.docId);
        }
      });

      return DocStatus.next({
        id: doc.docId,
        docVersion: doc.version,
        closure: true,
        checkedIds: $scope.checkedIds
      }, {}).$promise.then(function () {
        return $state.transitionTo($state.previous, $stateParams, { reload: true });
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  CaseFinishCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'DocStatus',
    'doc'
  ];

  angular.module('ems').controller('CaseFinishCtrl', CaseFinishCtrl);
}(angular, _));
