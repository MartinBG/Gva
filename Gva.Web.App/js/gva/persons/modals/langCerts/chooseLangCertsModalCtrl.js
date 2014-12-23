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
      var includedLangCerts = _.filter(langCerts, function (langCert) {
        return _.contains(scModalParams.includedLangCerts, langCert.partIndex);
      });

      var certs = _.union($scope.selectedLangCerts, includedLangCerts);

      var bgLangCerts = _.filter(certs, function (cert) {
        return cert.part.documentRole.alias === 'bgCert';
      });
      var engLangCerts = _.filter(certs, function (cert) {
        return cert.part.documentRole.alias === 'engCert';
      });

      if(bgLangCerts.length > 1 || engLangCerts.length > 1) {
        $scope.showErrorMessage = true;
      } else {
        return $modalInstance.close($scope.selectedLangCerts);
      }
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.selectLangCert = function (event, langCert) {
      $scope.showErrorMessage = false;
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
