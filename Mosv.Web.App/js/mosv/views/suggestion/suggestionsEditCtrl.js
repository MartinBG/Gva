/*global angular,_*/
(function (angular) {
  'use strict';

  function SuggestionEditCtrl(
    $scope,
    $state,
    $stateParams,
    Suggestions,
    SuggestionsData,
    suggestion,
    selectDoc
  ) {
    var originalSuggestion = _.cloneDeep(suggestion.partData);

    $scope.suggestion = suggestion.partData;
    $scope.data = suggestion.data;
    $scope.editMode = null;

    $scope.edit = function () {
      $scope.editMode = 'edit';
    };

    $scope.cancel = function () {
      $scope.editMode = null;
      $scope.suggestion = _.cloneDeep(originalSuggestion);
    };

    $scope.save = function () {
      return $scope.editSuggestionDataForm.$validate()
        .then(function () {
          if ($scope.editSuggestionDataForm.$valid) {
            return SuggestionsData
              .save({ id: $stateParams.id }, $scope.suggestion)
              .$promise
              .then(function () {
                return $state.transitionTo(
                  'root.suggestions.search',
                  $stateParams,
                  { reload: true }
                );
              });
          }
        });
    };

    $scope.connectToDoc = function () {
      return $state.go('root.suggestions.edit.docSelect');
    };

    $scope.disconnectDoc = function () {
      $scope.data.applicationDocId = undefined;

      return $scope.fastSave();
    };

    $scope.loadData = function () {
      return Suggestions
        .loadData({ id: $stateParams.id }, {})
        .$promise
        .then(function (data) {
          return $state.go('root.suggestions.edit', { id: data.id }, { reload: true });
        });
    };

    $scope.fastSave = function () {
      return Suggestions
        .fastSave({ id: $stateParams.id }, $scope.data)
        .$promise
        .then(function (data) {
          return $state.go('root.suggestions.edit', { id: data.id }, { reload: true });
        });
    };

    if (selectDoc.length > 0) {
      var sd = selectDoc.pop();

      $scope.data.applicationDocId = sd.docId;

      return $scope.fastSave();
    }
  }

  SuggestionEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'Suggestions',
    'SuggestionsData',
    'suggestion',
    'selectDoc'
  ];

  SuggestionEditCtrl.$resolve = {
    suggestion: [
      '$stateParams',
      'SuggestionsData',
      function ($stateParams, SuggestionsData) {
        return SuggestionsData.get({ id: $stateParams.id }).$promise;
      }
    ],
    selectDoc: [function () {
      return [];
    }]
  };

  angular.module('mosv').controller('SuggestionEditCtrl', SuggestionEditCtrl);
}(angular));
