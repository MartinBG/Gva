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
    var corrExistsPromise;

    if ($stateParams.corrId) {
      $scope.isEdit = true;
      $scope.corr = Corr.get({ corrId: $stateParams.corrId });
    } else {
      $scope.isEdit = false;
      $scope.corr = new Corr();
      $scope.corr.$promise = $q.when($scope.corr);
    }
    //$scope.roles = Role.query();
    $scope.saveClicked = false;

    //$q.all({
    //  user: $scope.user.$promise,
    //  roles: $scope.roles.$promise
    //}).then(function (res) {
    //  res.roles.forEach(function (role) {
    //    role.selected =
    //      $filter('filter')(res.user.roles || [], {roleId: role.roleId}).length > 0;
    //  });

    //  $scope.setPassword = res.user.hasPassword;
    //  $scope.password = '';
    //  $scope.confirmPassword = '';
    //  $scope.setCertificate = !!res.user.certificateThumbprint;
    //  $scope.certificate = res.user.certificateThumbprint;
    //});

    $scope.save = function () {
      $scope.saveClicked = true;

      if ($scope.corrForm.$valid) {
        if ($scope.isEdit) {
          corrExistsPromise = $q.when(false);
        } else {
          corrExistsPromise =
            Corr.query({ corrUin: $scope.corr.corrUin })
            .$promise
            .then(function (corrs) {
              return corrs.length > 0;
            });
        }

        corrExistsPromise.then(function (exists) {
          $scope.corrExists = exists;
          if (!exists) {
            $scope.corr.$save().then(function () {
              $state.go('corrs.search');
            });
          }
        });
      }
    };

    $scope.cancel = function () {
      $state.go('corrs.search');
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

  angular.module('corrs').controller('CorrsEditCtrl', CorrsEditCtrl);
}(angular));
