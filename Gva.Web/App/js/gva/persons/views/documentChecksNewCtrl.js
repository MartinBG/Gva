/*global angular*/
(function (angular) {
  'use strict';

  function DocumentChecksNewCtrl($scope, $state, $stateParams, PersonDocumentCheck) {
    $scope.isEdit = false;
    $scope.save = function () {
      $scope.personDocumentCheckForm.$validate()
         .then(function () {
            if ($scope.personDocumentCheckForm.$valid) {
              return PersonDocumentCheck
               .save({ id: $stateParams.id }, $scope.personDocumentCheck).$promise
               .then(function () {
                  return $state.go('root.persons.view.checks.search');
                });
            }
          });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.checks.search');
    };
  }

  DocumentChecksNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentCheck'];

  angular.module('gva').controller('DocumentChecksNewCtrl', DocumentChecksNewCtrl);
}(angular));
