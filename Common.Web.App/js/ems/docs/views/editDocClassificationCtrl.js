/*global angular*/
(function (angular) {
  'use strict';

  function EditDocClassificationCtrl(
    $scope,
    $state,
    $stateParams,
    Docs,
    doc
  ) {
    $scope.docClassifications = doc.docClassifications;

    $scope.addDocClassification = function addDocClassification(target) {
      target.isActive = true;
    };

    $scope.removeDocClassification = function removeDocClassification(target) {
      if (target.isAdded) {
        $scope.docClassifications.splice($scope.docClassifications.indexOf(target), 1);
      }
      target.isActive = false;
    };

    $scope.addNewDocClassification = function addNewDocClassification() {
      $scope.docClassifications.push({
        docId: $stateParams.id,
        classificationId: null,
        isActive: true,
        isInherited: true,
        isAdded: true
      });
    };

    $scope.save = function () {
      return $scope.editDocClassificationForm.$validate().then(function () {
        if ($scope.editDocClassificationForm.$valid) {
          return Docs.changeDocClassification({
            id: $stateParams.id
          }, $scope.docClassifications).$promise.then(function () {
            return $state.go('^');
          });
        }
      });
    };

    $scope.cancel = function () {
      return $state.go('^');
    };
  }

  EditDocClassificationCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Docs',
    'doc'
  ];

  angular.module('ems').controller('EditDocClassificationCtrl', EditDocClassificationCtrl);
}(angular));
