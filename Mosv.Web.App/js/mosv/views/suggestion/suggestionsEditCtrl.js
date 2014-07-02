/*global angular,_*/
(function (angular) {
  'use strict';

  function SuggestionEditCtrl(
    $scope,
    $state,
    $stateParams,
    SuggestionsData,
    suggestion
  ) {
    var originalSuggestion = _.cloneDeep(suggestion);

    $scope.suggestion = suggestion;
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
  }

  SuggestionEditCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'SuggestionsData',
    'suggestion'
  ];

  SuggestionEditCtrl.$resolve = {
    suggestion: [
      '$stateParams',
      'SuggestionsData',
      function ($stateParams, SuggestionsData) {
        return SuggestionsData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('mosv').controller('SuggestionEditCtrl', SuggestionEditCtrl);
}(angular));
