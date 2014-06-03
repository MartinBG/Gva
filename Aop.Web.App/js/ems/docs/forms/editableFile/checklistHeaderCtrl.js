/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChecklistHeaderCtrl(
    $scope,
    Aop
  ) {
    $scope.isLoaded = false;

    $scope.checklist = Aop.loadChecklist({
      id: $scope.model.docId
    }).$promise.then(function (result) {
      $scope.model.jObject = result.content;

      $scope.comparings = _.cloneDeep($scope.model.jObject.versions);
      $scope.comparings.unshift({ emptyChoice: true, version: -1 });

      $scope.checklistVM = {
        currentVersion: $scope.model.jObject.versions[$scope.model.jObject.versions.length - 1],
        compareToVersion: $scope.comparings[0],
        isComparing: false
      };

      $scope.isLoaded = true;
    });

    $scope.view = function (item) {
      $scope.checklistVM.currentVersion = item;
      $scope.checklistVM.compareToVersion = $scope.comparings[0];
      $scope.checklistVM.isComparing = false;
    };

    $scope.compareTo = function (item) {
      $scope.checklistVM.compareToVersion = item;
      if (item.emptyChoice ||
        $scope.checklistVM.compareToVersion.version === $scope.checklistVM.currentVersion.version) {
        $scope.checklistVM.isComparing = false;
      } else {
        $scope.checklistVM.isComparing = true;
      }
    };

    $scope.$watch('readonly', function (value) {
      if ($scope.checklistVM &&
        $scope.checklistVM.currentVersion.version !==
        $scope.model.jObject.versions[$scope.model.jObject.versions.length - 1].version) {
        $scope.checklistVM.currentVersion =
          $scope.model.jObject.versions[$scope.model.jObject.versions.length - 1];
      }

      if (!value && $scope.checklistVM) {
        $scope.checklistVM.compareToVersion = $scope.comparings[0];
        $scope.checklistVM.isComparing = false;
      }
    });
  }

  ChecklistHeaderCtrl.$inject = [
    '$scope',
    'Aop'
  ];

  angular.module('ems').controller('ChecklistHeaderCtrl', ChecklistHeaderCtrl);
}(angular, _));
