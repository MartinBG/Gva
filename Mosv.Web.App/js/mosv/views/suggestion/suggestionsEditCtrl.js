/*global angular,_*/
(function (angular) {
  'use strict';

  function SuggestionEditCtrl(
    $scope,
    $state,
    $stateParams,
    SuggestionData,
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
            return SuggestionData
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
    'SuggestionData',
    'suggestion'
  ];

  SuggestionEditCtrl.$resolve = {
    suggestion: [
      '$stateParams',
      'SuggestionData',
      function ($stateParams, SuggestionData) {
        return SuggestionData.get({ id: $stateParams.id }).$promise;
      }
    ]
  };

  angular.module('mosv').controller('SuggestionEditCtrl', SuggestionEditCtrl);
}(angular));
