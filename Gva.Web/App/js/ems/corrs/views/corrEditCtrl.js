/*global angular*/
(function (angular) {
  'use strict';

  function CorrsEditCtrl(
    $q,
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

      Corr.create().$promise
        .then(function (result) {
          $scope.corr = result;
        });

    }
    $scope.saveClicked = false;

    $scope.removeCorrContact = function (target) {
      var index = $scope.corr.correspondentContacts.indexOf(target);

      if (index > -1) {
        $scope.corr.correspondentContacts.splice(index, 1);
      }
    };

    $scope.addCorrContact = function () {
      Corr.contact($stateParams)
        .$promise
        .then(function (res) {
          $scope.corr.correspondentContacts.push(res);
        });
    };

    $scope.save = function () {
      $scope.saveClicked = true;

      if ($scope.corrForm.$valid) {
        $scope.corr.$save($stateParams).then(function () {
          $state.go('corrs/search');
        });
      }
    };

    $scope.cancel = function () {
      $state.go('corrs/search');
    };
  }

  CorrsEditCtrl.$inject = [
    '$q',
    '$scope',
    '$filter',
    '$state',
    '$stateParams',
    'Corr'
  ];

  angular.module('ems').controller('CorrsEditCtrl', CorrsEditCtrl);
}(angular));
