/*global angular*/
(function (angular) {
  'use strict';

  function CorrsEditCtrl(
    $scope,
    $filter,
    $state,
    $stateParams,
    Corr
  ) {
    if ($stateParams.corrId) {
      $scope.isEdit = true;
      $scope.corr = Corr.get({ corrId: $stateParams.corrId });
    } else {
      $scope.isEdit = false;
      $scope.corr = {};
    }

    $scope.removeCorrContact = function (target) {
      var index = $scope.corr.correspondentContacts.indexOf(target);

      if (index > -1) {
        $scope.corr.correspondentContacts.splice(index, 1);
      }
    };

    $scope.addCorrContact = function (corrId) {
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

    $scope.save = function () {
      $scope.corrForm.$validate()
        .then(function () {
          if ($scope.corrForm.$valid) {
            Corr.save($scope.corr).$promise
              .then(function () {
                $state.go('root.corrs.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      $state.go('root.corrs.search');
    };
  }

  CorrsEditCtrl.$inject = [
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Corr'
  ];

  angular.module('ems').controller('CorrsEditCtrl', CorrsEditCtrl);
}(angular));
