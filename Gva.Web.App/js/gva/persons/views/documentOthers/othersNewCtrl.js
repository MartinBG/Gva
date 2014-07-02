/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOthersNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentOthers,
    personDocumentOther,
    selectedPublisher
  ) {
    $scope.save = function () {
      return $scope.newDocumentOtherForm.$validate()
        .then(function () {
          if ($scope.newDocumentOtherForm.$valid) {
            return PersonDocumentOthers
              .save({ id: $stateParams.id }, $scope.personDocumentOther)
              .$promise
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
    'PersonDocumentOthers',
    'personDocumentOther',
    'selectedPublisher'
  ];

  DocumentOthersNewCtrl.$resolve = {
    personDocumentOther: [
      'application',
      function (application) {
        if (application) {
          return {
            part: {},
            files: [{ isAdded: true, applications: [application] }]
          };
        }
        else {
          return {
            part: {},
            files: []
          };
        }
      }
    ],
    selectedPublisher: function () {
      return [];
    }
  };

  angular.module('gva').controller('DocumentOthersNewCtrl', DocumentOthersNewCtrl);
}(angular));
