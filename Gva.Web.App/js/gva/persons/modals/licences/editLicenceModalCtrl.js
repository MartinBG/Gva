/*global angular, _*/
(function (angular, _) {
  'use strict';

  function EditLicenceModalCtrl(
    $scope,
    $modalInstance,
    scModalParams,
    scModal,
    PersonLicences,
    PersonLicenceEditions,
    licence,
    licenceEditions
  ) {
    $scope.form = {};
    $scope.licence = licence;
    $scope.lotId = scModalParams.lotId;
    $scope.lastEdition = _.last(licenceEditions);
    $scope.edition = _.find(licenceEditions, function (edition) {
      return edition.part.index === scModalParams.editionIndex;
    });

    $scope.save = function () {
      return $scope.form.editLicenceForm.$validate().then(function () {
        if ($scope.form.editLicenceForm.$valid) {
          return PersonLicenceEditions.save({
              id: scModalParams.lotId,
              ind: scModalParams.licencePartIndex,
              index: $scope.edition.partIndex
          }, $scope.edition)
          .$promise
          .then(function (savedLicenceEdition) {
            var returnValue = {};
            returnValue.savedLicenceEdition = savedLicenceEdition;
            returnValue.licence = $scope.licence;
            return $modalInstance.close(returnValue);
          });
        }
      });
    };

    $scope.cancel = function () {
      return $modalInstance.dismiss('cancel');
    };

    $scope.viewStatuses = function () {
      var params = {
        licence: $scope.licence,
        personId: scModalParams.lotId,
        licenceInd: scModalParams.licencePartIndex
      };

      var modalInstance = scModal.open('licenceStatuses', params);

      return modalInstance.opened;
    };
  }

  EditLicenceModalCtrl.$inject = [
    '$scope',
    '$modalInstance',
    'scModalParams',
    'scModal',
    'PersonLicences',
    'PersonLicenceEditions',
    'licence',
    'licenceEditions'
  ];

  EditLicenceModalCtrl.$resolve = {
    licence: [
      'PersonLicences',
      'scModalParams',
      function (PersonLicences, scModalParams) {
        return PersonLicences.get({
          id: scModalParams.lotId,
          ind: scModalParams.licencePartIndex
        }).$promise;
      }
    ],
    licenceEditions: [
      'PersonLicenceEditions',
      'scModalParams',
      function (PersonLicenceEditions, scModalParams) {
        return PersonLicenceEditions.query({
          id: scModalParams.lotId,
          ind: scModalParams.licencePartIndex
        }).$promise;
      }
    ]
  };

  angular.module('gva').controller('EditLicenceModalCtrl', EditLicenceModalCtrl);
}(angular, _));
