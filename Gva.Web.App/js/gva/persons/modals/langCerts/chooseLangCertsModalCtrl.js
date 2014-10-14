/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseLangCertsModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    langCerts
  ) {
    $scope.selectedLangCerts = [];

    $scope.langCerts = _.filter(langCerts, function (langCert) {
      return !_.contains(scModalParams.includedLangCerts, langCert.partIndex);
    });

    $scope.addLangCerts = function () {
      return $modalInstance.close($scope.selectedLangCerts);
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectLangCert = function (event, langCert) {
      if ($(event.target).is(':checked')) {
        $scope.selectedLangCerts.push(langCert);
      }
      else {
        $scope.selectedLangCerts = _.without($scope.selectedLangCerts, langCert);
      }
    };
  }

  ChooseLangCertsModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'langCerts'
  ];

  ChooseLangCertsModalCtrl.$resolve = {
    langCerts: [
      'PersonDocumentLangCerts',
      'scModalParams',
      function (PersonDocumentLangCerts, scModalParams) {
        return PersonDocumentLangCerts.query({ id: scModalParams.lotId }).$promise;
      }
    ]
  };

  angular.module('gva').controller('ChooseLangCertsModalCtrl', ChooseLangCertsModalCtrl);
}(angular, _, $));
