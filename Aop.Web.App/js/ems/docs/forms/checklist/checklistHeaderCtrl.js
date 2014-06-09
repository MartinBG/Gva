/*global angular, _*/
(function (angular, _) {
  'use strict';

  function ChecklistHeaderCtrl(
    $scope,
    $state,
    Aop
  ) {
    $scope.isLoaded = false;

    Aop.loadChecklist({
      id: $scope.model.docId
    }).$promise.then(function (result) {
      $scope.model.jObject = result.content;
      $scope.aopApplicationId = result.aopApplicationId;

      $scope.comparings = _.cloneDeep($scope.model.jObject.versions);

      $scope.checklistVM = {
        currentVersion: $scope.model.jObject.versions[0],
        compareToVersion: $scope.model.jObject.versions[0],
        isComparing: false
      };

      $scope.isLoaded = true;
    });

    $scope.gotoAopApp = function () {
      return $state.go('root.apps.edit', { id: $scope.aopApplicationId });
    };

    $scope.view = function (item) {
      $scope.checklistVM.currentVersion = item;
      $scope.checklistVM.compareToVersion = item;
      $scope.checklistVM.isComparing = false;
    };

    $scope.compareTo = function (item) {
      if (item.version === $scope.checklistVM.currentVersion.version) {
        $scope.checklistVM.isComparing = false;
      } else {
        $scope.checklistVM.isComparing = true;
      }
      $scope.checklistVM.compareToVersion = item;
    };

    $scope.$watch('readonly', function (value) {
      if ($scope.checklistVM &&
        $scope.checklistVM.currentVersion.version !==
        $scope.model.jObject.versions[0].version) {
        $scope.checklistVM.currentVersion =
          $scope.model.jObject.versions[0];
      }

      if (!value && $scope.checklistVM) {
        $scope.checklistVM.compareToVersion = $scope.checklistVM.currentVersion;
        $scope.checklistVM.isComparing = false;
      }
    });
  }

  ChecklistHeaderCtrl.$inject = [
    '$scope',
    '$state',
    'Aop'
  ];

  angular.module('ems').controller('ChecklistHeaderCtrl', ChecklistHeaderCtrl);
}(angular, _));
