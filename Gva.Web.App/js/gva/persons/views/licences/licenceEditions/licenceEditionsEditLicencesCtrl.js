/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditLicencesCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicences,
    PersonLicenceEditions,
    currentLicenceEdition,
    licenceEditions,
    scMessage,
    scModal
  ) {

    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;

    PersonLicences
      .query({ id: $stateParams.id })
      .$promise
      .then(function (licences) {
        $scope.includedLicences = 
          _.map($scope.currentLicenceEdition.part.includedLicences, function (licence) {
            var includedLicence = _.where(licences, { partIndex: licence.partIndex })[0];
            includedLicence.orderNum = licence.orderNum;
            return includedLicence;
          });
        $scope.includedLicences = _.sortBy($scope.includedLicences, 'orderNum');
      });

    $scope.addExistingLicence = function () {
      var hideLicences = _.clone($scope.currentLicenceEdition.part.includedLicences);

      if ($stateParams.partIndex) {
        hideLicences.push(parseInt($stateParams.partIndex, 10));
      }

      var modalInstance = scModal.open('chooseLicences', {
        includedLicences: _.pluck(hideLicences, 'partIndex'),
        lotId: $stateParams.id
      });

       modalInstance.result.then(function (selectedLicences) {
        var lastOrderNum = 0,
          lastLicence = _.last($scope.includedLicences);
        if (lastLicence) {
          lastOrderNum = _.last($scope.includedLicences).orderNum;
        }

        _.forEach(selectedLicences, function(licence) {
          var newlyAddedLicence = {
            orderNum: ++lastOrderNum,
            partIndex: licence.partIndex
          };
          $scope.currentLicenceEdition.part.includedLicences.push(newlyAddedLicence);

          licence.orderNum = newlyAddedLicence.orderNum;
          $scope.includedLicences.push(licence);
        });

        $scope.save();
      });

      return modalInstance.opened;
    };

    $scope.removeLicence = function (licence) {
      return scMessage('common.messages.confirmDelete')
        .then(function (result) {
          if (result === 'OK') {
            $scope.includedLicences = _.without($scope.includedLicences, licence);
            _.remove($scope.currentLicenceEdition.part.includedLicences,
              function(includedLicence) {
                return licence.partIndex === includedLicence.partIndex;
              });
            $scope.save();
          }
        });
    };

    $scope.changeOrder = function () {
      $scope.changeOrderMode = true;
    };

    $scope.saveOrder = function () {
      $scope.includedLicences = _.sortBy($scope.includedLicences, 'orderNum');
      $scope.changeOrderMode = false;
      $scope.currentLicenceEdition.part.includedLicences = [];
      _.forEach($scope.includedLicences, function (licence) {
        var changedLicence = {
          orderNum: licence.orderNum,
          partIndex: licence.partIndex
        };
        $scope.currentLicenceEdition.part.includedLicences.push(changedLicence);
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

  LicenceEditionsEditLicencesCtrl.$inject = [
    '$scope',
    '$state',
    '$stateParams',
    'PersonLicences',
    'PersonLicenceEditions',
    'currentLicenceEdition',
    'licenceEditions',
    'scMessage',
    'scModal'
  ];


  angular.module('gva')
    .controller('LicenceEditionsEditLicencesCtrl', LicenceEditionsEditLicencesCtrl);
}(angular, _));
