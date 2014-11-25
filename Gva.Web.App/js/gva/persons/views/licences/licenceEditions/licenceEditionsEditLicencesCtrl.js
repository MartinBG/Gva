/*global angular, _*/
(function (angular, _) {
  'use strict';

  function LicenceEditionsEditLicencesCtrl(
    $scope,
    $state,
    $stateParams,
    PersonLicenceEditions,
    currentLicenceEdition,
    licenceEditions,
    includedLicences,
    scMessage,
    scModal
  ) {

    $scope.currentLicenceEdition = currentLicenceEdition;
    $scope.isLast = _.last(licenceEditions).partIndex === currentLicenceEdition.partIndex;
        $scope.currentLicenceEdition.part.includedLicences =
      $scope.currentLicenceEdition.part.includedLicences || [];

    $scope.includedLicences = includedLicences;

    $scope.addExistingLicence = function () {
      var hideLicences = _.clone($scope.currentLicenceEdition.part.includedLicences);

      if ($stateParams.partIndex) {
        hideLicences.push(parseInt($stateParams.partIndex, 10));
      }

      var modalInstance = scModal.open('chooseLicences', {
        includedLicences: hideLicences,
        lotId: $stateParams.id
      });

       modalInstance.result.then(function (selectedLicences) {
        $scope.includedLicences = $scope.includedLicences.concat(selectedLicences);

        $scope.currentLicenceEdition.part.includedLicences = 
          $scope.currentLicenceEdition.part.includedLicences
          .concat(_.pluck(selectedLicences, 'partIndex'));

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
              function(includedLicencesPartIndex) {
                return licence.partIndex === includedLicencesPartIndex;
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
      $scope.currentLicenceEdition.part.includedLicences =
        _.pluck($scope.includedLicences, 'partIndex');
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
    'PersonLicenceEditions',
    'currentLicenceEdition',
    'licenceEditions',
    'includedLicences',
    'scMessage',
    'scModal'
  ];

  LicenceEditionsEditLicencesCtrl.$resolve = {
    includedLicences: [
      '$stateParams',
      'PersonLicences',
      'currentLicenceEdition',
      function ($stateParams, PersonLicences, currentLicenceEdition) {
        return PersonLicences
        .query({ id: $stateParams.id })
        .$promise
        .then(function (licences) {
          return _.map(currentLicenceEdition.part.includedLicences, function (partIndex) {
            return _.where(licences, { partIndex: partIndex })[0];
          });
        });
      }
    ]
  };

  angular.module('gva')
    .controller('LicenceEditionsEditLicencesCtrl', LicenceEditionsEditLicencesCtrl);
}(angular, _));
