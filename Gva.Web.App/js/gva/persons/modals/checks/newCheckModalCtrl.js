/*global angular*/
(function (angular) {
  'use strict';

  function NewCheckModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentChecks,
    personDocumentCheck,
    lotId
  ) {
    $scope.form = {};
    $scope.personDocumentCheck = personDocumentCheck;

    $scope.save = function () {
      return $scope.form.newDocumentCheckForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentCheckForm.$valid) {
            return PersonDocumentChecks
              .save({ id: lotId }, $scope.personDocumentCheck)
              .$promise
              .then(function (savedCheck) {
                return $modalInstance.close(savedCheck);
              });
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewCheckModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonDocumentChecks',
    'personDocumentCheck',
    'lotId'
  ];

  NewCheckModalCtrl.$resolve = {
    personDocumentCheck: function () {
      return {
        part: {},
        files: []
      };
    }
  };

  angular.module('gva').controller('NewCheckModalCtrl', NewCheckModalCtrl);
}(angular));
