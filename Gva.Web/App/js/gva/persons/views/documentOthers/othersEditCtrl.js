/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOthersEditCtrl($scope, $state, $stateParams, PersonDocumentOther) {
    PersonDocumentOther
      .get({ id: $stateParams.id, ind: $stateParams.ind }).$promise
      .then(function (docOther) {
        $scope.personDocumentOther = docOther;
      });

    $scope.save = function () {
      $scope.personDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.personDocumentOtherForm.$valid) {
            return PersonDocumentOther
              .save({ id: $stateParams.id, ind: $stateParams.ind }, $scope.personDocumentOther)
              .$promise
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

  DocumentOthersEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'PersonDocumentOther'];

  angular.module('gva').controller('DocumentOthersEditCtrl', DocumentOthersEditCtrl);
}(angular));