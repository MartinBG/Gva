/*global angular*/
(function (angular) {
  'use strict';

  function CorrsEditCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    Corr,
    corr
  ) {
    if ($stateParams.corrId) {
      $scope.isEdit = true;
      $scope.corr = corr;
    } else {
      $scope.isEdit = false;
      $scope.corr = {};
    }

    $scope.removeCorrContact = function removeCorrContact(target) {
      var index = $scope.corr.correspondentContacts.indexOf(target);

      if (index > -1) {
        $scope.corr.correspondentContacts.splice(index, 1);
      }
    };

    $scope.addCorrContact = function addCorrContact(corrId) {
      var correspondentContact = {
        correspondentContactId: null,
        corrId: corrId,
        name: undefined,
        uin: undefined,
        note: undefined
      };

      $scope.corr.correspondentContacts = $scope.corr.correspondentContacts || [];
      $scope.corr.correspondentContacts.push(correspondentContact);
    };

    $scope.save = function save() {
      $scope.corrForm.$validate()
        .then(function () {
          if ($scope.corrForm.$valid) {
            Corr.save($scope.corr).$promise
              .then(function () {
                return $state.go('root.corrs.search');
              });
          }
        });
    };

    $scope.cancel = function cancel() {
      return $state.go('root.corrs.search');
    };
  }

  CorrsEditCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Corr',
    'corr'
  ];

  CorrsEditCtrl.$resolve = {
    corr: [
      '$stateParams',
      'Corr',
      function resolveCorr($stateParams, Corr) {
        if ($stateParams.corrId) {
          return Corr.get({ corrId: $stateParams.corrId }).$promise;
        }

        return;
      }
    ]
  };

  angular.module('ems').controller('CorrsEditCtrl', CorrsEditCtrl);
}(angular));
