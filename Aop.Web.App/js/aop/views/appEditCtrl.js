/*global angular*/
(function (angular) {
  'use strict';

  function AppsEditCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    Aops,
    scMessage,
    app,
    selectDoc
  ) {
    $scope.inEditMode = ($state.payload && $state.payload.inEditMode) || false;
    $scope.app = app;

    $scope.enterEditMode = function () {
      $scope.inEditMode = true;
    };

    $scope.exitEditMode = function () {
      return $state.transitionTo($state.current, $stateParams, { reload: true });
    };

    $scope.readFedForFirstStage = function remove() {
      return Aops.readFedForFirstStage({ id: $scope.app.aopApplicationId }, {})
        .$promise
        .then(function (data) {
          return $state.go('root.apps.edit', { id: data.aopApplicationId }, { reload: true });
        });
    };

    $scope.readFedForSecondStage = function remove() {
      return Aops.readFedForSecondStage({ id: $scope.app.aopApplicationId }, {})
        .$promise
        .then(function (data) {
          return $state.go('root.apps.edit', { id: data.aopApplicationId }, { reload: true });
        });
    };

    $scope.remove = function remove() {
      return scMessage('apps.edit.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            return Aops.remove({ id: $scope.app.aopApplicationId })
              .$promise
              .then(function () {
                return $state.go('root.apps.search');
              });
          }
        });
    };

    $scope.save = function save() {
      return $scope.appForm.$validate()
        .then(function () {
          if ($scope.appForm.$valid) {
            return Aops
              .save($stateParams, $scope.app)
              .$promise
              .then(function (data) {
                return $state.go('root.apps.edit', { id: data.aopApplicationId }, { reload: true });
              });
          }
        });
    };

    $scope.fastSave = function save() {
      return Aops
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
      return Aops.getProperDocTypeForApp({ type: type }).$promise.then(function (data) {
        if (data.docTypeId) {
          return $state.go('root.apps.edit.docSelect',
            { type: type, csDocTypeId: data.docTypeId, csIsChosen: 1 }, null, null);
        } else {
          return $state.go('root.apps.edit.docSelect', { type: type, csIsChosen: 1 }, null, null);
        }
      });
    };

    $scope.disconnectDoc = function (connections) {
      for (var i = 0; i < connections.length; i++) {
        $scope.app[connections[i]] = undefined;
      }

      return $scope.fastSave();
    };

    //ST
    $scope.generateSTChecklist = function (action) { //? merge in one with generateNDChecklist?
      return Aops
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
      return Aops
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
      return Aops
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
      return Aops
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
    'Aops',
    'scMessage',
    'app',
    'selectDoc'
  ];

  AppsEditCtrl.$resolve = {
    app: [
      '$stateParams',
      'Aops',
      function resolveApp($stateParams, Aops) {
        return Aops.get({ id: $stateParams.id }).$promise.then(function (app) {

          app.flags = {};
          app.flags.isVisibleEditCmd = app.canEdit;

          return app;
        });
      }
    ],
    selectDoc: [function () {
      return [];
    }]
  };

  angular.module('aop').controller('AppsEditCtrl', AppsEditCtrl);
}(angular));
