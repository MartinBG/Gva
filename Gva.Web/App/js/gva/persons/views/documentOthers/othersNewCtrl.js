/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOthersNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentOther,
    personDocumentOther,
    selectedPublisher
  ) {
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

    $scope.personDocumentOther = personDocumentOther;
    $scope.personDocumentOther.part.documentPublisher = selectedPublisher.pop() ||
      personDocumentOther.part.documentPublisher;

    $scope.choosePublisher = function () {
      return $state.go('root.persons.view.documentOthers.new.choosePublisher');
    };

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentOthers.search');
    };
  }

  DocumentOthersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentOther',
    'personDocumentOther',
    'selectedPublisher'
  ];

  DocumentOthersNewCtrl.$resolve = {
    personDocumentOther: function () {
      return {
        part: {},
        files: []
      };
    },
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentOthersNewCtrl', DocumentOthersNewCtrl);
}(angular));