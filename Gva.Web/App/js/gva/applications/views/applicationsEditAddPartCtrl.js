/*global angular*/
(function (angular) {
  'use strict';

  function ApplicationsEditAddPartCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentId
    ) {

    $scope.cancel = function () {
      $scope.$parent.docFileType = null;
      return $state.go('applications/edit/case');
    };

    $scope.save = function () {
      return
      PersonDocumentId.save({ id: $stateParams.id }, $scope.personDocumentId).$promise
        .then(function () {

          return $state.go('applications/edit/case');
        });
    };
  }

  ApplicationsEditAddPartCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentId'
  ];

  angular.module('gva').controller('ApplicationsEditAddPartCtrl', ApplicationsEditAddPartCtrl);
}(angular
));
