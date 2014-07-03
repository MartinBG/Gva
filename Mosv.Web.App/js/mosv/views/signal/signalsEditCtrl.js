/*global angular,_*/
(function (angular) {
  'use strict';

  function SignalsEditCtrl(
    $scope,
    $state,
    $stateParams,
    Signals,
    SignalsData,
    signalData,
    selectDoc
  ) {
    var originalSignalData = _.cloneDeep(signalData.partData);

    $scope.signalData = signalData.partData;
    $scope.data = signalData.data;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.signalData = _.cloneDeep(originalSignalData);
    };

    $scope.save = function () {
      return $scope.editSignalForm.$validate()
        .then(function () {
          if ($scope.editSignalForm.$valid) {
            return SignalsData
              .save({ id: $stateParams.id }, $scope.signalData)
              .$promise
              .then(function () {
                return $state.transitionTo('root.signals.edit', $stateParams, { reload: true });
              });
          }
        });
    };

    $scope.connectToDoc = function () {
      return $state.go('root.signals.edit.docSelect');
    };

    $scope.disconnectDoc = function () {
      $scope.data.applicationDocId = undefined;

      return $scope.fastSave();
    };

    $scope.loadData = function () {
      return Signals
        .loadData({ id: $stateParams.id }, {})
        .$promise
        .then(function (data) {
          return $state.go('root.signals.edit', { id: data.id }, { reload: true });
        });
    };

    $scope.fastSave = function () {
      return Signals
        .fastSave({ id: $stateParams.id }, $scope.data)
        .$promise
        .then(function (data) {
          return $state.go('root.signals.edit', { id: data.id }, { reload: true });
        });
    };

    if (selectDoc.length > 0) {
      var sd = selectDoc.pop();

      $scope.data.applicationDocId = sd.docId;

      return $scope.fastSave();
    }
  }

  SignalsEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Signals',
    'SignalsData',
    'signalData',
    'selectDoc'
  ];

  SignalsEditCtrl.$resolve = {
    signalData: [
      '$stateParams',
      'SignalsData',
      function ($stateParams, SignalsData) {
        return SignalsData.get({ id: $stateParams.id }).$promise;
      }
    ],
    selectDoc: [function () {
      return [];
    }]
  };

  angular.module('mosv').controller('SignalsEditCtrl', SignalsEditCtrl);
}(angular));
