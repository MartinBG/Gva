/*global angular*/
(function (angular) {
  'use strict';

  function DocumentChecksEditCtrl($scope, $state, $stateParams, PersonDocumentCheck) {
    PersonDocumentCheck
      .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
      .then(function (check) {
        $scope.personDocumentCheck = check;
      });

    $scope.save = function () {
      return PersonDocumentCheck
        .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentCheck).$promise
        .then(function () {
          return $state.go('persons.checks.search');
        });
    };

    $scope.cancel = function () {
      return $state.go('persons.checks.search');
    };
  }

  DocumentChecksEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentCheck'];

  angular.module('gva').controller('DocumentChecksEditCtrl', DocumentChecksEditCtrl);
}(angular));