/*global angular, _*/
(function (angular) {
  'use strict';

  function ChooseCorrViewCtrl(
    $state,
    $stateParams,
    $scope,
    Corr
  ) {
    $scope.filters = {
      displayName: null,
      email: null
    };

    _.forOwn($stateParams, function (value, param) {
      if (value !== null && value !== undefined) {
        $scope.filters[param] = value;
      }
    });

    $scope.search = function () {
      $state.go('root.docs.edit.chooseCorr', {
        displayName: $scope.filters.displayName,
        email: $scope.filters.email
      });
    };

    Corr.query($stateParams).$promise.then(function (corrs) {
      $scope.corrs = corrs.map(function (corr) {
        return {
          corrId: corr.corrId,
          displayName: corr.displayName,
          email: corr.email,
          correspondentType: corr.correspondentType
        };
      });
    });

    $scope.selectCorr = function (corr) {
      var nomItem = {
        nomTypeValueId: corr.corrId,
        name: corr.displayName,
        content: corr
      };

      //todo return function
      $scope.doc.docCorrespondents.push(nomItem);
      //todo goto previous state
      $state.go('root.docs.edit.addressing');
    };

    $scope.goBack = function () {
      //todo goto previous state
      $state.go('root.docs.edit.addressing');
    };

  }

  ChooseCorrViewCtrl.$inject = [
    '$state',
    '$stateParams',
    '$scope',
    'Corr'
  ];

  angular.module('ems').controller('ChooseCorrViewCtrl', ChooseCorrViewCtrl);
}(angular, _));
