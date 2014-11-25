/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditLangCertsCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicenceEditions,
    includedLangCerts,
    currentLicenceEdition,
    licenceEditions,
    scMessage,
    scModal
  ) {

    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;
    $scope.currentLicenceEdition.part.includedLangCerts =
      $scope.currentLicenceEdition.part.includedLangCerts || [];
    $scope.includedLangCerts = includedLangCerts;

    $scope.addLangCert = function () {
      var modalInstance = scModal.open('newLangCert', {
        lotId: $stateParams.id,
        caseTypeId: $stateParams.caseTypeId,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newLangCert) {
        $scope.includedLangCerts.push(newLangCert);
        $scope.currentLicenceEdition.part.includedLangCerts.push(newLangCert.partIndex);
        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.addExistingLangCert = function () {
      var modalInstance = scModal.open('chooseLangCerts', {
        includedLangCerts: $scope.currentLicenceEdition.part.includedLangCerts,
        lotId: $stateParams.id
      });

      modalInstance.result.then(function (selectedLangCerts) {
        $scope.includedLangCerts = $scope.includedLangCerts.concat(selectedLangCerts);

        $scope.currentLicenceEdition.part.includedLangCerts = 
          $scope.currentLicenceEdition.part.includedLangCerts
          .concat(_.pluck(selectedLangCerts, 'partIndex'));

        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.removeLangCert = function (langCert) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedLangCerts = _.without($scope.includedLangCerts, langCert);

            _.remove($scope.currentLicenceEdition.part.includedLangCerts,
              function(includedLangCertsPartIndex) {
                return langCert.partIndex === includedLangCertsPartIndex;
              });
            $scope.save();
          }
        });
    };

    $scope.changeOrder = function () {
      $scope.changeOrderMode = true;
    };

    $scope.saveOrder = function () {
      $scope.includedLangCerts = _.sortBy($scope.includedLangCerts, 'orderNum');
      $scope.changeOrderMode = false;
      $scope.currentLicenceEdition.part.includedLangCerts =
        _.pluck($scope.includedLangCerts, 'partIndex');
      return $scope.save();
    }; 

    $scope.cancelChangeOrder = function () {
      $scope.changeOrderMode = false;
    };

    $scope.save = function () {
      return PersonLicenceEditions
        .save({
          id: $stateParams.id,
          ind: $stateParams.ind,
          index: $stateParams.index,
          caseTypeId: $scope.caseTypeId
        }, $scope.currentLicenceEdition)
        .$promise;
    };
  }

  LicenceEditionsEditLangCertsCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonLicenceEditions',
    'includedLangCerts',
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage',
    'scModal'
  ];

  LicenceEditionsEditLangCertsCtrl.$resolve = {
    includedLangCerts: [
      '$stateParams',
      'PersonDocumentLangCerts',
      'currentLicenceEdition',
      function ($stateParams, PersonDocumentLangCerts, currentLicenceEdition) {
        return  PersonDocumentLangCerts
          .query({ id: $stateParams.id })
          .$promise
          .then(function (langCerts) {
            return _.map(currentLicenceEdition.part.includedLangCerts, function (partIndex) {
              return _.where(langCerts, { partIndex: partIndex })[0];
            });
          });
      }
    ]
  };
  angular.module('gva')
    .controller('LicenceEditionsEditLangCertsCtrl', LicenceEditionsEditLangCertsCtrl);
}(angular, _));
