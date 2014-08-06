/*global angular*/
(function (angular) {
  'use strict';

  function DocumentOthersNewCtrl(
    $scope,
    $state,
    $stateParams,
    PersonDocumentOthers,
    personDocumentOther
  ) {
    $scope.personDocumentOther = personDocumentOther;
    $scope.lotId = $stateParams.id;
    $scope.caseTypeId = $stateParams.caseTypeId;

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

    $scope.cancel = function () {
      return $state.go('root.persons.view.documentOthers.search');
    };
  }

  DocumentOthersNewCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonDocumentOthers',
    'personDocumentOther'
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
    ]
  };

  angular.module('gva').controller('DocumentOthersNewCtrl', DocumentOthersNewCtrl);
}(angular));
