/*global angular*/
(function (angular) {
  'use strict';

  function EditCasePartCtrl(
    $scope,
    $state,
    $stateParams,
    Doc,
    doc,
    casePartModel
  ) {
    $scope.casePartModel = casePartModel;

    $scope.save = function () {
      $scope.casePartForm.$validate().then(function () {
        if ($scope.casePartForm.$valid) {
          if ($scope.casePartModel.docCasePartTypeId !== doc.docCasePartTypeId) {
            var casePartData = {
              docCasePartTypeId: $scope.casePartModel.docCasePartTypeId
            };

            return Doc.setCasePart({ docId: casePartModel.docId }, casePartData).$promise
              .then(function () {
              return $state.transitionTo('root.docs.edit.case', $stateParams, { reload: true });
            });
          }
          else {
            return $state.go('^');
          }
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  EditCasePartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Doc',
    'doc',
    'casePartModel'
  ];

  EditCasePartCtrl.$resolve = {
    casePartModel: ['doc',
      function (doc) {
        return {
          docId: doc.docId,
          docCasePartTypeId: doc.docCasePartTypeId
        };
      }
    ]
  };

  angular.module('ems').controller('EditCasePartCtrl', EditCasePartCtrl);
}(angular));
