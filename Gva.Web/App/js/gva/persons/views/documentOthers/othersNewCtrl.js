/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOthersNewCtrl($scope, $state, $stateParams, PersonDocumentOther) {
    $scope.save = function () {
      $scope.personDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.personDocumentOtherForm.$valid) {
            return PersonDocumentOther
              .save({ id: $stateParams.id }, $scope.personDocumentOther).$promise
              .then(function () {
                return $state.go('root.persons.view.documentOthers.search');
              });
          }
        });
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentOthers.search');
    };
  }

  DocumentOthersNewCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentOther'];

  angular.module('gva').controller('DocumentOthersNewCtrl', DocumentOthersNewCtrl);
}(angular));