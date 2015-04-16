/*global angular*/
(function (angular) {
  'use strict';

  function NewCheckModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentChecks,
    scModalParams,
    personDocumentCheck
  ) {
    $scope.form = {};
    $scope.personDocumentCheck = personDocumentCheck;
    $scope.personDocumentCheck.part.documentNumber = scModalParams.documentNumber;
    $scope.personDocumentCheck.part.documentPersonNumber = scModalParams.lastGroupNumber;
    
    $scope.lotId = scModalParams.lotId;
    $scope.appId = scModalParams.appId;
    $scope.caseTypeId = scModalParams.caseTypeId;
    $scope.publisher = scModalParams.publisher;

    $scope.save = function () {
      return $scope.form.newDocumentCheckForm.$validate()
        .then(function () {
          if ($scope.form.newDocumentCheckForm.$valid) {
            return PersonDocumentChecks
              .save({ id: $scope.lotId }, $scope.personDocumentCheck)
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
    'scModalParams',
    'personDocumentCheck'
  ];

  NewCheckModalCtrl.$resolve = {
    personDocumentCheck: [
      'PersonDocumentChecks',
      'scModalParams',
      function (PersonDocumentChecks, scModalParams) {
        return PersonDocumentChecks.newCheck({
          id: scModalParams.lotId,
          caseTypeId: scModalParams.caseTypeId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('NewCheckModalCtrl', NewCheckModalCtrl);
}(angular));
