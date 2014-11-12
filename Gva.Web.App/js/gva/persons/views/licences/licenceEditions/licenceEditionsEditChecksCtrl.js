/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditChecksCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicenceEditions,
    PersonDocumentChecks,
    currentLicenceEdition,
    licenceEditions,
    scMessage,
    scModal
  ) {

    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.changeOrderMode = false;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;

    PersonDocumentChecks
      .query({ id: $stateParams.id })
      .$promise
      .then(function (checks) {
        $scope.includedChecks = 
          _.map($scope.currentLicenceEdition.part.includedChecks, function (check) {
            var includedCheck = _.where(checks, { partIndex: check.partIndex })[0];
            includedCheck.orderNum = check.orderNum;
            return includedCheck;
          });

        $scope.includedChecks = _.sortBy($scope.includedChecks, 'orderNum');
      });

    $scope.addCheck = function () {
      var modalInstance = scModal.open('newCheck', {
        lotId: $stateParams.id,
        caseTypeId: $stateParams.caseTypeId,
        appId: $stateParams.appId
      });

      modalInstance.result.then(function (newCheck) {
        var lastOrderNum = 0,
          lastCheck = _.last($scope.includedChecks);
        if (lastCheck) {
          lastOrderNum = _.last($scope.includedChecks).orderNum;
        }

        newCheck.orderNum = ++lastOrderNum;
        $scope.includedChecks.push(newCheck);

        $scope.currentLicenceEdition.part.includedChecks =
          _.map($scope.includedChecks, function(check) {
            return {
              orderNum: check.orderNum,
              partIndex: check.partIndex
            };
          });
        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.addExistingCheck = function () {
      var modalInstance = scModal.open('chooseChecks', {
        includedChecks: _.pluck($scope.currentLicenceEdition.part.includedChecks, 'partIndex'),
        lotId: $stateParams.id
      });

      modalInstance.result.then(function (selectedChecks) {
        var lastOrderNum = 0,
          lastCheck = _.last($scope.includedChecks);
        if (lastCheck) {
          lastOrderNum = _.last($scope.includedChecks).orderNum;
        }

        _.forEach(selectedChecks, function(check) {
          var newlyAddedCheck = {
            orderNum: ++lastOrderNum,
            partIndex: check.partIndex
          };
          $scope.currentLicenceEdition.part.includedChecks.push(newlyAddedCheck);

          check.orderNum = newlyAddedCheck.orderNum;
          $scope.includedChecks.push(check);
        });

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
                function(includedCheck) {
                  return check.partIndex === includedCheck.partIndex;
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
      $scope.currentLicenceEdition.part.includedChecks = [];
      _.forEach($scope.includedChecks, function (check) {
        var changedCheck = {
          orderNum: check.orderNum,
          partIndex: check.partIndex
        };
        $scope.currentLicenceEdition.part.includedChecks.push(changedCheck);
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

  LicenceEditionsEditChecksCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonLicenceEditions',
    'PersonDocumentChecks',
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage',
    'scModal'
  ];


  angular.module('gva')
    .controller('LicenceEditionsEditChecksCtrl', LicenceEditionsEditChecksCtrl);
}(angular, _));
