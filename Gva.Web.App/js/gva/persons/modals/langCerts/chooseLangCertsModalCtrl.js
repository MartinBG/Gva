/*global angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function ChooseLangCertsModalCtrl(
    $scope,
    $modalInstance,
    PersonDocumentLangCerts,
    scModalParams,
    langCerts
  ) {
    $scope.selectedLangCerts = [];

    $scope.filterLangCerts = function (langCerts) {
      return _.filter(langCerts, function (langCert) {
        return !_.contains(scModalParams.includedLangCerts, langCert.partIndex);
      });
    };

    $scope.langCerts = $scope.filterLangCerts(langCerts);

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

    $scope.showAllLangCerts = function (event) {
      PersonDocumentLangCerts.getLangCertsByValidity({ 
        id: scModalParams.lotId,
        valid: !$(event.target).is(':checked'),
        caseTypeId: scModalParams.caseTypeId
      }).$promise.then(function (allLangCerts) {
        $scope.langCerts =  $scope.filterLangCerts(allLangCerts);
      });
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
    'PersonDocumentLangCerts',
    'scModalParams',
    'langCerts'
  ];

  ChooseLangCertsModalCtrl.$resolve = {
    langCerts: [
      'PersonDocumentLangCerts',
      'scModalParams',
      function (PersonDocumentLangCerts, scModalParams) {
        return PersonDocumentLangCerts.getLangCertsByValidity({
          id: scModalParams.lotId
        })
        .$promise
        .then(function (langCerts){
          return _.filter(langCerts, function (langCert) {
            return langCert['case'].caseType.nomValueId === scModalParams.caseTypeId ||
              langCert['case'].caseType.alias === 'person';
          });
        });
      }
    ]
  };

  angular.module('gva').controller('ChooseLangCertsModalCtrl', ChooseLangCertsModalCtrl);
}(angular, _, $));
