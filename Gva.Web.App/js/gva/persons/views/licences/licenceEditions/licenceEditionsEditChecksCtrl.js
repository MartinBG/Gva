/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditChecksCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicenceEditions,
    PersonDocumentChecks,
    includedChecks,
    currentLicenceEdition,
    licenceEditions,
    scMessage,
    scModal
  ) {

    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.changeOrderMode = false;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;
    $scope.currentLicenceEdition.part.includedChecks =
      $scope.currentLicenceEdition.part.includedChecks || [];
    $scope.includedChecks = includedChecks;

    $scope.addCheck = function () {
      var modalInstance = scModal.open('newCheck', {
        lotId: $stateParams.id,
        caseTypeId: $scope.currentLicenceEdition.cases[0].caseType.nomValueId,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newCheck) {
        $scope.currentLicenceEdition.part.includedChecks.push(newCheck.partIndex);
        $scope.save();
        PersonDocumentChecks.getCheckView({
          id: $stateParams.id,
          ind: newCheck.partIndex
        }).$promise
          .then(function(check) {
            $scope.includedChecks.push(check);
          });
      });

      return modalInstance.opened;
    };

    $scope.addExistingCheck = function () {
      var modalInstance = scModal.open('chooseChecks', {
        includedChecks: $scope.currentLicenceEdition.part.includedChecks,
        lotId: $stateParams.id,
        caseTypeId: $scope.currentLicenceEdition.cases[0].caseType.nomValueId
      });

      modalInstance.result.then(function (selectedChecks) {
        $scope.includedChecks = $scope.includedChecks.concat(selectedChecks);

        $scope.currentLicenceEdition.part.includedChecks = 
          $scope.currentLicenceEdition.part.includedChecks
          .concat(_.pluck(selectedChecks, 'partIndex'));

        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.removeCheck = function (check) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedChecks = _.without($scope.includedChecks, check);

            _.remove($scope.currentLicenceEdition.part.includedChecks,
              function(includedCheckPartIndex) {
                return check.partIndex === includedCheckPartIndex;
              });
            $scope.save();
          }
        });
    };

    $scope.changeOrder = function () {
      $scope.changeOrderMode = true;
    };

    $scope.saveOrder = function () {
      $scope.includedChecks = _.sortBy($scope.includedChecks, 'orderNum');
      $scope.changeOrderMode = false;
      $scope.currentLicenceEdition.part.includedChecks =
        _.pluck($scope.includedChecks, 'partIndex');
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

  LicenceEditionsEditChecksCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonLicenceEditions',
    'PersonDocumentChecks',
    'includedChecks',
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage',
    'scModal'
  ];

  LicenceEditionsEditChecksCtrl.$resolve = {
    includedChecks: [
      '$stateParams',
      'PersonDocumentChecks',
      'currentLicenceEdition',
      function ($stateParams, PersonDocumentChecks, currentLicenceEdition) {
        return  PersonDocumentChecks
          .query({ id: $stateParams.id })
          .$promise
          .then(function (checks) {
            return _.map(currentLicenceEdition.part.includedChecks, function (partIndex) {
              return _.where(checks, { partIndex: partIndex })[0];
            });
          });
       }
    ]
  };

  angular.module('gva')
    .controller('LicenceEditionsEditChecksCtrl', LicenceEditionsEditChecksCtrl);
}(angular, _));
