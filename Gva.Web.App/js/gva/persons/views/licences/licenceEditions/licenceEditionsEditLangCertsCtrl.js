/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditLangCertsCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicenceEditions,
    PersonDocumentLangCerts,
    currentLicenceEdition,
    licenceEditions,
    scMessage,
    scModal
  ) {

    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;

    PersonDocumentLangCerts
      .query({ id: $stateParams.id })
      .$promise
      .then(function (langCerts) {
        $scope.includedLangCerts = 
          _.map($scope.currentLicenceEdition.part.includedLangCerts, function (cert) {
            var includedLangCert = _.where(langCerts, { partIndex: cert.partIndex })[0];
            includedLangCert.orderNum = cert.orderNum;
            return includedLangCert;
          });
        $scope.includedLangCerts = _.sortBy($scope.includedLangCerts, 'orderNum');
      });

    $scope.addLangCert = function () {
      var modalInstance = scModal.open('newLangCert', {
        lotId: $stateParams.id,
        caseTypeId: $stateParams.caseTypeId,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newLangCert) {
        var lastOrderNum = 0,
          lastExam = _.last($scope.includedLangCerts);
        if (lastExam) {
          lastOrderNum = _.last($scope.includedLangCerts).orderNum;
        }

        newLangCert.orderNum = ++lastOrderNum;
        $scope.includedLangCerts.push(newLangCert);

        $scope.currentLicenceEdition.part.includedLangCerts =
          _.map($scope.includedLangCerts, function(langCert) {
            return {
              orderNum: langCert.orderNum,
              partIndex: langCert.partIndex
            };
          });
        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.addExistingLangCert = function () {
      var modalInstance = scModal.open('chooseLangCerts', {
        includedLangCerts:
          _.pluck($scope.currentLicenceEdition.part.includedLangCerts, 'partIndex'),
        lotId: $stateParams.id
      });

      modalInstance.result.then(function (selectedLangCerts) {
        var lastOrderNum = 0,
          lastCert = _.last($scope.includedLangCerts);
        if (lastCert) {
          lastOrderNum = _.last($scope.includedLangCerts).orderNum;
        }

        _.forEach(selectedLangCerts, function(langCert) {
          var newlyAddedCertLang = {
            orderNum: ++lastOrderNum,
            partIndex: langCert.partIndex
          };
          $scope.currentLicenceEdition.part.includedLangCerts.push(newlyAddedCertLang);

          langCert.orderNum = newlyAddedCertLang.orderNum;
          $scope.includedLangCerts.push(langCert);
        });

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
              function(includedLangCert) {
                return langCert.partIndex === includedLangCert.partIndex;
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
      $scope.currentLicenceEdition.part.includedLangCerts = [];
      _.forEach($scope.includedLangCerts, function (cert) {
        var changedCert = {
          orderNum: cert.orderNum,
          partIndex: cert.partIndex
        };
        $scope.currentLicenceEdition.part.includedLangCerts.push(changedCert);
      });
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
    'PersonDocumentLangCerts',
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage',
    'scModal'
  ];


  angular.module('gva')
    .controller('LicenceEditionsEditLangCertsCtrl', LicenceEditionsEditLangCertsCtrl);
}(angular, _));
