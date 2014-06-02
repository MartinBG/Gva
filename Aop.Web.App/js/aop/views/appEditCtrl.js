/*global angular*/
(function (angular) {
  'use strict';

  function AppsEditCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    Aop,
    app,
    selectDoc
  ) {
    $scope.inEditMode = false;
    $scope.app = app;

    $scope.enterEditMode = function () {
      $scope.inEditMode = true;
    };

    $scope.exitEditMode = function () {
      return $state.transitionTo($state.current, $stateParams, { reload: true });
    };

    $scope.save = function save() {
      return $scope.appForm.$validate()
        .then(function () {
          if ($scope.appForm.$valid) {
            return Aop
              .save($stateParams, $scope.app)
              .$promise
              .then(function (data) {
                return $state.go('root.apps.edit', { id: data.aopApplicationId }, { reload: true });
              });
          }
        });
    };

    $scope.fastSave = function save() {
      return Aop
        .save($stateParams, $scope.app)
        .$promise
        .then(function (data) {
          return $state.go('root.apps.edit', { id: data.aopApplicationId }, { reload: true });
        });
    };

    $scope.cancel = function cancel() {
      return $state.go('root.apps.search');
    };

    $scope.newAopEmployer = function () {
      return $state.go('root.apps.edit.newAopEmployer');
    };

    $scope.connectToDoc = function (type) {
      return $state.go('root.apps.edit.docSelect', { type: type }, null, null);
    };

    $scope.disconnectDoc = function (connections) {
      for (var i = 0; i < connections.length; i++) {
        $scope.app[connections[i]] = undefined;
      }

      return $scope.fastSave();
    };

    //ST
    $scope.generateSTChecklist = function (action) {
      return Aop
        .generateChecklist({
          id: $scope.app.aopApplicationId,
          action: action,
          identifier: 'st'
        }, {})
        .$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.generateSTNote = function () {
      return Aop
        .generateNote({
          id: $scope.app.aopApplicationId
        }, {})
        .$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    //ND
    $scope.generateNDChecklist = function (action) {
      return Aop
        .generateChecklist({
          id: $scope.app.aopApplicationId,
          action: action,
          identifier: 'nd'
        }, {})
        .$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    $scope.generateNDReport = function () {
      return Aop
        .generateReport({
          id: $scope.app.aopApplicationId
        }, {})
        .$promise
        .then(function () {
          return $state.transitionTo($state.current, $stateParams, { reload: true });
        });
    };

    if (selectDoc.length > 0) {
      var sd = selectDoc.pop();
      switch (sd.type) {
        case 'stDoc':
          $scope.app.stDocId = sd.docId;
          return $scope.fastSave();
        case 'stChecklist':
          $scope.app.stChecklistId = sd.docId;
          return $scope.fastSave();
        case 'stNote':
          $scope.app.stNoteId = sd.docId;
          return $scope.fastSave();
        case 'ndDoc':
          $scope.app.ndDocId = sd.docId;
          return $scope.fastSave();
        case 'ndChecklist':
          $scope.app.ndChecklistId = sd.docId;
          return $scope.fastSave();
        case 'ndReport':
          $scope.app.ndReportId = sd.docId;
          return $scope.fastSave();
      }
    }
  }

  AppsEditCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Aop',
    'app',
    'selectDoc'
  ];

  AppsEditCtrl.$resolve = {
    app: [
      '$stateParams',
      'Aop',
      function resolveApp($stateParams, Aop) {
        return Aop.get({ id: $stateParams.id }).$promise;
      }
    ],
    selectDoc: [function () {
      return [];
    }]
  };

  angular.module('aop').controller('AppsEditCtrl', AppsEditCtrl);
}(angular));
