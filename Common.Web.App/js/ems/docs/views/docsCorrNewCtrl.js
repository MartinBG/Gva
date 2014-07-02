/*global angular*/
(function (angular) {
  'use strict';

  function DocsCorrNewCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    Corrs,
    corr,
    doc
  ) {
    $scope.corr = corr;
    $scope.doc = doc;

    $scope.save = function save() {
      return $scope.corrForm.$validate()
        .then(function () {
          if ($scope.corrForm.$valid) {
            return Corrs.save($scope.corr).$promise.then(function (corr) {
              doc.docCorrespondents.push({
                name: corr.obj.displayName,
                nomValueId: corr.correspondentId
              });
              return $state.go('^');
            });
          }
        });
    };

    $scope.cancel = function cancel() {
      return $state.go('^');
    };
  }

  DocsCorrNewCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Corrs',
    'corr',
    'doc'
  ];

  DocsCorrNewCtrl.$resolve = {
    corr: [
      '$stateParams',
      'Corrs',
      function resolveCorr($stateParams, Corrs) {
        return Corrs.getNew().$promise;
      }
    ]
  };

  angular.module('ems').controller('DocsCorrNewCtrl', DocsCorrNewCtrl);
}(angular));
