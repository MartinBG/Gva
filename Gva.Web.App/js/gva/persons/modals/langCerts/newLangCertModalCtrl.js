/*global angular*/
(function (angular) {
  'use strict';

  function NewLangCertModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentLangCerts,
    scModalParams,
    newLangCert
  ) {
    $scope.form = {};
    $scope.newLangCert = newLangCert;
    $scope.caseTypeId = scModalParams.caseTypeId;
    $scope.lotId = scModalParams.lotId;

    $scope.save = function () {
      return $scope.form.newLangCertForm.$validate()
        .then(function () {
          if ($scope.form.newLangCertForm.$valid) {
            return PersonDocumentLangCerts
              .save({ id: $scope.lotId }, $scope.newLangCert)
              .$promise
              .then(function (savedLangCert) {
                return $modalInstance.close(savedLangCert);
              });
          }
        });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };
  }

  NewLangCertModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'PersonDocumentLangCerts',
    'scModalParams',
    'newLangCert'
  ];

  NewLangCertModalCtrl.$resolve = {
    newLangCert: [
      'PersonDocumentLangCerts',
      'scModalParams',
      function (PersonDocumentLangCerts, scModalParams) {
        return PersonDocumentLangCerts.newLangCert({
          id: scModalParams.lotId,
          appId: scModalParams.appId
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('NewLangCertModalCtrl', NewLangCertModalCtrl);
}(angular));
