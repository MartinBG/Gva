/*global angular*/
(function (angular) {
  'use strict';

  function EditCasePartCtrl(
    $scope,
    $state,
    $stateParams,
    Docs,
    doc
  ) {
    $scope.docId = doc.docId;
    $scope.docVersion = doc.version;
    $scope.docCasePartTypeId = doc.docCasePartTypeId;

    $scope.save = function () {
      return $scope.casePartForm.$validate().then(function () {
        if ($scope.casePartForm.$valid) {
          if ($scope.docCasePartTypeId !== doc.docCasePartTypeId) {
            return Docs
              .setCasePart({
                id: $scope.docId,
                docVersion: $scope.docVersion,
                docCasePartTypeId: $scope.docCasePartTypeId
              }, {})
              .$promise
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
    'Docs',
    'doc'
  ];

  angular.module('ems').controller('EditCasePartCtrl', EditCasePartCtrl);
}(angular));
